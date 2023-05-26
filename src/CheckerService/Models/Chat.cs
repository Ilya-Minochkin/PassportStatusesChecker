using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerService.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public List<Application> Applications { get; set; }
    }
}
