using Film.Application.Contract.Base;

namespace Film.Application.Contract.UpLoad.Dtos
{
    public class SyncFromFileDto
    {
        public string FileNameFilms { get; set; }
        public string FileNameCategories { get; set; }
    }
}
