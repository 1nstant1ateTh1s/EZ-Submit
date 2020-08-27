using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EZSubmitApp.Core.JsonConverters
{
    public abstract class DerivedTypeJsonConverter<TBase> : JsonConverter<TBase>
    {
        protected abstract string TypePropertyName { get; }
        protected abstract Dictionary<string, Type> TypeMap { get; }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TBase) == objectType;
        }

        public override TBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Check for null values
            if (reader.TokenType == JsonTokenType.Null) return default;

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            // Copy the starting state from Json reader (it's a struct) - we will be deserializing the entire JSON object from this copy, because looking up the formType 
            // is going to modify the state of the reader.
            var readerAtStart = reader;

            // Read the 'typeName' from our JSON document
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                jsonDocument.RootElement.TryGetProperty(TypePropertyName, out JsonElement typeNameElement);
                string typeName = (typeNameElement.ValueKind == JsonValueKind.String) 
                    ? typeNameElement.GetString() 
                    : null;

                // See if a Type has been specified for the given typeName string
                if (!string.IsNullOrWhiteSpace(typeName) && TypeMap.TryGetValue(typeName, out var targetType))
                {
                    // Deserialize the JSON to the type specified
                    try
                    { 
                        return (TBase)JsonSerializer.Deserialize(ref readerAtStart, targetType, options);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Invalid JSON in request.", ex);
                    }
                }

                throw new InvalidOperationException($"Missing or invalid value for {TypePropertyName} (base type {typeof(TBase).FullName}).");
            }
        }


        public override void Write(Utf8JsonWriter writer, TBase value, JsonSerializerOptions options)
        {
            // Create an ExpandoObject from the value to serialize so we can dynamically add a $type property to it
            ExpandoObject expando = ToExpandoObject(value);
            expando.TryAdd(TypePropertyName, TypeMap.FirstOrDefault(x => x.Value == value.GetType()).Key); // TODO: thoroughly test this method for retrieving TKey from dictionary based on TValue
            //expando.TryAdd(TypePropertyName, TypeToName(value.GetType()));

            // Serialize the expando
            JsonSerializer.Serialize(writer, expando, options);
        }


        private static ExpandoObject ToExpandoObject(object obj)
        {
            var expando = new ExpandoObject();
            if (obj != null)
            {
                // Copy all public properties
                foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead))
                {
                    expando.TryAdd(property.Name, property.GetValue(obj));
                }
            }

            return expando;
        }
    }
}
