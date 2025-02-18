﻿using NetReact.Domain.Interfaces;
using NetReact.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetReact.Application.Common
{
     public static class ServiceUtils
     {
          public static async Task<string> SaveFile(IWebHostEnvironment environment, IFormFile file, string directory)
          {
               if (file == null || file.Length == 0) return null;

               var filePath = Path.Combine(directory, file.FileName);
               var absolutePath = Path.Combine(environment.WebRootPath, filePath);

               if (File.Exists(filePath))
               {
                    return filePath;
               }
               
               await using var fileStream = new FileStream(absolutePath, FileMode.Create);
               await file.CopyToAsync(fileStream);

               return filePath;
          }

          public static bool CheckBookWithIsbnExists(IBookRepository bookRepository, string isbn)
          {
               return bookRepository.GetBooksByCondition(b => b.Isbn == isbn).Any();
          }

          public static bool CheckBookCategoryExists(ICategoryRepository categoryRepository, string name)
          {
               return categoryRepository.GetAllByCondition(c => c.Name == name).Any();
          }
     }
}
