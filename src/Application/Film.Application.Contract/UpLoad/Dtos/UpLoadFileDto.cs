using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.UpLoad.Dtos
{
    public class UpLoadFileDto
    {
        public IFormFile File { get; set; }
 
    }
}
