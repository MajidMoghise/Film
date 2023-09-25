using Film.Domain.Contract.Base.Models;
using Film.Domain.Contract.Base.Repository;
using Film.Domain.Contract.Film.Models;
using Film.Domain.Enities;

namespace Film.Domain.Contract.Film
{
    public interface IFilmRepository:ICreateRepository<Enities.Film>,IUpdateRepository<Enities.Film>
    {
        Task<PageResponseModel<FilmsListResponseModel>> GetListAsync(FilmsListFilterRequestModel filter);
        Task<FilmDetailResponseModel> GetDetailAsync(int id);
        Task Delete(Enities.Film del);
    }
    
}