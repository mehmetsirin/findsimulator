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
        public enum CriminalEnum
        {

            OgsHgs = 1,
            Accident = 4,
            Examination = 3,
            TrafficCriminal = 2,
            Maintenance = 9,
            Muayene = 10,
            Emmisyon = 11,
            Mtv = 12,
            Trafficinsurance = 13,
            Kasko = 14,
            Akaryakit = 15
        }
    }
}
