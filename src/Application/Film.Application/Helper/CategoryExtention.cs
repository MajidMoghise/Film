using Film.Application.Contract.Category.Dtos;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Film.Application.Helper
{
    public static class CategoryExtention
    {
        public static byte[] GetSHA256(this Domain.Enities.Category obj)
        {
            var CategoryExtantionDto = MapperCategoryExtantion.CategoryExtantionDto(obj);
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(CategoryExtantionDto)));
            }
        }
        public static byte[] GetSHA256(this CreateCategoryDto obj)
        {
            var CategoryExtantionDto = MapperCategoryExtantion.CategoryExtantionDto(obj);
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(CategoryExtantionDto)));
            }
        }
        class MapperCategoryExtantion
        {
            internal static CategoryExtantionDto CategoryExtantionDto(Domain.Enities.Category input)
            {
                return new CategoryExtantionDto
                {
                    Priority = input.Priority,
                    Code = input.Code,
                    Description = input.Description,
                    IsEnabled = input.IsEnabled,
                    Name = input.Name
                };
            }internal static CategoryExtantionDto CategoryExtantionDto(CreateCategoryDto input)
            {
                return new CategoryExtantionDto
                {
                    Priority = input.Priority,
                    Code = input.Code,
                    Description = input.Description,
                    IsEnabled = input.IsEnabled,
                    Name = input.Name
                };
            }
        }
        class CategoryExtantionDto
        {
            public string Description { get; set; }
            public int Priority { get; set; }
            public int Code { get; set; }
            public string Name { get; set; }
            public bool IsEnabled { get; set; }

        }
    }
}
