using Film.Application.Contract.Base;
using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.UpLoad.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.UpLoad
{
    public interface IUploadService
    {
        Task UploadFile(UpLoadFileDto file);
        Task SyncFromFileUploaded(SyncFromFileDto sync);
    }
    
}
