using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Core.Model
{
   public  class FileUpload
    {
        #region FilUploadDetails

        
        public IFormFile ExcelValues { get; set; }
        public string Filename { get; set; }
        public byte[] FileByte { get; set; }
        public string ContentType { get; set; }

        #endregion
    }
}
