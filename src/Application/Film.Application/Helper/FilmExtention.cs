using Film.Application.Contract.Film.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Helper
{
    public static class FilmExtention
    {
        public static byte[] GetSHA256(this Domain.Enities.Film obj)
        {
            var filmExtantionDto = MapperFilmExtantion.FilmExtantionDto(obj);
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(filmExtantionDto)));
            }
        }
        public static byte[] GetSHA256(this CreateFilmDto obj)
        {
            var filmExtantionDto = MapperFilmExtantion.FilmExtantionDto(obj);
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(filmExtantionDto)));
            }
        }
        class MapperFilmExtantion
        {
            internal static FilmExtantionDto FilmExtantionDto(Domain.Enities.Film input)
            {
                return new FilmExtantionDto
                {
                    CategoryId = input.CategoryId,
                    Code = input.Code,
                    Description = input.Description,
                    IsEnabled = input.IsEnabled,
                    Name = input.Name
                };
            }internal static FilmExtantionDto FilmExtantionDto(CreateFilmDto input)
            {
                return new FilmExtantionDto
                {
                    CategoryId = input.CategoryId,
                    Code = input.Code,
                    Description = input.Description,
                    IsEnabled = input.IsEnabled,
                    Name = input.Name
                };
            }
        }
        class FilmExtantionDto
        {
            public string Description { get; set; }
            public int CategoryId { get; set; }
            public int Code { get; set; }
            public string Name { get; set; }
            public bool IsEnabled { get; set; }

        }
    }
}
