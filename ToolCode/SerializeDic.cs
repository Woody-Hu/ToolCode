using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ToolCode
{
    [Serializable]
    public class SerializeDic<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        private const string m_useNodeName = "DicNode";

        private const string m_useKeyNodeName = "KeyNode";

        private const string m_useValueNodeName = "ValueNode";

        /// <summary>
        /// 普通构造
        /// </summary>
        public SerializeDic()
            : base()
        {

        }

        /// <summary>
        /// 键值比较器构造
        /// </summary>
        /// <param name="comparer"></param>
        public SerializeDic(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {

        }


        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 读取方法
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty) return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {

                reader.ReadStartElement(m_useNodeName);

                reader.ReadStartElement(m_useKeyNodeName);
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement(m_useValueNodeName);
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);
                reader.ReadEndElement();

                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// 写出方法
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement(m_useNodeName);

                writer.WriteStartElement(m_useKeyNodeName);
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement(m_useValueNodeName);
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }
}
