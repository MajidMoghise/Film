using Film.Application.Base;
using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Film.Dtos;
using Film.Application.Contract.Film;
using Film.Application.Helper;
using Film.Application.Services.Film;
using Film.Domain.Contract.Film;
using Film.Domain.Contract.Film.Models;

namespace Film.Application.Services.Film
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly FilmMapper _mapper;
        public FilmService(IFilmRepository FilmRepository)
        {
            _filmRepository = FilmRepository;
            _mapper = new FilmMapper();
        }

        public async Task<int> CreateFilm(CreateFilmDto film)
        {
            var _film = _mapper.Film(film);
            _film.Hash = _film.GetSHA256();
            await _filmRepository.AddAsync(_film);
            _filmRepository.UnitOfWork.Commit();
            if (_film.LastUpdate == DateTime.MinValue) { throw new BusinessException("Create not succeed", BusinessExceptionType.None); }
            return _film.Code;
        }

        public async Task DeleteFilm(FilmDeleteRequestDto Film)
        {
            var del = _mapper.Film(Film);
            await _filmRepository.Delete(del);
            _filmRepository.UnitOfWork.Commit();
            if (del.Code != 0) { throw new BusinessException("delete not succeed", BusinessExceptionType.None); }

        }

        public async Task<PageResponseDto<FilmsListResponseDto>> GetFilms(FilmsListFilterRequestDto filter)
        {
            var filtermodel = _mapper.FilmsListFilterRequestModel(filter);
            var resultModel = await _filmRepository.GetListAsync(filtermodel);
            return _mapper.PageResponseDto_FilmsListResponseDto(resultModel);
        }

        public async Task<FilmDetailResponseDto> GetDetailFilm(int FilmId)
        {
            var resultModel = await _filmRepository.GetDetailAsync(FilmId);
            return _mapper.FilmDetailResponseDto(resultModel);
        }

        public async Task<int> UpdateFilm(FilmDto Film)
        {
            var upd = _mapper.Film(Film);
            await _filmRepository.UpdateAsync(upd);
            _filmRepository.UnitOfWork.Commit();
            if (upd.LastUpdate == Film.LastUpdate) { throw new BusinessException("update not succeed", BusinessExceptionType.None); }
            return upd.Code;
        }

        public async Task BulkFilms(ICollection<CreateFilmDto> films)
        {
            var bulkRequests = _mapper.List_BulkRequestModel(films);
            var resultBulk = await _filmRepository.GetIdsFromHash(bulkRequests);
            await Task.WhenAll(
                Task.Run(async () =>
                {
                    var resultUpd = await SetUpdateFilmFromExcel(films, resultBulk.UpdateCodes);
                    await BulkUpdateFilms(resultUpd);
                }),
              Task.Run(async () =>
              {
                  var resultUpd = await SetUpdateFilmFromExcel(films, resultBulk.InsertCodes);
                  await BulkInsertFilms(resultUpd);
              }));
        }

        public async Task BulkUpdateFilms(ICollection<CreateFilmDto> films)
        {
            var upds = _mapper.Film(films);
            await _filmRepository.UpdateListAsync(upds);
        }

        public async Task BulkInsertFilms(ICollection<CreateFilmDto> films)
        {
            var inserts = _mapper.Film(films);
            await _filmRepository.AddListAsync(inserts);
        }
        private async Task<ICollection<CreateFilmDto>> SetUpdateFilmFromExcel(ICollection<CreateFilmDto> films, List<int> codes)
        {
            return await Task.Run(() => films.Where(w => codes.Contains(w.Code)).ToList());
        }
    }
}
