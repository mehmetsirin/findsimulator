using FindSimulator.Service.Model.Payment;
using FindSimulator.Share.Results.Concrete;

using IyzipayCore;
using IyzipayCore.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public interface IPaymentManager
    {
        public Task<DataResult<object>> Add(AddPaymentBO bo);
    }
}
