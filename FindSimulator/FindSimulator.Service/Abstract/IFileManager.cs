using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public interface IFileManager
    {
        Task<DataResult<string>> Upload(IFormFile formFile);
    }
}
