/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Xml.Serialization;
using System;
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
            string serializedClient;
            var writer = new StringWriter();
            _serializer.Serialize(writer, client);
            serializedClient = writer.ToString().Replace('\n', ' ');
            writer.Dispose();
            return serializedClient;
        }

        /// <summary>
        /// Deserialize the specified client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <exception cref="FormatException">Thrown when the format of the given client is invalid.</exception>
        public T Deserialize(string client)
        {
            T deserializedClient = default(T);
            var reader = new StringReader(client);
            try
            {
                deserializedClient = (T)_serializer.Deserialize(reader);
            }
            catch (InvalidOperationException e)
            {
                throw new FormatException("The format of the given client is invalid", e);
            }
            reader.Dispose();
            return deserializedClient;
        }
    }
}