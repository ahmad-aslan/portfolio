﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class EmailSender : ISenderEmail
    {
        public Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
        {
            return Task.CompletedTask;
        }
    }
}
