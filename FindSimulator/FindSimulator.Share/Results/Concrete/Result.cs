using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Results.Concrete
{
    public class Result : IResult
    {
        public Result() { }
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }
        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception.Message;
        }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        //public Exception Exception { get; }
        public string Exception { get; }

        // new Result(ResultStatus.Error,exception.message,exception)
    }
}
