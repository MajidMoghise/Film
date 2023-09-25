using Film.Application.Contract.Attributes;
using Film.Application.Contract.Base.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.Film.Dtos
{
    [RequiredValidation]
    public class FilmDeleteRequestDto:BaseDto
    {
        public int Code { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
