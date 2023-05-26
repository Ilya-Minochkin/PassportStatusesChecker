using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    [Table("application")]
    public class Application
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("chat_id")]
        public Chat Chat { get; set; }
        [Column("number")]
        public string Number { get; set; }
    }
}
