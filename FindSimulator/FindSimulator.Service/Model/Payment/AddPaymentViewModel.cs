﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Payment
{
    public class AddPaymentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
