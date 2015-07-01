using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SRSOO.Util.Extension;

namespace SRSOO.Util
{
    public class JSONHelper
    {
        /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string serialStr = JsonConvert.SerializeObject(t, Formatting.Indented, timeConverter);
            return serialStr;
            //return  JsonConvert.SerializeObject(t, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //jss.MaxJsonLength = Int32.MaxValue;
            //return jss.Serialize(t);
        }    

        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            return new JavaScriptSerializer().Deserialize<T>(strJson);
        }

        /// <summary>
        /// 反序列化json数组(此方法使用DataContractJsonSerializer)
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="json">json数组</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
            
            //T obj = Activator.CreateInstance<T>();
            //using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            //{
            //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //    return (T)serializer.ReadObject(ms);
            //}
        }

      /// <summary>
        /// 将db reader转换为Hashtable列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Hashtable> DbReaderToHash(IDataReader reader)
        {
            var list = new List<Hashtable>();
            while (reader.Read())
            {
                var item = new Hashtable();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var value = reader[i];
                    item[name] = value;
                }
                list.Add(item);
            }
            return list;
        }

        private static List<Hashtable> DataTableToHash(DataTable dt)
        {
            var list = new List<Hashtable>();           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new Hashtable();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var name = dt.Columns[j].ColumnName;
                    var value = dt.Rows[i][j];
                    item[name] = value;
                }
                list.Add(item);
            }
            return list;
        }

        public static string GetJsonForLigerGrid(DataTable dt, int total)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"Rows\":");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(":");

                    if (dt.Rows[i][j] == DBNull.Value)
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append("\"");
                    }
                    else if (dt.Rows[i][j] is DateTime)
                    {
                        jsonBuilder.Append(ToJson(dt.Rows[i][j]));
                    }
                    else
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(dt.Rows[i][j].ConvertToString().EscapeJson());
                        jsonBuilder.Append("\"");
                    }
                    if (j != dt.Columns.Count - 1)
                    {
                        jsonBuilder.Append(",");
                    }
                }
                jsonBuilder.Append("}");

                if (i != dt.Rows.Count - 1)
                {
                    jsonBuilder.Append(",");
                }
            }
            jsonBuilder.Append("], \"Total\":" + total.ConvertToString());
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        public static string GetJsonForLookup(DataTable dt, int total){
        
            string json = string.Empty;

            if ( dt.Rows.Count>0)
            {
                json = "{\"Success\": true, \"Count\": " + total.ConvertToString() + ", \"Message\": \"\", \"Rows\": [";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    json += "{";

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        json += "\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j].ConvertToString().Replace("\"", "\\\"") + "\"";

                        if (j != dt.Columns.Count - 1)
                        {
                            json += ", ";
                        }
                    }

                    json += "}";

                    if (i != dt.Rows.Count - 1)
                    {
                        json += ", ";
                    }
                }

                json += "]";
                json += "}";

            }
            else
            {
                json = "{\"Success\": true, \"Count\": 0, \"Message\": \"\"}";
            }

            return json;
        }

        /// <summary>
        /// 生成数据表JSON
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>JSON</returns>
        public static string GetJsonForDataTable(DataTable dt)
        {
            string json = string.Empty;

            if (dt != null)
            {
                json = "{ \"success\": true, ";
                json += "\"Count\": " + dt.Rows.Count.ToString() + ", ";
                json += "\"Rows\": [";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    json += "{";

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        json += "\"" + dt.Columns[j].ColumnName.ConvertToString() + "\": " +
                            "\"" + dt.Rows[i][j].ConvertToString() + "\"";

                        if (j != dt.Columns.Count - 1)
                        {
                            json += ", ";
                        }
                    }

                    json += "}";

                    if (i != dt.Rows.Count - 1)
                    {
                        json += ", ";
                    }
                }

                json += "]}";
            }
            else
            {
                json += "{\"success\": false}";
            }

            return json;
        }

        /// <summary>
        /// 生成异常JSON
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>JSON</returns>
        public static string GetJsonForException(Exception ex)
        {
            return "{\"success\": false, \"ErrMsg\": \"" + ex.Message.Replace("\"", "\\\"").Replace("\r", "\\\r").Replace("\n", "\\\\n") + "\"}";
        }

        public static string GetJsonForSomeReson(string reason)
        {
            return "{\"success\": false, \"ErrMsg\": \"" + reason + " \"}";
        }

        public static string GetJsonForUnknownReason()
        {
            return "{\"success\": false, \"ErrMsg\": \"未知原因\"}";
        }

        public static string GetJsonForSuccess()
        {
            return "{\"success\": true }";
        }

        public static string GetJsonForSuccessData(string data)
        {
            return "{\"success\":true, \"Data\":\"" + data + "\"}";
        }

        public static string GetJsonForSuccessData(object data)
        {
            return "{\"success\":true, \"Data\":\"" + ToJson(data) + "\"}";
        }

        public static string GetJsonForSuccessData(string data, string message)
        {
            return "{\"success\":true, \"Data\":\"" + data + "\", \"Message\":\"" + message + "\"}";
        }

        public static string GetJsonForSuccessDataArray(string dataArray)
        {
            return "{\"success\":true, \"Data\":" + dataArray + "}";
        }

    }
}
