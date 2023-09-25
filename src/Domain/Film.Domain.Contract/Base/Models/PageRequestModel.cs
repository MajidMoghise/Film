using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Domain.Contract.Base.Models
{
    public class PageRequestModel : BaseModel
    {
        public PageRequestModel() { }

        public PageRequestModel(PageRequestModel listRequestModel)
        {
            PageIndex = listRequestModel.PageIndex;
            PageSize = listRequestModel.PageSize;
            OrderByAsces = listRequestModel.OrderByAsces;
            OrderByDesces = listRequestModel.OrderByDesces;
        }

        public PageRequestModel(int pageIndex, int pageSize, IEnumerable<string> orderByAsces, IEnumerable<string> orderByDesces)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            OrderByAsces = orderByAsces?.ToList();
            OrderByDesces = orderByDesces?.ToList();
        }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<string> OrderByAsces { get; set; }
        public List<string> OrderByDesces { get; set; }
    }
}
