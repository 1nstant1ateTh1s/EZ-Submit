using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EZSubmitApp.Core.JsonConverters
{
    public class CaseFormConverterWithTypeDiscriminator<T> : JsonConverter<T> where T : class, ICaseForm
    {
        private readonly Dictionary<string, Type> _typeMap;
        //private readonly Dictionary<string, Type> _typeMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        //                {
        //                    { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtForm) },
        //                    { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerForm) }
        //                };

        public CaseFormConverterWithTypeDiscriminator()
        {
            _typeMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                        {
                            { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtForm) },
                            { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerForm) }
                        };
        }

        public CaseFormConverterWithTypeDiscriminator(Dictionary<string, Type> typeMap)
        {
            _typeMap = typeMap;
        }

        /// <summary>
        /// Can the typeToConvert be (de)serialized by this JsonConverter?
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        public override bool CanConvert(Type typeToConvert)
        {
            // Any child type of CaseForm can be deserialized
            return typeof(T).IsAbstract && typeof(T).IsAssignableFrom(typeToConvert);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Check for null values
            if (reader.TokenType == JsonTokenType.Null) return null;

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            // Copy the starting state from Json reader (it's a struct) - we will be deserializing the entire JSON object from this copy, because looking up the formType 
            // is going to modify the state of the reader.
            var readerAtStart = reader;

            // Read the 'formType' from our JSON document
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDocument.RootElement;
                var formType = jsonObject.GetProperty("formType").GetString();

                // See if that class can be deserialized or not
                if (!string.IsNullOrEmpty(formType) && _typeMap.TryGetValue(formType, out var targetType))
                {
                    // Deserialize it
                    return JsonSerializer.Deserialize(ref readerAtStart, targetType, options) as T;

                    // Deserialize it.
                    //Don't pass in options when recursively calling Deserialize.
                    //return JsonSerializer.Deserialize(ref readerAtStart, targetType) as T;

                    // TODO: I COULD CHECK FOR REQUIRED FIELDS SET BY VALUES IN JSON BEFORE RETURNING THE CLASS OBJ.
                    // I.E.:
                    //var caseFormForCreationDto = JsonSerializer.Deserialize(ref readerAtStart, targetType) as CaseFormForCreationDto;
                    //if (caseFormForCreationDto.CaseNumber == default)
                    //{
                    //    throw new JsonException("Required property not received in the JSON");
                    //}
                    //return caseFormForCreationDto;
                }

                throw new JsonException($"{formType ?? "<unknown>"} can not be deserialized");
            }
        }

        //public override void Write(Utf8JsonWriter writer, CaseForm value, JsonSerializerOptions options)
        //{
        //    throw new NotImplementedException();
        //}

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
