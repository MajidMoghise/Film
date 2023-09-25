using Film.Domain.Contract.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Domain.Contract.Category.Models
{
    public class CategoryDeleteRequestModel : BaseModel
    {
        public int Code { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
