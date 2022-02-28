using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Claims
{
    public interface IClaimService
    {
        string GetToken();

        string GetClaim(string key);
    }
}
