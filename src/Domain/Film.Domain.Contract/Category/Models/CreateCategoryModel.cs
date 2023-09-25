using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Category.Models
{
    public class CreateCategoryModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

    }
}
