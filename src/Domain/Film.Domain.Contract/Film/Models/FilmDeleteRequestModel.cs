using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Domain.Contract.Film.Models
{
    public class FilmDeleteRequestModel
    {
        public int Code { get; set; }
        public byte[] RowVersion { get; set; }

    }

}
