using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Film.Models
{
    public class FilmsListFilterRequestModel: PageRequestModel
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string CategoryName { get; set; }
        public int Code { get; set; }
    }

}
