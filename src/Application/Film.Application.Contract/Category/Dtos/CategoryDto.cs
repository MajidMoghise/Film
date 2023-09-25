using Film.Application.Contract.Base.Dtos;

namespace Film.Application.Contract.Category.Dtos
{
    public class CategoryDto : BaseDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] RowVersion { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Priority { get; set; }
    }
}
