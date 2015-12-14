/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Xml.Serialization;
using System.IO;

namespace Ordersystem.Utilities
{
	/// <summary>
	/// Xml string serializer.
	/// </summary>
    public class XmlStringSerializer<T>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ordersystem.Utilities.XmlStringSerializer`1"/> class.
		/// </summary>
        public XmlStringSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        private XmlSerializer _serializer;

		/// <summary>
		/// Serialize the specified client.
		/// </summary>
		/// <param name="client">Client.</param>
        public string Serialize(T client)
        {
            var writer = new StringWriter();
            _serializer.Serialize(writer, client);
            return writer.ToString().Replace('\n',' ');
        }

		/// <summary>
		/// Deserialize the specified client.
		/// </summary>
		/// <param name="client">Client.</param>
        public T Deserialize(string client)
        {
            var reader = new StringReader(client);
            return (T)_serializer.Deserialize(reader);
        }
    }
}