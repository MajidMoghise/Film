using Film.Application.Contract.Base.Dtos;

namespace Film.Application.Contract.Category.Dtos
{
    public class CategoriesListFilterRequestDto : PageRequestDto
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }

    }

}
