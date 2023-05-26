using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerService.Models
{
    public class Application
    {
        public int Id { get; set; }
        public Chat Chat { get; set; }
        public string Number { get; set; }
    }
}
