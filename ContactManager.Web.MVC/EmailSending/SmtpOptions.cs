﻿namespace SampleAppWithDapper.EmailSending
{
    public class SmtpOptions
    {
        public string EmailIsFrom { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}