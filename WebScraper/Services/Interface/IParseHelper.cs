using System;
using System.Collections.Generic;
using WebScraper.Data;

namespace WebScraper.Services.Interface
{
    public interface IParseHelper
    {
        string ParseHtmlTag(string section, string[] delimiter);
    }
}
