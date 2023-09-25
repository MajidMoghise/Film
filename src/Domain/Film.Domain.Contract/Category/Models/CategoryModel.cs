using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Category.Models
{
    public class CategoryModel : BaseModel
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
