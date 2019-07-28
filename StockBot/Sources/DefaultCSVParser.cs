using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StockBot.Sources
{
    public class DefaultCSVParser : ICSVParser
    {
        public List<List<string>> Parse(byte[] data)
        {
            List<List<string>> list = null;

            if (data != null)
            {
                MemoryStream stream = new MemoryStream(data);
                using (TextFieldParser parser = new TextFieldParser(stream))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        //Process row
                        if (list == null) list = new List<List<string>>();
                        List<string> element = new List<string>();
                        element.AddRange(parser.ReadFields());
                        list.Add(element);
                    }
                }
            }
            return list;
        }
    }
}