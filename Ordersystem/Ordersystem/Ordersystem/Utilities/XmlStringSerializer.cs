/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Xml.Serialization;
using System.IO;

namespace Ordersystem.Utilities
{
    class XmlStringSerializer<T>
    {
        public XmlStringSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        private XmlSerializer _serializer;

        public string Serialize(T client)
        {
            var writer = new StringWriter();
            _serializer.Serialize(writer, client);
            return writer.ToString();
        }

        public T Deserialize(string client)
        {
            var reader = new StringReader(client);
            return (T)_serializer.Deserialize(reader);
        }
    }
}