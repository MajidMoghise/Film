using ExcelDataReader;
using ExcelMapper;
using Film.Application.Base;
using Film.Application.Contract.Base;
using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Category;
using Film.Application.Contract.Category.Dtos;
using Film.Application.Contract.Film;
using Film.Application.Contract.Film.Dtos;
using Film.Application.Contract.UpLoad;
using Film.Application.Contract.UpLoad.Dtos;
using Film.Domain.Enities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Services.Upload
{
    public class UploadService : IUploadService
    {
        private readonly string _filePath;
        private readonly IFilmService _filmService;
        private readonly ICategoryService _categoryService;

        public UploadService(IFilmService filmService, ICategoryService categoryService)
        {

            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "files", "uploads");
            CheckDirectory();

            _filmService = filmService;
            _categoryService = categoryService;
        }

        public async Task UploadFile(UpLoadFileDto file)
        {
           await Task.FromResult(() => { 
            DeleteFileIfExist(file.File.FileName);
            SaveFileOnDisk(file.File.FileName, file.File);
            });
        }

        public async Task SyncFromFileUploaded(SyncFromFileDto sync)
        {
            CheckfileExisted(sync.FileNameFilms);
            CheckfileExisted(sync.FileNameCategories);
            await LoadCategories(sync.FileNameCategories);
            await LoadFilms(sync.FileNameFilms);
        }

        private async Task LoadCategories(string fileNameOnDisk)
        {
            var list = new List<CreateCategoryDto>();
            using (var stream = File.OpenRead(Path.Combine(_filePath, fileNameOnDisk)))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        list.Add(new CreateCategoryDto
                        {
                            Code = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsEnabled = string.Equals(reader.GetString(2).Trim(), "فعال") ? true : false,
                            Description = reader.GetString(3),
                            Priority=reader.GetInt32(5)
                            
                        });
                    }
                }
            }
           await _categoryService.BulkCategories(list);
        }
        private async Task LoadFilms(string fileNameOnDisk)
        {
            var list = new List<CreateFilmDto>();
            using (var stream = File.OpenRead(Path.Combine(_filePath, fileNameOnDisk)))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        list.Add(new CreateFilmDto
                        {
                            Code = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsEnabled = string.Equals(reader.GetString(2).Trim(), "فعال") ? true : false,
                            CategoryId = reader.GetInt32(3),
                            Description = reader.GetString(4),
                            
                        });
                    }
                }
            }
            await _filmService.BulkFilms(list);
        }
        private void CheckDirectory()
        {
            if (!Directory.Exists(_filePath))
                Directory.CreateDirectory(_filePath);
        }
        private void SaveFileOnDisk(string fileNameOnDisk, byte[] file)
        {
            var path = Path.Combine(_filePath, fileNameOnDisk);

            MemoryStream ms = new(file);

            using FileStream fileStream = new(path, FileMode.Create);
            ms.CopyTo(fileStream);
        }
        private void SaveFileOnDisk(string fileNameOnDisk, IFormFile file)
        {
            var path = Path.Combine(_filePath, fileNameOnDisk);

            using FileStream fileStream = new(path, FileMode.Create);
            file.CopyTo(fileStream);
        }
        private void DeleteFileIfExist(string fileNameOnDisk)
        {
            var path = Path.Combine(_filePath, fileNameOnDisk);
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
        private void CheckfileExisted(string fileNameOnDisk)
        {
            var path = Path.Combine(_filePath, fileNameOnDisk);
            if (!File.Exists(path))
            {
                throw new BusinessException("file is not exist", BusinessExceptionType.NotFound);
            }

        }

    }
}
