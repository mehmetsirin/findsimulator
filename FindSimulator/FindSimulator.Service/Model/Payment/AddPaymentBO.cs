using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Payment
{
    public class AddPaymentBO
    {
        public Guid PlanId { get; set; }
        public int UserCount { get; set; }
        public string BillingType { get; set; }
        public string CreditCardNo { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }
        public string CompanyName { get; set; }
        public string LegalName { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int SessionsID { get; set; }
        public int SessionID { get; set; }
        public int SessionDetailID { get; set; }
    }
}
