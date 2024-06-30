using System;
using System.IO;
using System.Threading.Tasks;
using FlexCoach.Core.Services.Contract;
using Microsoft.AspNetCore.Http;

namespace FlexCoach.Service.ImageUploadService
{
	public class ImageUploadService : IImageUploadService
	{
		private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

		public async Task<string> UploadFile(IFormFile file, string folderName)
		{
			// Validate file type
			var allowedExtensions = new[] { ".png", ".jpeg", ".jpg" };
			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

			if (!allowedExtensions.Contains(extension))
			{
				throw new InvalidOperationException("Only PNG, JPEG, and JPG files are allowed.");
			}

			// Validate file size
			if (file.Length > MaxFileSize)
			{
				throw new InvalidOperationException("File size must be less than 5 MB.");
			}

			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			string fileName = $"{Guid.NewGuid()}{extension}";

			string filePath = Path.Combine(folderPath, fileName);

			using var fileStream = new FileStream(filePath, FileMode.Create);

			await file.CopyToAsync(fileStream);

			return fileName;
		}
	}
}
