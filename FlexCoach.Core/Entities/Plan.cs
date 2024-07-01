using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoach.Core.Entities
{
	public class Plan : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public int Price { get; set; }
        public int CoachId { get; set; }
        public Coach Coach { get; set; }

    }
}
