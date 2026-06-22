using System.Text.RegularExpressions;

namespace SelfFinanceApp.Common
{
    public static class StringExtension
    {
        public static string DeletePartNameTypeEntity(this string strName, string deletePartStr)
        {
            string tempStr = strName;
            tempStr = Regex.Replace(tempStr, @$"{deletePartStr}$", "");

            return tempStr;
        }

        public static string LowerFirstChar(this string str)
        {
            string tempStr = str;
            char firstChar = str[0];
            tempStr = Regex.Replace(tempStr, @"^\w{1}", firstChar.ToString().ToLower());

            return tempStr;
        }
    }
}
