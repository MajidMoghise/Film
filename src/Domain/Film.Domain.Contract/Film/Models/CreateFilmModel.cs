using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Film.Models
{
    public class CreateFilmModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
