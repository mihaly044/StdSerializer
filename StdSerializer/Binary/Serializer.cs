using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using StdSerializer.Exceptions;

// ReSharper disable InvalidXmlDocComment

namespace StdSerializer.Binary
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
        /// <returns>The serialized object as a byte array</returns>
        /// <example>
        /// [...]
        ///     var bytes = someObject.Serialize();
        /// [...]
        /// </example>
        public static byte[] Serialize<T>(this T @object)
        {
            if(!typeof(T).IsSerializable)
                throw new StdSerializerException("Type not serializable");

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter
                {
                    Binder = new Binder()
                }.Serialize(ms, @object);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Deserialize an object that was previously serialized with <see cref="Serialize{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of the deserialized object</typeparam>
        /// <param name="byteArray">Byte array containing the serialized object</param>
        /// <returns>If successful, the deserialized object as a <typeparam name="T:T"></typeparam>/>/></returns>
        /// <example>
        /// [...]
        ///     var someObject = StdSerializer.Binary.Deserialize<typeof(SomeObject)>(byteArray);
        /// [...]
        /// </example>
        public static T Deserialize<T>(this byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return (T)new BinaryFormatter
                {
                    Binder = new Binder()
                }.Deserialize(ms);
            }
        }
    }
}
