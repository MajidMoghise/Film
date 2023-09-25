using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.Base.Dtos
{
    public abstract class PageRequestDto
    {
        public PageRequestDto() { }

        public PageRequestDto(PageRequestDto listRequestModel)
        {
            PageIndex = listRequestModel.PageIndex;
            PageSize = listRequestModel.PageSize;
            OrderByAsces = listRequestModel.OrderByAsces;
            OrderByDesces = listRequestModel.OrderByDesces;
        }

        public PageRequestDto(int pageIndex, int pageSize, IEnumerable<string> orderByAsces, IEnumerable<string> orderByDesces)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            OrderByAsces = orderByAsces?.ToList();
            OrderByDesces = orderByDesces?.ToList();
        }

        public List<string> OrderByAsces { get; set; }
        public List<string> OrderByDesces { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}
