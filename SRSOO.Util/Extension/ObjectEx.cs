using System;
using System.Collections.Specialized;

namespace SRSOO.Util.Extension
{
    public static class ObjectEx
    {

        public static int ConvertToIntBaseZero(this object o)
        {
            int intRtn = 0;

            if (o != null)
            {
                Int32.TryParse(o.ToString(), out intRtn);
            }

            return intRtn;
        }

        public static int ConvertToIntBaseNegativeOne(this object o)
        {
            int intRtn = -1;

            if (o != null && o.ToString() != string.Empty)
            {
                Int32.TryParse(o.ToString(), out intRtn);
            }

            return intRtn;
        }
        public static decimal ConvertToDecimalBaseMinValue(this object o)
        {
            decimal decRtn = decimal.MinValue;

            if (o == null || o == DBNull.Value)
            {
                decRtn = decimal.MinValue;
            }
            else
            {
                if (!decimal.TryParse(o.ToString(), out decRtn))
                {
                    decRtn = decimal.MinValue;
                }
            }

            return decRtn;
        }
        public static bool ConvertToBool(this object o)
        {
            bool bolRtn = false;

            try
            {
                if (o != null && o != DBNull.Value)
                {
                    string str = o.ToString().ToLower();

                    string[] trueStrings = new string[] { "true", "yes", "1", "√", "Y", "T", "是", "对" };

                    if (str.IsElementOfArray(trueStrings))
                    {

                        bolRtn = true;
                    }
                }
            }
            catch
            { 
            
            }

            return bolRtn;
        }

        public static string ConvertToString(this object o, string replaceString)
        {
            string strRtn = "";

            if (o == null || o == DBNull.Value)
            {
                strRtn = replaceString;
            }
            else
            {
                strRtn = o.ToString();
            }

            return strRtn;
        }

        public static decimal ConvertToDecimal(this object o)
        {
            decimal decRtn = 0m;

            if (o == null || o == DBNull.Value)
            {
                decRtn = 0m;
            }
            else
            {
                decimal.TryParse(o.ToString(), out decRtn);
            }

            return decRtn;
        }

        public static double ConvertToDouble(this object o)
        {
            double decRtn = 0d;

            if (o == null || o == DBNull.Value)
            {
                decRtn = 0d;
            }
            else
            {
                double.TryParse(o.ToString(), out decRtn);
            }

            return decRtn;
        }

        public static string ConvertToString(this object o)
        {
            return ConvertToString(o, string.Empty);
        }

        public static DateTime ConvertToDateTime(this object o)
        {
            DateTime dtRtn = DateTime.MinValue;

            try
            {
                if (o != null)
                {
                    DateTime.TryParse(o.ToString(), out dtRtn);
                }
            }
            catch
            { 
            
            }

            return dtRtn;
        }

        public static object ConvertToDataCellValue(this object o)
        {
            object objRtn = o;

            switch (o.GetType().ToString())
            { 
                case "System.String" :
                case "System.Char":
                    if (o.ToString().Trim() == string.Empty)
                        objRtn = DBNull.Value;
                    break;
                case "System.Int32" :
                case "System.Decimal":
                case "System.Int16":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                    if (o.ConvertToIntBaseNegativeOne() == -1)
                        objRtn = DBNull.Value;
                    break;
                case "System.DateTime" :
                    if (o.ConvertToDateTime() == DateTime.MinValue)
                        objRtn = DBNull.Value;
                    break;
                case "System.Boolean":
                    break;
                default:
                    break;
            }

            return objRtn;
        }

        public static string ConvertToDataCellValueInSQL(this object o)
        {
            string strRtn = string.Empty;

            switch (o.GetType().ToString())
            {
                case "System.String":
                case "System.Char":
                    if (o.ToString().Trim() == string.Empty)
                    {
                        strRtn = "NULL";
                    }
                    else
                    {
                        strRtn = "'" + o + "'";
                    }
                    break;
                case "System.Int32":
                case "System.Decimal":
                case "System.Int16":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                    if (o.ToString() == string.Empty || o.ConvertToIntBaseNegativeOne() == -1)
                    {
                        strRtn = "NULL";
                    }
                    else
                    {
                        strRtn = o.ToString();
                    }
                    break;
                case "System.DateTime":
                    if (o.ConvertToDateTime() == DateTime.MinValue)
                    {
                        strRtn = "NULL";
                    }
                    else
                    {
                        strRtn = "'" + o.ToString() + "'";
                    }
                    break;
                case "System.Boolean":
                    strRtn = Convert.ToBoolean(o) ? "1" : "0";
                    break;
                default:
                    if (o == null || o == DBNull.Value)
                    {
                        strRtn = "NULL";
                    }
                    else
                    {
                        strRtn = "'" + o.ToString() + "'";
                    }
                    break;
            }

            return strRtn;
        }

        #region 扩展用于对象的数据加载 byYMZ

        public static void Load(this object entity, NameValueCollection coll)
        {
            if (coll == null) return;
            foreach (string key in coll.AllKeys)
            {
                string propName = key;
                if (key.Contains("_val"))
                {
                    propName = key.RemoveSuffix("_val"); ;
                }
                var prop = entity.GetType().GetProperty(propName);
                if (prop != null)
                {
                    if (!string.IsNullOrEmpty(coll[key]))
                    {
                        var propValue = ConvertValue(prop.PropertyType, coll[key]);
                        prop.SetValue(entity, propValue, null);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(entity, null, null);
                    }
                }
            }
        }

        private static bool CheckStruct(Type type)
        {
            if (!type.IsValueType || type.IsEnum)
            {
                return false;
            }
            return (!type.IsPrimitive && !type.IsSerializable);
        }

        public static object ConvertValue(Type type, object value)
        {
            if (Convert.IsDBNull(value) || (value == null))
            {
                return null;
            }
            if (CheckStruct(type))
            {
                //string data = value.ToString();
                //return SerializationManager.Deserialize(type, data);
                throw new NotSupportedException("ConvertValue目前不支持非值类型！");
            }
            Type type2 = value.GetType();
            if (type == type2)
            {
                return value;
            }
            if (((type == typeof(Guid)) || (type == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }
                return new Guid(value.ToString());
            }
            if (((type == typeof(DateTime)) || (type == typeof(DateTime?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }
                return Convert.ToDateTime(value);
            }
            if (type.IsEnum)
            {
                try
                {
                    return Enum.Parse(type, value.ToString(), true);
                }
                catch
                {
                    return Enum.ToObject(type, value);
                }
            }
            if ((type == typeof(bool)) || (type == typeof(bool?)))
            {
                bool result = false;
                if (bool.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return false;
                }
                return true;
            }
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            return Convert.ChangeType(value, type);
        }
        
        #endregion
    }
}

