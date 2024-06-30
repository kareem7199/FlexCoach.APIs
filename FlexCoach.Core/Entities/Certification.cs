using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoach.Core.Entities
{
	public class Certification : BaseEntity
	{
        public string CertificateUrl { get; set; }
        public int CoachId { get; set; }

        public Coach Coach { get; set; }
    }
}
