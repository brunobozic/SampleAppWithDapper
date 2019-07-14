﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SampleAppWithDapper.ControllerExtensions;
using SampleAppWithDapper.DataAccess.Repositories.Contact;
using SampleAppWithDapper.DataTablesHelpers;
using SampleAppWithDapper.LoggingHelper;
using SampleAppWithDapper.Models;
using SampleAppWithDapper.Models.Contacts;
using SampleAppWithDapper.Models.Contacts.Extensions;
using SampleAppWithDapper.ToastrAlertHelpers;
using SampleAppWithDapper.UserHelpers;

namespace SampleAppWithDapper.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(ILoggingHelper logging, IContactRepository contactRepository) : base(logging)
        {
            _contactRepository = contactRepository;
        }

        // GET: Contact
        public ActionResult Index()
        {
            var vm = new ContactViewModel();

            return View(vm);
        }

        // GET: Contact
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "FirstName, LastName, EMail, TelephoneNumber_Entry")]CreateContactViewModel createContactVModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var req = new ContactCreateRequest
                    {
                        LastName = createContactVModel.LastName,
                        FirstName = createContactVModel.FirstName,
                        EMail = createContactVModel.EMail,
                        TelephoneNumber_Entry = createContactVModel.TelephoneNumber_Entry,
                        Creator = LoggedInUserFacade.UserName
                    };

                    var result = await _contactRepository.ContactCreateAsync(req);

                    this.AddToastMessage(@Resource.Resource.Toast_Success, @Resource.Resource.CreateContact_Toast_Success, ToastType.Success);

                    return RedirectToAction("Edit", new { id = result.Contact.Id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", @Resource.Resource.Model_Error_Unable_To_Perform_Action);

                this.AddToastMessage(@Resource.Resource.CreateContact_Toast_Failure, ex.Message, ToastType.Error);

                var em = new ContactCreateExceptionMessage { Message = ex.Message };

                return View("ContactCreationFailed", em);
            }

            return View(createContactVModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _contactRepository.GetContactByIdAsync(id);

            if (result.Success)
            {
                var vm = result.Contact.ConvertToViewModel();

                return View(vm);
            }
            else
            {
                var vm = new GenericErrorVM { ErrorMessage = result.Message };

                return View("../GenericError/GenericError", vm);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Edit(int? id, [Bind(Include = "FirstName, LastName, EMail, TelephoneNumber_Entry")]EditContactViewModel editModel)
        {
            var req = new ContactUpdateRequest
            {
                Id = id ?? editModel.Id,
                FirstName = editModel.FirstName,
                LastName = editModel.LastName,
                EMail = editModel.EMail,
                TelephoneNumber_Entry = editModel.TelephoneNumber_Entry,
                Updater = "SystemFakeUser"
            };

            var result = await _contactRepository.UpdateContactAsync(req.Id, req);

            if (result.Success)
            {
                this.AddToastMessage(@Resource.Resource.Toast_Success, @Resource.Resource.UpdateContact_Toast_Success, ToastType.Success);

                return View(result.Contact.ConvertToViewModel());
            }

            var vm = new GenericErrorVM { ErrorMessage = result.Message };

            this.AddToastMessage(result.Message, @Resource.Resource.UpdateContact_Toast_Failure, ToastType.Error);

            return View("../GenericError/GenericError", vm);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _contactRepository.GetContactByIdAsync(id);

            if (result.Success)
            {
                var vm = result.Contact.ConvertToDeleteViewModel();

                return View(vm);
            }
            else
            {
                var vm = new GenericErrorVM { ErrorMessage = result.Message };

                return View("../GenericError/GenericError", vm);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id, [Bind(Include = "Id")]ContactDeleteViewModel deleteModel)
        {
            var req = new ContactDeleteRequest { Id = id ?? deleteModel.Id, Deleter = "FakeUserForDeletionTest" };

            var result = await _contactRepository.DeleteContactAsync(req);

            if (result.Success)
            {
                result.DeletedId = id.GetValueOrDefault();
                this.AddToastMessage(@Resource.Resource.Toast_Success, @Resource.Resource.DeleteContact_Toast_Success, ToastType.Success);

                return RedirectToAction("Deleted", result);
            }

            var vm = new GenericErrorVM { ErrorMessage = result.Message };
            this.AddToastMessage(@Resource.Resource.DeleteContact_Toast_Failure, result.Message, ToastType.Success);

            return View("../GenericError/GenericError", vm);
        }

        public async Task<JsonResult> CustomServerSideSearchActionAsync(DataTableDTOs.DataTableAjaxPostModel datatableAjaxPostModel)
        {
            var result = await ContactDatatableQuery(datatableAjaxPostModel);

            var totalResultsCount = result.Contacts.FirstOrDefault().TotalCount;
            var filteredResultsCount = result.FilteredCount;

            return Json(new
            {
                draw = datatableAjaxPostModel.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result.Contacts
            });
        }

        private async Task<PaginatedContactsViewModel> ContactDatatableQuery(DataTableDTOs.DataTableAjaxPostModel model)
        {
            var searchBy = model.search_extra ?? model.search?.value ?? "";
            // searchBy = model.search?.value ?? "";
            var take = model.length;

            var skip = model.start;

            var sortBy = "";
            var sortDir = "";
            var pgNr = skip / take;

            if (skip == 0)
            {
                pgNr = 1;
            }
            else
            {
                pgNr += 1;
            }

            if (model.order != null)
            {
                // we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
            }

            sortDir = model.order[0].dir;

            // convert the format that datatables gave us, into a DTO that can be used by our data access layer
            var requestForPaginatedContacts = new ContactsGetAllPaginatedRequest
            {
                PageNumber = pgNr,
                PageSize = take,
                SearchTerm = searchBy,
                SortColumn = sortBy,
                SortOrder = sortDir
            };

            var response = await _contactRepository.GetPaginatedResultsAsync(requestForPaginatedContacts);

            if (response.Success)
            {
                foreach (var item in response.Contacts)
                {
                    item.Action =
                        "<a type='button' class='btn btn-outline-dark btn-xs btnGridEdit' style='float:right; padding:6px' href='" +
                        this.Url.Action("Edit", "Contact", new { Id = item.Id }) +
                        "'><i class= 'fa fa-edit fa-lg'></i> EDIT</a>";
                }

                return response.Contacts.ConvertToPaginatedViewModel();
            }

            return null;
        }

        public ActionResult ContactCreationConfirmed(ContactCreateResponse contactCreateResponse)
        {
            return View();
        }

        public ActionResult ContactCreationFailed(ContactCreateExceptionMessage em)
        {
            return View(em);
        }

        public ActionResult Deleted(ContactDeleteResponse cdr)
        {
            return View("../Contact/ContactDeleted", cdr);
        }
    }

}