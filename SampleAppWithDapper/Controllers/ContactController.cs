using System;
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
using ContactViewModel = SampleAppWithDapper.DataAccess.Repositories.Contact.ContactViewModel;

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

                    this.AddToastMessage(@Resource.Resource.CreateContact_Toast_Success, "Success", ToastType.Success);

                    // return View("ContactCreationConfirmed", result);
                    var ecvm = new EditContactViewModel
                    {
                        Id = result.Contact.Id,
                        EMail = result.Contact.EMail,
                        FirstName = result.Contact.FirstName,
                        LastName = result.Contact.LastName,
                        TelephoneNumber_Entry = result.Contact.TelephoneNumber_Entry
                    };

                    return View("Edit", ecvm);
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
                this.AddToastMessage(@Resource.Resource.UpdateContact_Toast_Success, "Success", ToastType.Success);

                return View(editModel);
            }
            else
            {
                var vm = new GenericErrorVM { ErrorMessage = result.Message };

                this.AddToastMessage(@Resource.Resource.UpdateContact_Toast_Failure, result.Message, ToastType.Error);

                return View("../GenericError/GenericError", vm);
            }

        }


        public ActionResult Delete()
        {
            var vm = new ContactDeleteViewModel();

            return View(vm);
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

        private async Task<PaginatedContacts> ContactDatatableQuery(DataTableDTOs.DataTableAjaxPostModel model)
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

            var paginatedResults = await _contactRepository.GetPaginatedResultsAsync(requestForPaginatedContacts);

            foreach (var item in paginatedResults.Contacts)
            {
                item.Action = "<a type='button' class='btn btn-outline-dark btn-xs btnGridEdit' style='float:right; padding:6px' href='" + this.Url.Action("Edit", "Contact", new { Id = item.Id }) + "'><i class= 'fa fa-edit fa-lg'></i> EDIT</a>";
            }

            return paginatedResults;
        }

        public ActionResult ContactCreationConfirmed(ContactCreateResponse contactCreateResponse)
        {
            return View();
        }

        public ActionResult ContactCreationFailed(ContactCreateExceptionMessage em)
        {
            return View(em);
        }
    }

}