using Film.Application.Contract.Base.Dtos;

namespace Film.Application.Contract.Film.Dtos
{
    public class FilmsListFilterRequestDto : PageRequestDto
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string CategoryName { get; set; }
        public int Code { get; set; }
    }

}
