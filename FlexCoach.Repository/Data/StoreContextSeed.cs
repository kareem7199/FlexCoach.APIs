using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;

namespace FlexCoach.Repository.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext _storeContext)
		{
			if (!_storeContext.Admins.Any())
			{
				var admin = new Admin()
				{
					Email = "kareemtameregy@gmail.com",
					Gender = Gender.MALE,
					Name = "Kareem Tamer",
					Password = "$2a$10$i8JNlOp5RpaNgiVcim1It.S9hjURCFyF11azIZAuEIunrWOeixUOy",
					PictureUrl = "dsalldsadas"
				};

				_storeContext.Add(admin);
				await _storeContext.SaveChangesAsync();
			}
		}
	}
}
