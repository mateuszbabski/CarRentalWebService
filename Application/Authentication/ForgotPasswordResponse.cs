﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class ForgotPasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
    }
}
