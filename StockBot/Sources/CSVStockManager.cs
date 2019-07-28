using Microsoft.VisualBasic.FileIO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StockBot.Sources
{
    /// <summary>
    /// Manages the stock provider and parser
    /// </summary>
    public class CSVStockManager
    {

        protected ICSVProvider _csvProvider;
        protected ICSVParser _csvParser;

        /// <summary>
        /// the provider
        /// </summary>
        public ICSVProvider CSVProvider { get => _csvProvider ?? (_csvProvider = new DefaultCSVProvider()); set => _csvProvider = value; }

        /// <summary>
        /// the parser
        /// </summary>
        public ICSVParser CSVParser { get => _csvParser ?? (_csvParser = new DefaultCSVParser()); set => _csvParser = value; }

        /// <summary>
        /// retrives the parsed value collection of a given stock code
        /// </summary>
        /// <param name="code">the stock code</param>
        /// <returns>a collection of a list o stock value elements</returns>
        public async Task<List<List<string>>> GetParsedCSV(string code )
        {
            byte[] data = await CSVProvider.GetStockData(code);
            return CSVParser.Parse(data);
        }

    }
}