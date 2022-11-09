﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAppWithDapper.Models.Contacts
{
    public class ContactDeleteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Display_Name_Email", ResourceType = typeof(Resource.Resource))]
        public string EMail { get; set; }

        [Display(Name = "Display_Name_FirstName", ResourceType = typeof(Resource.Resource))]
        public string FirstName { get; set; }

        [Display(Name = "Display_Name_LastName", ResourceType = typeof(Resource.Resource))]
        public string LastName { get; set; }

        [Display(Name = "Display_Name_TelephoneNumber", ResourceType = typeof(Resource.Resource))]
        public string TelephoneNumber_Entry { get; set; }

        public int TotalCount { get; set; }

        [Display(Name = "Display_Name_Action", ResourceType = typeof(Resource.Resource))]
        public string Action { get; set; }

        [Display(Name = "Display_Name_CreatedUtc", ResourceType = typeof(Resource.Resource))]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Display_Name_ModifiedUtc", ResourceType = typeof(Resource.Resource))]
        public DateTimeOffset? ModifiedUtc { get; set; }

        [Display(Name = "Display_Name_CreatedBy", ResourceType = typeof(Resource.Resource))]
        public string CreatedBy { get; set; }

        [Display(Name = "Display_Name_ModifiedBy", ResourceType = typeof(Resource.Resource))]
        public string ModifiedBy { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}