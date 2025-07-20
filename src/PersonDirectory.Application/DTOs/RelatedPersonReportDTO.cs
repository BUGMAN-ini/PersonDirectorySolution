using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.DTOs
{
    public class RelatedPersonReportDTO
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public Dictionary<string, int> RelationCounts { get; set; } = new();
    }
}
