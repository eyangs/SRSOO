namespace KPMS.WebUtils.Handler
{
    using System;
    using System.Xml;
    using System.Web;

    public class XmlFile
    {
        private XmlDataDocument mydoc;
        private string strDataFileName = "";

        public XmlFile()
        { 
        
        }

        public XmlFile(string FileName)
        {
            this.strDataFileName = FileName;
            this.mydoc = new XmlDataDocument();
            this.mydoc.Load(this.strDataFileName);
            this.mydoc.DataSet.EnforceConstraints = false;
        }

        public void AddSubNode(string CurrentNodePath, string elementName, string elementValue, string AttributeName, string AttributeValue)
        {
            string xpath = CurrentNodePath;
            XmlNode node = this.mydoc.SelectSingleNode(xpath);
            XmlElement newChild = this.mydoc.CreateElement(elementName);
            newChild.InnerText = elementValue;
            XmlAttribute attribute = this.mydoc.CreateAttribute(AttributeName);
            attribute.Value = AttributeValue;
            newChild.Attributes.Append(attribute);
            node.AppendChild(newChild);
        }

        public void RemoveNode()
        {
        }

        public void Save()
        {
            this.mydoc.Save(this.strDataFileName);
        }

        public void SaveAs(string NewFileName)
        {
            this.mydoc.Save(NewFileName);
        }

        public void UpdateNode()
        {
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="Target">节点</param>
        /// <param name="Path">配置文件的路径</param>
        /// <returns></returns>
        public static string GetXMLValue(string Target, string Path)
        {
            try
            {
                string XmlPath = HttpContext.Current.Server.MapPath(Path);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(XmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNode node = root.SelectSingleNode(Target);
                if (node != null)
                {
                    return node.InnerXml;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <param name="strValue">新值</param>
        /// <param name="strSource">路径</param>
        public static void SaveXmlConfig(string strTarget, string strValue, string strSource)
        {
            string xmlPath = HttpContext.Current.Server.MapPath(strSource);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            elemList[0].InnerXml = strValue;
            xdoc.Save(xmlPath);
        }


        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="xpath"></param>
        /// <param name="value"></param>
        static public void setXmlInnerText(string filepath, string xpath, string value)
        {
            XmlDocument xmldoc = new XmlDocument();
            string physicsPath = HttpContext.Current.Server.MapPath(filepath);
            xmldoc.Load(physicsPath);
            XmlNode node = xmldoc.SelectSingleNode(xpath);
            if (node != null)
            {
                node.InnerText = value;
                xmldoc.Save(physicsPath);
            }
        }


    }
}

