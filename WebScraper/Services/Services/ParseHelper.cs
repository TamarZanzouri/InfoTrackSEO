using System;
using System.Collections.Generic;
using System.Linq;
using WebScraper.Data;
using WebScraper.Services.Interface;

namespace WebScraper.Services
{
    public class ParseHelper : IParseHelper
    {
        public string ParseHtmlTag(string section, string[] delimiter)
        {
            var startPos = section.IndexOf(delimiter.FirstOrDefault(), StringComparison.Ordinal) + delimiter.FirstOrDefault().Length;
            var length = section.IndexOf(delimiter.LastOrDefault(), StringComparison.Ordinal) - startPos;
            if (length > 0)
                return section.Substring(startPos, length);
            return section.Substring(startPos);
        }
    }
}
