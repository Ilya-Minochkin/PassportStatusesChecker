using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    [Table("readiness_response")]
    public class ReadinessResponse
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("uid")]
        public string Uid { get; set; }
        [Column("chat_id")]
        public Chat Chat { get; set; }
        [Column("reception_date")]
        public DateTime ReceptionDate { get; set; }
        public PublicStatus PublicStatus { get; set; }
        [Column("public_status_id")]
        public int PublicStatusId { get; set; }
        public InternalStatus InternalStatus { get; set; }
        [Column("internal_status_id")]
        public int InternalStatusId { get; set; }
    }
}
