using Film.Application.Contract.Base.Dtos;

namespace Film.Application.Contract.Film.Dtos
{
    public class FilmDto : BaseDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] RowVersion { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
