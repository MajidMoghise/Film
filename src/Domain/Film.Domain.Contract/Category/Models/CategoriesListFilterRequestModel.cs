using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Category.Models
{
    public class CategoriesListFilterRequestModel : PageRequestModel
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }

    }

}
