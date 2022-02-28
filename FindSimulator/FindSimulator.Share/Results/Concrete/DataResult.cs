using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(T data)
        {
            Data = data;
            ResultStatus = ResultStatus.Success;
        }

        public DataResult()
        {
        }

        public DataResult(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }

        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }
        //public DataResult(ResultStatus resultStatus, Exception exception)
        //{
        //    ResultStatus = resultStatus;
        //    Exception = exception;
        //}
        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }
        //public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        //{
        //    ResultStatus = resultStatus;
        //    Message = message;
        //    Data = data;
        //    Exception = exception;
        //}
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        //public Exception Exception { get; }
        public T Data { get; set; }
    }
}
