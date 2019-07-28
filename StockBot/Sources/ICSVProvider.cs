using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StockBot.Sources
{
    public interface ICSVProvider
    {
        Task<byte[]> GetStockData(string code);
    }
}