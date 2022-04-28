using FindSimulator.Service.Abstract;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileManager fileManager;

        public FileController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        [HttpPost]
        public async Task<Object> Upload([FromForm] IFormFile getFile)
        {
            var data =  await fileManager.Upload(getFile);
            return data;
        }
    }
}
