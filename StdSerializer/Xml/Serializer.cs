using System.IO;
using System.Xml.Serialization;
using StdSerializer.Exceptions;

// ReSharper disable InvalidXmlDocComment

namespace StdSerializer.Xml
{
    /// <summary>
    /// Generic class for serializing and deserializing <see cref="object"/>s
    /// to- and -from binary form.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serializes an object to its binary representation
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        /// <param name="object">The object to serialize</param>
        /// <returns>The serialized object as an XML string</returns>
        /// <example>
        /// [...]
        ///     var xmlString = someObject.Serialize();
        /// [...]
        /// </example>
        public static string Serialize<T>(this T @object)
        {
            if (!typeof(T).IsSerializable)
                throw new StdSerializerException("Type not serializable");

            var xmlSerializer = new XmlSerializer(@object.GetType());
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, @object);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Deserialize an object that was previously serialized with <see cref="Serialize{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of the deserialized object</typeparam>
        /// <param name="xmlString">XML string containing the serialized object</param>
        /// <returns>If successful, the deserialized object a <typeparam name="T:T"></typeparam>/>/></returns>
        /// <example>
        /// [...]
        ///     var someObject = StdSerializer.Xml.Deserialize<typeof(SomeObject)>(xmlString);
        /// [...]
        /// </example>
        public static T Deserialize<T>(this string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textReader = new StringReader(xmlString))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }
    }
}
