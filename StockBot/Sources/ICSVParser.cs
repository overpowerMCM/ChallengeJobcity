using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.Sources
{
    /// <summary>
    /// CSV Parser Interface, this is used to parse the data fromn a cvs provider
    /// </summary>
    public interface ICSVParser
    {
        /// <summary>
        /// Parses the given data
        /// </summary>
        /// <param name="data">cvs as byte array</param>
        /// <returns>a collection of a list of stock value elements</returns>
        List<List<string>> Parse( byte[] data );
    }
}
