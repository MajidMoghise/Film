using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Film.Models
{
    public class FilmDetailResponseModel : BaseModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public byte[] RowVersion { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public byte[] Hash { get; set; }

    }

}
