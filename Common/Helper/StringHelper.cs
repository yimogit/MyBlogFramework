using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
   public static  class StringHelper
    {
       public static string ReplaceHtmlTag(string html, int length = 0)
       {
           if (string.IsNullOrEmpty(html))
               return "";
           string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
           strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

           if (length > 0 && strText.Length > length)
               return strText.Substring(0, length);

           return strText;
       }
    }
}
