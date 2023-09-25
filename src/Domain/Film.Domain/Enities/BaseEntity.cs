using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Domain.Enities
{
    public class BaseEntity
    {
        public int Code{ get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] Hash { get; set; }

        public byte[] RowVersion { get; set; }

    }
}
