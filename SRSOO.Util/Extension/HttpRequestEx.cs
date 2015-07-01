using System;
using System.Web;

namespace SRSOO.Util.Extension
{
    public static class HttpRequestEx
    {
        /// <summary>
        /// 将Request对象的InputStream读取为一个字符串
        /// </summary>
        /// <param name="hr">HttpRequest itself</param>
        /// <returns>InputStream中包含的字符串</returns>
        public static string ReadStringInInputStream(HttpRequest hr)
        {
            string rtn = string.Empty;

            System.IO.Stream body = hr.InputStream;

            body.Position = 0;      //很多代码示例无法获取参数值的原因就是因为少了这一句，cumtzzp@163.com

            System.Text.Encoding encoding = hr.ContentEncoding;

            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding); //此对象不能Dispose掉，否则无法再次获取值

            rtn = reader.ReadToEnd();

            return rtn;
        }

        /// <summary>
        /// 从InputStream中读取指定key对应的字符串值，仅限于请求数据是JSON对象格式的字符串时使用
        /// </summary>
        /// <param name="hr">HttpRequest itself</param>
        /// <param name="key">键/名称</param>
        /// <returns>字符串值</returns>
        public static string GetStringValueInInputStreamWithJsonFormat(this HttpRequest hr, string key)
        {
            string s = string.Empty;

            if (hr != null && hr.InputStream != null)
            {
                s = ReadStringInInputStream(hr).Trim();

                if (s != string.Empty && s.StartsWith("{") && s.EndsWith("}") && s.IndexOf(key) >= 0)
                {
                    s = s.Replace("{", string.Empty).Replace("}", string.Empty);

                    string[] arr = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string a in arr)
                    {
                        string name = a.Substring(0, a.IndexOf(":")).Trim();

                        if (name == key)
                        {
                            s = a.Substring(a.IndexOf(":") + 1).Replace("\"", string.Empty).Replace("'", string.Empty).Trim();
                            break;
                        }
                    }
                }
                else
                {
                    s = string.Empty;
                }
            }

            return s;
        }

        /// <summary>
        /// 从InputStream中读取指定key对应的整形值，仅限于请求数据是JSON对象格式的字符串时使用
        /// </summary>
        /// <param name="hr">HttpRequest itself</param>
        /// <param name="key">键/名称</param>
        /// <returns>字符串值</returns>
        public static int GetIntValueInInputStreamWithJsonFormat(this HttpRequest hr, string key)
        {
            return hr.GetStringValueInInputStreamWithJsonFormat(key).ConvertToIntBaseNegativeOne();
        }

        /// <summary>
        /// 从InputStream中读取指定key对应的日期时间值，仅限于请求数据是JSON对象格式的字符串时使用
        /// </summary>
        /// <param name="hr">HttpRequest itself</param>
        /// <param name="key">键/名称</param>
        /// <returns>字符串值</returns>
        public static DateTime GetDateTimeValueInInputStreamWithJsonFormat(this HttpRequest hr, string key)
        {
            return hr.GetStringValueInInputStreamWithJsonFormat(key).ConvertToDateTime();
        }


        public static int GetIntValueInQueryString(this HttpRequest hr, string key)
        {
            return hr.QueryString[key].ConvertToIntBaseNegativeOne();
        }

        public static string GetStringValueInQueryString(this HttpRequest hr, string key)
        {
            return hr.QueryString[key].ConvertToString();
        }

        public static DateTime GetDateTimeValueInQueryString(this HttpRequest hr, string key)
        {
            return hr.QueryString[key].ConvertToDateTime();
        }

        public static int GetIntValueInForm(this HttpRequest hr, string key)
        {
            return hr.Form[key].ConvertToIntBaseNegativeOne();
        }

        public static string GetStringValueInForm(this HttpRequest hr, string key)
        {
            return hr.Form[key].ConvertToString();
        }

        public static DateTime GetDateTimeValueInForm(this HttpRequest hr, string key)
        {
            return hr.Form[key].ConvertToDateTime();
        }

        public static int GetIntValueInCookies(this HttpRequest hr, string key)
        {
            return hr.Cookies[key].ConvertToIntBaseNegativeOne();
        }

        public static string GetStringValueInCookies(this HttpRequest hr, string key)
        {
            return hr.Cookies[key].ConvertToString();
        }

        public static DateTime GetDateTimeValueInCookies(this HttpRequest hr, string key)
        {
            return hr.Cookies[key].ConvertToDateTime();
        }

        public static bool CheckFormValue(this HttpRequest request, string parameterName)
        {
            if (
                    request != null
                    && request.Form != null
                    && request.Form.Count > 0
                    && request.Form[parameterName] != null
                    && request.Form[parameterName].Trim() != string.Empty
                )
            {
                return true;
            }

            return false;
        }

        
        
    }
}
