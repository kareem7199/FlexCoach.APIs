using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoach.Core.Entities
{
	public class Account : BaseEntity
	{
        public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
        public string PictureUrl { get; set; }
        public Gender Gender { get; set; }
    }
}
