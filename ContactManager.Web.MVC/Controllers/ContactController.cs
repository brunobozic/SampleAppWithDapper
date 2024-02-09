﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleAppWithDapper.DataAccess.MessagePattern;
using SampleAppWithDapper.DataAccess.Repositories.Contact;
using SampleAppWithDapper.DataTablesHelpers;
using SampleAppWithDapper.Models;
using SampleAppWithDapper.Models.Contacts;
using SampleAppWithDapper.Models.Contacts.Extensions;
using SampleAppWithDapper.UserHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAppWithDapper.Controllers
{
    [AllowAnonymous]
    public class ContactController : BaseController
    {
        private readonly IContactRepositoryAsync _contactRepository;

        public ContactController(IContactRepositoryAsync contactRepository, INotyfService notifyService)
        {
            _contactRepository = contactRepository;
            _notifyService = notifyService;
        }

        public INotyfService _notifyService { get; }

        public ActionResult ContactCreationConfirmed(ContactCreateResponse contactCreateResponse, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        public ActionResult ContactCreationFailed(ContactCreateExceptionMessage em, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View(em);
        }

        // GET: Contact
        [HttpGet]
        public ActionResult Create(string currentFilter)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            
            TempData["CurrentFilter"] = currentFilter;
            

            return View();
        }

        // POST: Contact
        [HttpPost]
        public async Task<ActionResult> Create([Bind("FirstName, LastName, EMail, TelephoneNumber_Entry")] CreateContactViewModel createContactVModel, string currentFilter, string returnUrl = null)
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

                    if (result.Success)
                    {
                        _notifyService.Success("Contact " + result.Contact.EMail + " created ");

                        ViewData["ReturnUrl"] = returnUrl;

                        return RedirectToAction("Edit", new { id = result.Contact.Id, purpose = "AfterCreate", currentFilter = currentFilter });
                    }
                }
                else
                {
                    _notifyService.Error("Invalid model.");

                    ViewData["ReturnUrl"] = returnUrl;

                    return View(createContactVModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", @Resource.Resource.Model_Error_Unable_To_Perform_Action);

                var em = new ContactCreateExceptionMessage { Message = ex.Message };

                Log.Error(ex, ex.Message);

                _notifyService.Error(ex.Message);

                ViewData["ReturnUrl"] = returnUrl;

                return View("../Contact/ContactCreationFailed", em);
            }

            return View(createContactVModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id, string currentFilter, string returnUrl = null)
        {
            var result = await _contactRepository.GetContactByIdAsync(id);

            if (result.Success)
            {
                var vm = result.Contact.ConvertToDeleteViewModel();

                Log.Warning("Delete contact - Name: [ " + result.Contact.FirstName + "], Surname: [ " + result.Contact.LastName + " ]");

                ViewData["ReturnUrl"] = returnUrl;
                
                TempData["CurrentFilter"] = currentFilter;
                

                return View(vm);
            }
            else
            {
                var vm = new GenericErrorVM { ErrorMessage = result.Message };

                Log.Error(result.Message);

                _notifyService.Error(vm.ErrorMessage);

                ViewData["ReturnUrl"] = returnUrl;
                
                TempData["CurrentFilter"] = currentFilter;
                

                return View("../GenericError/GenericError", vm);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id, [Bind("Id")] ContactDeleteViewModel deleteModel, string currentFilter, string returnUrl = null)
        {
            var req = new ContactDeleteRequest { Id = id ?? deleteModel.Id, Deleter = "FakeUserForDeletionTest" };

            var result = await _contactRepository.DeleteContactAsync(req);

            if (result.Success)
            {
                result.DeletedId = id.GetValueOrDefault();

                _notifyService.Success("Contact " + result.DeletedId + " deleted ");

                ViewData["ReturnUrl"] = returnUrl;
                
                TempData["CurrentFilter"] = currentFilter;
                

                result.CurrentFilter = currentFilter;

                return RedirectToAction("Deleted", result);
            }

            var vm = new GenericErrorVM { ErrorMessage = result.Message };

            Log.Error(result.Message);

            _notifyService.Error(vm.ErrorMessage);

            ViewData["ReturnUrl"] = returnUrl;
            
            TempData["CurrentFilter"] = currentFilter;
            

            return View("../GenericError/GenericError", vm);
        }

        [HttpGet]
        public ActionResult Deleted(ContactDeleteResponse cdr, string currentFilter, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["CurrentFilter"] = cdr.CurrentFilter;
            TempData["CurrentFilter"] = cdr.CurrentFilter;
            ViewBag.CurrentFilter = cdr.CurrentFilter;

            return View("../Contact/ContactDeleted", cdr);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id, string purpose, string currentFilter, string returnUrl = null)
        {
            var result = await _contactRepository.GetContactByIdAsync(id);

            if (result.Success)
            {
                var vm = new ContactViewModel
                {
                    Id = result.Contact.Id,
                    EMail = result.Contact.EMail,
                    Created = result.Contact.CreatedUtc.UtcDateTime,
                    CreatedBy = result.Contact.CreatedBy,
                    // Modified = result.Contact.ModifiedUtc,
                    ModifiedBy = result.Contact.ModifiedBy,
                    FirstName = result.Contact.FirstName,
                    LastName = result.Contact.LastName,
                    TelephoneNumber_Entry = result.Contact.TelephoneNumber_Entry
                };

                if (purpose == "AfterCreate") { vm.PurposeMessage = "New contact has been created for you, you may make final changes to it."; }

                ViewData["ReturnUrl"] = returnUrl;
                
                TempData["CurrentFilter"] = currentFilter;
                

                return View(vm);
            }
            else
            {
                var vm = new GenericErrorVM { ErrorMessage = result.Message };

                Log.Error(result.Message);

                _notifyService.Error(vm.ErrorMessage);

                ViewData["ReturnUrl"] = returnUrl;
                
                TempData["CurrentFilter"] = currentFilter;
                

                vm.SearchFilter = currentFilter;

                return View("../GenericError/GenericError", vm);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int? id, string purpose, [Bind("FirstName, LastName, EMail, TelephoneNumber_Entry")] ContactViewModel editModel, string currentFilter, string returnUrl = null)
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

            if (ModelState.IsValid)
            {
                var result = await _contactRepository.UpdateContactAsync(req.Id, req);

                if (result.Success)
                {
                    _notifyService.Success("Contact " + result.Contact.EMail + " editted ");

                    result.SearchFilter = currentFilter;
                    TempData["CurrentFilter"] = currentFilter;

                    return View("ContactEditConfirmed", result.Contact.ConvertToViewModel());
                }

                var vm = new GenericErrorVM { ErrorMessage = result.Message };
                vm.SearchFilter = "";

                Log.Error(result.Message);

                _notifyService.Error(vm.ErrorMessage);

                ViewData["ReturnUrl"] = returnUrl;
                TempData["CurrentFilter"] = currentFilter;


                return View("../GenericError/GenericError", vm);
            }
            else
            {
                _notifyService.Error("Invalid model.");

                ViewData["ReturnUrl"] = returnUrl;
                TempData["CurrentFilter"] = currentFilter;
                

                return View(editModel);
            }
        }

        // GET: Contact
        [HttpGet]
        public ActionResult Index(string currentFilter = "", string returnUrl = "")
        {
            var vm = new ContactViewModel();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            ViewData["ReturnUrl"] = returnUrl;
            
            TempData["CurrentFilter"] = currentFilter;
            
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> SearchAction(DataTableDTOs.DataTableAjaxPostModel datatableAjaxPostModel, string returnUrl = null)
        {
            var result = await ContactDatatableQuery(datatableAjaxPostModel, returnUrl);

            var totalResultsCount = result.Contacts.FirstOrDefault().TotalCount;
            var filteredResultsCount = result.FilteredCount;

            // ViewBag.CurrentFilter = datatableAjaxPostModel.search;
            ViewBag.CurrentColumns = datatableAjaxPostModel.columns;
            ViewBag.CurrentStart = datatableAjaxPostModel.start;
            ViewBag.CurrentFilter = datatableAjaxPostModel.search_extra;

            return Json(new
            {
                draw = datatableAjaxPostModel.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result.Contacts
            });
        }

        private async Task<PaginatedContactsViewModel> ContactDatatableQuery(DataTableDTOs.DataTableAjaxPostModel model, string returnUrl = null)
        {
            var searchBy = model.search_extra ?? model.search?.value ?? "";
            // searchBy = model.search?.value ?? "";
            var take = model.length;

            var skip = model.start;

            var sortBy = "";
            var sortDir = "";
            var pgNr = 0;

            if (skip > 0)
            {
                pgNr = skip / take;
            }
            else if (skip == 0)
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
                if (response.Contacts.Count > 0)
                {
                    foreach (var item in response.Contacts)
                    {
                        if (item != null)
                        {
                            item.Action =
                                "<a type='button' class='btn btn-light btnGridEdit' style='padding:6px; font-size: 12px !important' href='" +
                                this.Url.Action("Edit", "Contact", new { Id = item.Id, purpose = "", currentFilter = searchBy, returnUrl = "" }) +
                                "'><i class= 'fa fa-edit fa-lg'></i> EDIT</a>";
                        }
                    }

                    var converted = response.Contacts.ConvertToPaginatedViewModel();
                    converted.FilteredCount = response.FilteredCount;

                    return converted;
                }
                else
                {
                    var nfText = Resource.Resource.Datatables_No_Record_Found;
                    var nullContact = new ContactViewModel
                    {
                        TotalCount = 1,
                        EMail = nfText,
                        FirstName = nfText,
                        LastName = nfText,
                        TelephoneNumber_Entry = nfText
                    };

                    var list = new List<ContactViewModel> { nullContact };

                    return new PaginatedContactsViewModel
                    {
                        Contacts = list,
                        FilteredCount = 0,
                        TotalCount = 0
                    };
                }
            }

            return null;
        }
    }
}