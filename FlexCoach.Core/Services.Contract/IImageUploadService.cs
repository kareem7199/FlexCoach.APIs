using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlexCoach.Core.Services.Contract
{
	public interface IImageUploadService
	{
		public Task<string> UploadFile(IFormFile file, string folderName);
	}
}
