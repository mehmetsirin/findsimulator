using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Share.ComplexTypes
{
    public class CommonEnum
    {
        public enum ErrorType
        {
            Successful = 1,
            IsNull = 2,
            CommonError = 3,
            Transaction = 4

        }
        public enum LogsEnum
        {

            Error = 1,
            Information = 2

        }
        public enum SessionDetailStatus
        {
            Close = 0,
            Open = 1,
            Pending = 2,
            Sold=3,
            Canceled=4,
            Training=5

        }
        public enum UserStatus
        {

        }

    }
}
