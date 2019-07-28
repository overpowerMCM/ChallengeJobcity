using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StockBot.Sources
{
    public class DefaultCSVProvider : ICSVProvider
    {
        const string data = "Symbol,Date,Time,Open,High,Low,Close,Volume\nAAPL.US,2019-07-26,16:27:13,207.48,209.73,207.14,208.2594,5240858";
        public Task<byte[]> GetStockData(string code)
        {
            return Task.FromResult( System.Text.Encoding.UTF8.GetBytes(data));
        }
    }
}