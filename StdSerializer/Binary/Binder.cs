using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace StdSerializer.Binary
{
    internal sealed class Binder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            var exeAssembly = Assembly.GetExecutingAssembly().FullName;
            var typeToDeserialize = Type.GetType($"{typeName}, {exeAssembly}");

            return typeToDeserialize;
        }
    }
}