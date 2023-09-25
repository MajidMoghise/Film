using Film.Application.Contract.Base.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.Category.Dtos
{
    public class CategoryDeleteRequestDto : BaseDto
    {
        public int Code { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
