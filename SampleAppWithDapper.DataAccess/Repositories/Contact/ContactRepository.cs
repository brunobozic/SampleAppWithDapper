﻿using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SampleAppWithDapper.DataAccess.Repositories.Contact.Extensions;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionProvider _databaseConnectionManager;
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public ContactRepository(IDbConnectionProvider databaseConnectionManager)
        {
            _connection = databaseConnectionManager.Connection;
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            // _transaction = databaseConnectionManager.Transaction;
            // _databaseConnectionManager = databaseConnectionManager;
        }

        public async Task<PaginatedContacts> GetPaginatedResultsAsync(ContactsGetAllPaginatedRequest request)
        {
            var retVal = new PaginatedContacts();

            var resultAsync = await _connection.QueryMultipleAsync("usp_Contacts_GetPaginated"
                ,
                new
                {
                    SearchTerm = request.SearchTerm,
                    SortColumn = request.SortColumn,
                    SortOrder = request.SortOrder,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                }
                , commandType: CommandType.StoredProcedure);


            var contacts = await resultAsync.ReadAsync<Domain.DomainModels.Contact.Contact>();
            var filtertedCount = await resultAsync.ReadAsync<int>();
            retVal.TotalCount = contacts.Count();
            retVal.Contacts = contacts.ConvertToViewModel().ToList();
            retVal.FilteredCount = filtertedCount.FirstOrDefault();

            return retVal;
        }

        public async Task<ContactCreateResponse> ContactCreateAsync(ContactCreateRequest createRequest)
        {
            var response = new ContactCreateResponse();

            var resultAsync = await _connection.QueryMultipleAsync("usp_Insert_Contact"
                ,
                new
                {
                    createRequest.FirstName,
                    createRequest.LastName,
                    createRequest.EMail,
                    createRequest.TelephoneNumber_Entry,
                    createRequest.Creator
                }
                , commandType: CommandType.StoredProcedure);

            var contact = resultAsync.ReadSingleOrDefault<Domain.DomainModels.Contact.Contact>().ConvertToDto();

            response.Contact = contact;

            return response;
        }

        public async Task<ContactGetByIdResponse> GetContactByIdAsync(int contactId)
        {
            if (contactId <= 0) throw new ArgumentNullException(nameof(contactId));

            var retVal = new ContactGetByIdResponse
            {
                Success = false,
                Message = ""
            };

            try
            {
                // using a stored procedure here is not normally needed, but is also not harmful.
                var resultAsync = await _connection.QueryMultipleAsync("usp_Contact_GetById",
                    new
                    {
                        Id = contactId
                    }
                    , commandType: CommandType.StoredProcedure);

                var result = resultAsync.ReadSingleOrDefault<Domain.DomainModels.Contact.Contact>().ConvertToDto();

                retVal.Success = true;
                retVal.Contact = result;
            }
            catch (Exception ex)
            {
                retVal.Success = false;
                retVal.Message = ex.Message;
            }

            return retVal;
        }

        public async Task<ContactUpdateResult> UpdateContactAsync(int id, ContactUpdateRequest updateRequest)
        {
            var retVal = new ContactUpdateResult();

            try
            {
                // using a stored procedure here is not normally needed, but is also not harmful.
                var resultAsync = await _connection.QueryMultipleAsync("usp_Contact_Update",
                    new
                    {
                        id,
                        updateRequest.FirstName,
                        updateRequest.LastName,
                        updateRequest.EMail,
                        updateRequest.TelephoneNumber_Entry,
                        updateRequest.Updater
                    }
                    , commandType: CommandType.StoredProcedure);

                var result = resultAsync.ReadSingleOrDefault<Domain.DomainModels.Contact.Contact>();

                retVal.Success = true;
                retVal.Contact = result;
            }
            catch (Exception ex)
            {
                retVal.Success = false;
                retVal.Message = ex.Message;
            }

            return retVal;
        }
    }
}
