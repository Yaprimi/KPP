using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IDZ
{
    public static class ContainerSerializer
    {
        public static void SerializeToFile<T>(ProductContainerBase<T> container, string filePath) where T : IName<T>
        {
            using (var stream = File.Create(filePath))
            using (var writer = new BinaryWriter(stream))
            {
                string containerType = container.GetType().AssemblyQualifiedName;
                writer.Write(containerType);

                writer.Write(container.Count);

                foreach (var item in container)
                {
                    SerializeItem(writer, item);
                }
            }
        }

        public static ProductContainerBase<T> DeserializeFromFile<T>(string filePath) where T : IName<T>
        {
            using (var stream = File.OpenRead(filePath))
            using (var reader = new BinaryReader(stream))
            {
                string containerTypeName = reader.ReadString();
                Type containerType = Type.GetType(containerTypeName);
                if (containerType == null)
                    throw new InvalidOperationException($"Тип контейнера {containerTypeName} не знайдено.");

                var container = (ProductContainerBase<T>)Activator.CreateInstance(containerType);

                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    T item = DeserializeItem<T>(reader);
                    container.Add(item);
                }

                return container;
            }
        }

        private static void SerializeItem<T>(BinaryWriter writer, T item) where T : IName<T>
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            string typeName = item.GetType().AssemblyQualifiedName;
            writer.Write(typeName);

            var fields = item.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            var properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanWrite);

            writer.Write(fields.Length + properties.Count());

            foreach (var field in fields)
            {
                writer.Write(field.Name);
                WriteValue(writer, field.GetValue(item));
            }

            foreach (var prop in properties)
            {
                writer.Write(prop.Name);
                WriteValue(writer, prop.GetValue(item));
            }
        }

        private static T DeserializeItem<T>(BinaryReader reader) where T : IName<T>
        {
            string typeName = reader.ReadString();
            Type type = Type.GetType(typeName);
            if (type == null)
                throw new InvalidOperationException($"Тип {typeName} не знайдено.");

            object obj = Activator.CreateInstance(type);

            int memberCount = reader.ReadInt32();
            for (int i = 0; i < memberCount; i++)
            {
                string memberName = reader.ReadString();
                var field = type.GetField(memberName);
                if (field != null)
                {
                    object value = ReadValue(reader, field.FieldType);
                    field.SetValue(obj, value);
                }
                else
                {
                    var prop = type.GetProperty(memberName);
                    if (prop != null && prop.CanWrite)
                    {
                        object value = ReadValue(reader, prop.PropertyType);
                        prop.SetValue(obj, value);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Член {memberName} не знайдено.");
                    }
                }
            }

            return (T)obj;
        }

        private static void WriteValue(BinaryWriter writer, object value)
        {
            switch (value)
            {
                case int intVal:
                    writer.Write(intVal);
                    break;
                case decimal decimalVal:
                    writer.Write(decimalVal);
                    break;
                case string stringVal:
                    writer.Write(stringVal);
                    break;
                case bool boolVal:
                    writer.Write(boolVal);
                    break;
                default:
                    throw new NotSupportedException($"Тип {value?.GetType()} не підтримується.");
            }
        }

        private static object ReadValue(BinaryReader reader, Type type)
        {
            if (type == typeof(int)) return reader.ReadInt32();
            if (type == typeof(decimal)) return reader.ReadDecimal();
            if (type == typeof(string)) return reader.ReadString();
            if (type == typeof(bool)) return reader.ReadBoolean();
            throw new NotSupportedException($"Тип {type} не підтримується.");
        }
    }
}