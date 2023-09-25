using Film.Application.Contract.Base.Dtos;

namespace Film.Application.Contract.Category.Dtos
{
    public class CreateCategoryDto:BaseDto
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Code { get; set; }
    }
}
