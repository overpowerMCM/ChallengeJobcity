using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.Sources
{
    public interface ICSVParser
    {
        List<List<string>> Parse( byte[] data );
    }
}
