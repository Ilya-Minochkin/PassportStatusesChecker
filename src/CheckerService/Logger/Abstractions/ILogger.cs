using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerService.Logger.Abstractions
{
    public interface ILogger
    {
        void Information(string message);
        void Error(string message);
    }
}
