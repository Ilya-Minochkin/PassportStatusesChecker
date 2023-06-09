﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    [Table("internal_status")]
    public class InternalStatus
    {
        [Column("id")]
        public int Id { get; set; }
        public ReadinessResponse Response { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("percent")]
        public int Percent { get; set; }
    }
}
