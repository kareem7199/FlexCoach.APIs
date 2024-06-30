using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Services.Contract;
using Microsoft.AspNetCore.Http;

namespace FlexCoach.Service.ImageUploadService
{
	public class ImageUploadService : IImageUploadService
	{
		public async Task<string> UploadFile(IFormFile file, string folderName)
		{
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" , folderName);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			string filePath = Path.Combine(folderPath, fileName);

			using var fileStream = new FileStream(filePath, FileMode.Create);

			await file.CopyToAsync(fileStream);

			return fileName;
		}
	}
}
