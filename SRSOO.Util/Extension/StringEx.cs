using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SRSOO.Util.Extension
{
    public static class StringEx
    {
        /// <summary>
        /// 为防止SQL注入而清理字符串，用于清理用户输入值
        /// </summary>
        /// <param name="s">string itself</param>
        /// <returns>清理后的字符串</returns>
        public static string Clean(this string s)
        {
            s = s.Trim();
            s = s.Replace("'", "''");
            s = s.Replace(";--", "");
            s = s.Replace(" or ", "&nbsp;or;&nbsp;").Replace("&nbsp;and&nbsp;", string.Empty);
            s = s.Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;");
            return s;
        }

        /// <summary>
        /// 为存储支持Json的数据转义
        /// </summary>
        /// <param name="s">string itself</param>
        /// <returns>转移后的字符串</returns>
        public static string EscapeJson(this string s)
        {
            return s.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\"", "\\\"").Replace("\r", "   ").Replace("\t", "    ");
        }

        /// <summary>
        /// 将某个字符串重复连接在一起
        /// </summary>
        /// <param name="字符串"></param>
        /// <param name="字符串个数">重复次数</param>
        /// <returns>连接后字符串</returns>
        public static string CumulateChar(this string 字符串, int 字符串个数)
        {
            string str = "";
            for (int i = 0; i < 字符串个数; i++)
            {
                str = str + 字符串;
            }
            return str;
        }

        private static string forbiddenCharacters = "'=+-_)(&^%$#@!`[]{}";

        /// <summary>
        /// 判断字符串中是否存在被禁止的字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasSpecialCharacter(this string str)
        {
            bool flag = false;
            if (str == "")
            {
                return false;
            }
            char[] anyOf = forbiddenCharacters.ToCharArray();
            if (str.IndexOfAny(anyOf) > -1)
            {
                flag = true;
            }
            return flag;
        }


       

       

        /// <summary>
        /// 拆分字符串为数组
        /// </summary>
        /// <param name="源字符串"></param>
        /// <param name="分隔符"></param>
        /// <param name="去除前后空格"></param>
        /// <param name="格式化大写"></param>
        /// <param name="格式化小写"></param>
        /// <returns></returns>
        public static ArrayList GetArrayFromString(this string 源字符串, string 分隔符, bool 去除前后空格, bool 格式化大写, bool 格式化小写)
        {
            string str = "";
            int length = 0;
            ArrayList list = new ArrayList();
            源字符串.LastIndexOf(分隔符);
            if ((源字符串.LastIndexOf(分隔符) + 分隔符.Length) < 源字符串.Length)
            {
                源字符串 = 源字符串 + 分隔符;
            }
            for (length = 源字符串.IndexOf(分隔符); length >= 0; length = 源字符串.IndexOf(分隔符))
            {
                str = 源字符串.Substring(0, length);
                if (去除前后空格)
                {
                    str = str.Trim();
                }
                if (格式化大写)
                {
                    str = str.ToUpper();
                }
                if (格式化小写)
                {
                    str = str.ToLower();
                }
                list.Add(str);
                源字符串 = 源字符串.Remove(0, length + 分隔符.Length);
            }
            return list;
        }

        /// <summary>
        /// 获取字符bytes数
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static int GetLengthInBytes(this string Source)
        {
            return Encoding.Default.GetByteCount(Source);
        }
        
        #region Join
        /// <summary>
        /// Removes the last specified char.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="c">The c.</param>
        public static StringBuilder RemoveLastSpecifiedChar(this StringBuilder stringBuilder, char c)
        {
            if (stringBuilder.Length == 0)
                return stringBuilder;
            if (stringBuilder[stringBuilder.Length - 1] == c)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            return stringBuilder;
        }

        /// <summary>
        /// Joins the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string JoinString<T>(this IEnumerable<T> enumerable, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in enumerable)
            {
                stringBuilder.AppendFormat("{0},", item.ToString());
            }
            stringBuilder.RemoveLastSpecifiedChar(',');
            return stringBuilder.ToString();
        }
        #endregion

        #region Strip Tags
        /// <summary>
        /// 去除Html Xml 标记        
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static string StripHtmlXmlTags(this string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            return Regex.Replace(content, "<[^>]+>?", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// 去除所有的标记
        /// </summary>
        /// <param name="stringToStrip">The string to strip.</param>
        /// <returns></returns>
        public static string StripAllTags(this string stringToStrip)
        {
            if (string.IsNullOrEmpty(stringToStrip))
            {
                return stringToStrip;
            }
            // paring using RegEx
            //
            stringToStrip = Regex.Replace(stringToStrip, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "<br(?:\\s*)/>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = StripHtmlXmlTags(stringToStrip);

            return stringToStrip;
        }
        #endregion

        /// <summary>
        /// Truncates the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="wordsLimit">The words limit.</param>
        /// <returns></returns>
        public static string Truncate(this string s, int wordsLimit)
        {
            if (s.Length < wordsLimit)
            {
                return s;
            }
            return s.Substring(0, wordsLimit);
        }
        #region Cast type
        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="s">The s ,for example:"1,2,3" .</param>
        /// <param name="spliter">The spliter. for example:','</param>
        /// <returns></returns>
        public static IEnumerable<int> ToInt(this string s, params char[] spliter)
        {
            string[] strArr = s.Split(spliter);
            List<int> ints = new List<int>();
            foreach (var str in strArr)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    ints.Add(int.Parse(str));
                }
            }
            return ints;
        }

        /// <summary>
        /// Gets the nullable bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool? GetNullableBool(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return bool.Parse(value);
            }
            return null;
        }
        /// <summary>
        /// Gets the nullable int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int? GetNullableInt(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return int.Parse(value);
            }
            return null;
        }

        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid? GetNullableGuid(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return new Guid(value);
            }
            return null;
        }
        #endregion

        #region Trim
        /// <summary>
        /// Trims the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string TryTrim(this string s)
        {
            if (s != null)
            {
                return s.Trim();
            }
            return s;
        }
        #endregion

        /// <summary>
        /// Replaces to valid URL.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ReplaceToValidUrl(this string s)
        {
            string expression = @"[^\w|^\d|^\u4e00-\u9fa5]+";
            if (!string.IsNullOrEmpty(s))
            {
                return Regex.Replace(s, expression, "_", RegexOptions.Compiled | RegexOptions.Multiline);
            }
            return s;
        }

        /// <summary>
        /// Determines whether [is null or empty trim] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty trim] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmptyTrim(this string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim()))
            {
                return true;
            }
            return false;
        }

        ///// <summary>
        ///// Trims the specified s.
        ///// </summary>
        ///// <param name="s">The s.</param>
        //public static string Trim(string s)
        //{
        //    if (s == null)
        //    {
        //        return null;
        //    }
        //    return s.Trim();
        //}

        /// <summary>
        /// Appends the HTML line.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="s">The s.</param>
        public static void AppendHtmlLine(this StringBuilder stringBuilder, string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                stringBuilder.AppendFormat("{0}<br/>", s);
            }
        }


        public static bool IsTrue(this string s)
        {
            if (s.IsNullOrEmptyTrim())
            {
                return false;
            }
            s = s.ToLower();
            return s == "on" || s == "true";
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 判断字符串是否可以用以表示一个整型数值
        /// </summary>
        /// <param name="toTest">要测试的字符串本身</param>
        /// <returns>true：是；false：不是</returns>
        public static bool IsInteger(this string toTest)
        {
            bool bolRtn = false;

            try
            {
                Regex regex = new Regex("^([-]|[0-9])[0-9]*$");
                bolRtn = regex.IsMatch(toTest);
            }
            catch
            {

            }

            return bolRtn;
        }

        public static bool IsDateTime(this string s)
        {
            bool bolRtn = false;

            DateTime dt = DateTime.MinValue;

            bolRtn = DateTime.TryParse(s, out dt);

            return bolRtn;
        }

        /// <summary>
        /// 判断字符串是否可以用以表示一个带小数的实数
        /// </summary>
        /// <param name="toTest">要测试的字符串本身</param>
        /// <returns>true：是；false：不是</returns>
        public static bool IsReal(this string toTest)
        {
            bool bolRtn = false;

            try
            {
                Regex regex = new Regex("^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$");
                bolRtn = regex.IsMatch(toTest);
            }
            catch
            {

            }

            return bolRtn;
        }


        /// <summary>
        /// 判断一个带千分位的数字字符串是否真正是一个数字
        /// </summary>
        /// <param name="toTest">要测试的字符串本身</param>
        /// <returns>true：是；false：不是</returns>
        public static bool IsDecimalWithThousands(this string toTest)
        {
            bool bolRtn = false;

            try
            {
                Regex regex = new Regex(@"^([-])(\d{1,3},(\d{3},)*\d{3}(\.\d{1,4})?|\d*(\.\d{1,4})?)$");
                bolRtn = regex.IsMatch(toTest);
            }
            catch
            {

            }

            return bolRtn;
        }

        /// <summary>
        /// 移除表达式字符串中可能出现的和运算符相关的特殊字符
        /// </summary>
        /// <param name="toRemove">要移除特殊字符的字符串</param>
        /// <returns>移除特殊字符串后的字符串</returns>
        public static string RemoveOperators(this string toRemove)
        {
            string strRtn = toRemove;

            string strOperators = "#\'()*+/<=> ";

            for (int i = 0; i < strOperators.Length; i++)
            {
                strRtn = strRtn.Replace(strOperators.Substring(i, 1), string.Empty);
            }

            return strRtn;
        }

        /// <summary>
        /// 判断一个字符串是否是一个集合的项目之一
        /// </summary>
        /// <param name="str">string itself</param>
        /// <param name="collection">以特定字符分割的集合字符串</param>
        /// <param name="separator">分割字符</param>
        /// <returns></returns>
        public static bool IsItemOfCollection(this string str, string collection, string separator)
        {
            bool bolRtn = false;

            string[] collectionItems = collection.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < collectionItems.Length; i++)
            {
                if (str == collectionItems[i])
                {
                    bolRtn = true;
                    break;
                }
            }

            return bolRtn;
        }

        /// <summary>
        /// 判断字符串是否指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">string itself</param>
        /// <param name="array">指定的数组</param>
        /// <returns>true：是；false：否。</returns>
        public static bool IsElementOfArray(this string str, string[] array)
        {
            bool bolRtn = false;

            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == str)
                    {
                        bolRtn = true;
                        break;
                    }
                }
            }

            return bolRtn;
        }


        /// <summary>
        /// 判断字符串是否和指定数组中某些元素相像
        /// </summary>
        /// <param name="str">string itself</param>
        /// <param name="array">数组</param>
        /// <param name="selfLonger">待比较的字符串是否比数组中的元素更长</param>
        /// <returns>是否相像</returns>
        public static bool MatchElementOfArray(this string str, string[] array, bool selfLonger)
        {
            bool bolRtn = false;

            if (selfLonger)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (str.IndexOf(array[i].ConvertToString()) > -1)
                    {
                        bolRtn = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].ConvertToString().IndexOf(str) > -1)
                    {
                        bolRtn = true;
                        break;
                    }
                }
            }

            return bolRtn;
        }

        /// <summary>
        /// 增加换行
        /// </summary>
        /// <param name="str">string itself</param>
        /// <returns>增加换行后的字符串</returns>
        public static string AddBreakLine(this string str)
        {
            string rtn = string.Empty;

            rtn = str + @"

";

            return rtn;
        }

        /// <summary>
        /// 移除特定后缀
        /// </summary>
        /// <param name="str">string itself</param>
        /// <param name="suffix">指定的后缀</param>
        /// <returns></returns>
        public static string RemoveSuffix(this string str, string suffix)
        {
            if (str.EndsWith(suffix))
            {
                str = str.Substring(0, str.Length - suffix.Length);
            }

            return str;
        }

        /// <summary>
        /// 获取使用指定字符串截取某个长字符串后的第指定个截断字符串部分
        /// </summary>
        /// <param name="longString">长字符串</param>
        /// <param name="sperator">用于截取的字符串</param>
        /// <param name="order">顺序</param>
        /// <returns>截断字符串</returns>
        public static string SplittedStringInSpecificOrder(this string longString, string sperator, int order)
        {
            string strRtn = string.Empty;

            string[] strs = longString.Split(new string[] { sperator }, StringSplitOptions.RemoveEmptyEntries);

            if (strs.Length >= order)
            {
                strRtn = strs[order - 1];
            }

            strs = null;

            return strRtn;
        }

        /// <summary>
        /// 替换第一个出现的短字符串
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <param name="oldString">要替换的字符串</param>
        /// <param name="newString">替换为的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceFirst(this string originalString, string oldString, string newString)
        {
            Regex reg = new Regex(oldString);

            if (reg.IsMatch(originalString))
            {
                originalString = reg.Replace(originalString, newString, 1);
            }

            return originalString;
        }

        /// <summary>
        /// 构造Where子句中In表达式
        /// </summary>
        /// <param name="s">原始字符串</param>
        /// <param name="sperator">分割字符</param>
        /// <param name="elementType">元素类型：Numeric/String</param>
        /// <returns>构造后的表达式</returns>
        public static string BuildWhereClauseInList(this string s, string separator, string elementType)
        {
            string strRtn = string.Empty;

            if (s.Trim() != string.Empty)
            {
                string[] sArr = s.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < sArr.Length; i++)
                {
                    if (elementType == "String")
                    {
                        strRtn += "'";
                    }

                    strRtn += sArr[i];

                    if (elementType == "String")
                    {
                        strRtn += "'";
                    }

                    if (i != sArr.Length - 1)
                    {
                        strRtn += ", ";
                    }
                }
            }

            return strRtn;
        }

        /// <summary>
        /// 替换文本中的空格和换行
        /// </summary>
        public static string ReplaceSpace(this string str)
        {
            string s = str;
            s = s.Replace(" ", "&nbsp;");
            s = s.Replace("\n", "<BR />");
            return s;
        }


        internal static ArrayList SeparateSQL(this string strSQL)
        {
            ArrayList list = new ArrayList();
            string[] strArray = strSQL.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Trim().ToUpper() == "GO")
                {
                    if (builder.Length > 0)
                    {
                        list.Add(builder.ToString());
                    }
                    builder.Remove(0, builder.Length);
                }
                else
                {
                    builder.AppendLine(strArray[i]);
                }
            }
            if (builder.ToString() != "")
            {
                list.Add(builder.ToString());
            }
            return list;
        }


        /// <summary>
        /// 将字符转化为HTML编码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回编译后的字符串</returns>
        public static string HtmlEncode(this string Input)
        {
            return HttpContext.Current.Server.HtmlEncode(Input);
        }


        /// <summary>
        /// 将字符HTML解码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string HtmlDecode(this string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            else
                return HttpContext.Current.Server.HtmlDecode(Input);
        }


        /// <summary>
        /// URL地址编码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返编码后的字符串</returns>
        public static string URLEncode(this string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            else
                return HttpContext.Current.Server.UrlEncode(Input);
        }


        /// <summary>
        /// URL地址解码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string URLDecode(this string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                return HttpContext.Current.Server.UrlDecode(Input);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 判断是否是电子邮件
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>返回true或false</returns>
        public static bool IsEmail(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }
            return Regex.IsMatch(strIn, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        /// <summary>
        /// 判断是否是国内手机号码,前面不加0
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>返回true或false</returns>
        public static bool IsMobile(this string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            string regu = "^[1][3,5,8][0-9]{9}$";

            if (Regex.Match(strIn, regu, RegexOptions.Compiled).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 过滤字符串中的html代码
        /// </summary>
        /// <param name="Str">传入字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string LostHTML(this string Str)
        {
            string Re_Str = "";

            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "<\\/*[^<>]*>";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }

            return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
        }






        /// <summary>
        /// 替换字符串中的脚本标记
        /// </summary>
        public static string ReplaceScriptTags(this string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("</script", "&lt;/script");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 替换HTML中的换行符和常用字符实体
        /// </summary>
        public static String ReplaceBrsAndCharEntitiesForText(this string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }



        /// <summary>
        /// 替换HTML中的常见字符实体
        /// </summary>
        public static String ReplaceCommonCharEntitiesForText(this string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            return sb.ToString();
        }



        /// <summary>
        /// 取出常用标签及字符实体
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string RemoveCommonTagsAndCharEntitiesForText(this string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("<font", " ");
            sb.Replace("<span", " ");
            sb.Replace("<style", " ");
            sb.Replace("<div", " ");
            sb.Replace("<p", "");
            sb.Replace("</p>", "");
            sb.Replace("<label", " ");
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "");
            sb.Replace("<br />", "");
            sb.Replace("<br />", "");
            sb.Replace("&lt;", "");
            sb.Replace("&gt;", "");
            sb.Replace("&amp;", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            return sb.ToString();
        }


        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ReplaceEscapeCharsForHtml(this string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                StringBuilder sb = new StringBuilder(Input);
                sb.Replace("&", "&amp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\r\n", "<br />");
                sb.Replace("\n", "<br />");
                sb.Replace("\t", " ");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 去除script
        /// </summary>
        public static string LoseScript(this string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            return html;
        }

        /// <summary>
        /// 去除iframe
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string LoseIframe(this string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            return html;
        }


        /// <summary>
        /// 去除除IMG以外的HTML
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string LoseExceptImg(this string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<(?!/?img)[\s\S]*?>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex.Replace(html, "");
            return html;
        }


        /// <summary>
        /// 去除多余的逗号
        /// </summary>
        public static string FixCommaStr(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string[] arr = str.Split(',');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Trim() != "")
                    {
                        sb.Append(arr[i]);
                        sb.Append(",");
                    }
                }
                string sbstr = sb.ToString();
                if (!string.IsNullOrEmpty(sbstr))
                {
                    sbstr = sbstr.Substring(0, sbstr.Length - 1);
                }
                return sbstr;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 过滤和T-SQL语法相关的字符字符
        /// </summary>
        public static string Filter(this string sInput)
        {
            if (sInput == null || sInput.Trim() == string.Empty)
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        public static string WrapValueForField(this string value, string type)
        {
            string rtn = string.Empty;

            switch (type)
            {
                case "文本":
                    rtn = "'" + value + "'";
                    break;
                case "数字":
                    rtn = value;
                    break;
                case "日期":
                    rtn = "'" + value + "'";
                    break;
                case "是否":
                    rtn = (value.ConvertToBool() ? "1" : "0");
                    break;
                default:
                    break;
            }

            return rtn;
        }

        /// <summary>
        /// 将一个较长的字符串拆分成多个部分，每个部分的长度为partLength，每个部分中间使用 separator 分割，最后将所有拆分出来的部分再合并为一个字符串返回
        /// </summary>
        /// <param name="str">较长的字符串</param>
        /// <param name="partLength">每个部分的长度</param>
        /// <param name="separator">分割字符</param>
        /// <returns></returns>
        public static string Divide(this string str, int partLength, string separator)
        {
            string rtn = string.Empty;

            int times = 0;

            while (times * partLength < str.Length)
            {
                int left = str.Length - times * partLength;

                rtn += str.Substring(times * partLength, left >= partLength ? partLength : left);

                rtn += left > partLength ? separator : string.Empty;

                times++;
            }

            return rtn;
        }


        /// <summary>
        /// 以不超过限定的长度来拆分字符串形成数组
        ///     如 s1=123&s2=456&s3=789，限定长度 7 使用分隔符 & 拆分出来的数组元素是 s1=123, s2=456, s3=789
        ///     如 s1=123&s2=456&s3=789，限定长度 15 使用分隔符 & 拆分出来的数组元素是 s1=123&s2=456, s3=789
        /// </summary>
        /// <param name="str">要拆分的字符串</param>
        /// <param name="length">要限定每个数组的最大长度</param>
        /// <param name="arrayLength">数组元素个数</param>
        /// <param name="separator">分隔符</param>
        /// <returns>拆分后的数组</returns>
        public static string[] SliceWithLimitLength(this string str, int length, int arrayLength, string separator)
        {
            string[] arr = new string[arrayLength];

            if (str.Length < length)
            {
                arr[0] = str;
            }
            else
            {
                int loop = 0;

                while (str.Length > length && loop < arrayLength)
                {
                    string temp = str.Substring(0, length);

                    temp = temp.Substring(0, temp.LastIndexOf(separator));

                    arr[loop] = temp;

                    str = str.Substring(temp.Length + 1);

                    loop++;
                }

                arr[loop] = str;
            }

            return arr;
        }


        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(instance, args);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool EqualsTo(this string str, string to, bool ignoreCase)
        {
            return (string.Compare(str, to, ignoreCase) == 0);
        }

        public static int ConvertToExcelColumnIndex(this string columnName)
        {
            if (!Regex.IsMatch(columnName.ToUpper(), @"[A-Z]+")) { throw new Exception("非法列名称，无法转换！"); }

            int index = 0;

            char[] chars = columnName.ToUpper().ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }

            return index - 1;
        }

        #region 验证相关


        public static bool ValidateAsIDCard(this string s, out string err)
        {
            bool succeed = true;
            err = string.Empty;

            if (s.Length != 18 && s.Length != 15)
            {
                succeed = false;
                err = "身份证号码必须是15位或者18位";
            }
            else
            {
                if (s.Length == 18)
                {
                    succeed = CheckIDCard18(s, out err);
                }

                if (s.Length == 15)
                {
                    succeed = CheckIDCard15(s, out err);
                }
            }

            return succeed;
        }


        private static bool CheckIDCard18(string Id, out string err)
        {
            err = string.Empty;
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                err = "18位身份证号码除最后一位可以是X外其它17为均应为数字";
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                err = "身份证号码不能通过省份代码验证";
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                err = "身份证号码不能通过生日验证";
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                err = "身份证号码不能通过校验码验证";
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        private static bool CheckIDCard15(string Id, out string err)
        {
            err = string.Empty;
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                err = "15位身份证号码的每一位均应为数字";
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                err = "身份证号码不能通过省份代码验证";
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                err = "身份证号码不能通过生日验证";
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }


        #endregion

    }
}
