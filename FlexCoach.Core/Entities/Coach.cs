using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoach.Core.Entities
{
	public class Coach : Account
	{
		public string AboutMe { get; set; }
		public int Experience { get; set; }
		public ICollection<Certification> Certifications { get; set; } = new HashSet<Certification>();
        public ICollection<Plan> Plans { get; set; } = new HashSet<Plan>();
    }
}
