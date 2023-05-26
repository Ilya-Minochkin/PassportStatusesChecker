using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Database.Entities
{
    [Table("public_status")]
    public class PublicStatus
    {
        [Column("id")]
        public int Id { get; set; }
        public ReadinessResponse Response { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("subscription")]
        public bool Subscription { get; set; }
    }
}
