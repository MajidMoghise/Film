using System.Collections.Generic;

namespace Film.Application.Contract.Base.Dtos
{
    public class PageResponseDto<TDto>: BaseDto where TDto : BaseDto
    {
        public ICollection<TDto> Data { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int PageIndex { get;  set; }
        public int PageSize { get;  set; }

    }
}
