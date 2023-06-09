﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    [Table("chat")]
    public class Chat
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("chat_id")]
        public long ChatId { get; set; }
        public List<Application> Applications { get; set; }
    }
}
