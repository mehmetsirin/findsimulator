using FindSimulator.Service.Abstract;
using FindSimulator.Share.AppConfiguration;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class FileManager : IFileManager
    {
        private readonly IOptions<DocumentSettings> _options = null;

        public FileManager(IOptions<DocumentSettings> options)
        {
            _options = options;
        }

        public async Task<DataResult<string>> Upload(IFormFile getFile)
        {


            if (getFile != null)
            {
                string fileName = getFile.FileName;
                var guid = Guid.NewGuid();
                //var Upload = Path.Combine(_options.Value.Path, fileName).Replace(@"\\", @"\"); 
                var Upload = _options.Value.Path;
                if (!Directory.Exists(_options.Value.Path))
                {
                    Directory.CreateDirectory(_options.Value.Path);
                }
                Upload=Upload + "/" +guid+ fileName;
                getFile.CopyTo(new FileStream(Upload, FileMode.CreateNew));

                //model.Path = "http://178.18.200.116:81/DipaProfilePicture/" + fileName;
                Upload = _options.Value.PathShow+ "/" + guid+fileName;
                 //var path = Path.Combine(Upload, fileName);
             
                return new DataResult<string>(ResultStatus.Success,Upload,Upload);

            }
            return new DataResult<string>(ResultStatus.DataNull, "");

        }
    }
}
