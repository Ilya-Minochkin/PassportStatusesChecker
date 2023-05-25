using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class ReadinessResponse
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public Chat Chat { get; set; }
        public DateTime ReceptionDate { get; set; }
        public PublicStatus PublicStatus { get; set; }
        public InternalStatus InternalStatus { get; set; }
    }
}
