using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.RequestModel;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly ISessionDetailManager _sessionDetailManager;

        public OrderController(ISessionDetailManager sessionDetailManager)
        {
            _sessionDetailManager = sessionDetailManager;
        }

        [HttpPost]
        [Route("order-confirm")]
        public   async Task<DataResult<bool>> OrderConfirm([FromBody] OrderConfirmRequest request )
        {
          var data=   await _sessionDetailManager.OrderConfirm(request);
            return data;
        }
    }
}
