using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StockBot.Sources
{
    /// <summary>
    /// CSV data Provider interface
    /// </summary>
    public interface ICSVProvider
    {
        /// <summary>
        /// Retrieves the byte array of a given stock code.
        /// </summary>
        /// <param name="code">the stock code</param>
        /// <returns></returns>
        Task<byte[]> GetStockData(string code);
    }
}