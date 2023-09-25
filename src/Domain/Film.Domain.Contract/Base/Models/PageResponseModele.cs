using System.Collections.Generic;

namespace Film.Domain.Contract.Base.Models
{
    public class PageResponseModel<TModle> where TModle : BaseModel
    {
        public PageResponseModel(ICollection<TModle> data, int totalCount, int pageIndex, int totalPage, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            TotalPage = totalPage;
            PageSize = pageSize;
        }
        public ICollection<TModle> Data { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPage { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

    }
}
