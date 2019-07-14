﻿namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactDeleteResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public bool ContactDeleted { get; set; }
        public int DeletedId { get; set; }
    }
}