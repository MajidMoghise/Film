using Film.Domain.Contract.Film.Models;
using System.Linq.Expressions;

namespace Film.Infrastructure.Persistance.Repositories.Film
{
    internal class FilmMapper
    {

        internal Expression<Func<Domain.Enities.Film, FilmDetailResponseModel>> FilmDetailResponseModel = (input) =>

          new FilmDetailResponseModel
          {
              CategoryId = input.CategoryId,
              CategoryName = input.Category.Name,
              Description = input.Description,
              Name = input.Name,
              IsEnabled = input.IsEnabled,
              Code = input.Code,
              LastUpdate = input.LastUpdate,
              RowVersion = input.RowVersion,

          };

        internal FilmsListResponseModel FilmsListResponseModel(Domain.Enities.Film s)
        {
            throw new NotImplementedException();
        }
    }
}
