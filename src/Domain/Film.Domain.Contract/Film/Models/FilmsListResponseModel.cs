using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Film.Models
{
    public class FilmsListResponseModel:BaseModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }

}
