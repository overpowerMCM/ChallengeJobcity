using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace StockBot.Sources
{
    /// <summary>
    /// Stooq stock provider: https://stooq.com
    /// </summary>
    public class StooqCSVProvider : ICSVProvider
    {
        public async Task<byte[]> GetStockData(string stockCode)
        {
            using (var client = new WebClient())
            {
                Uri uri = new Uri(string.Format("https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv​", stockCode));
                var buffer = await client.DownloadDataTaskAsync(uri);
                return buffer;
            }
        }
    }
}