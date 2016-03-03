using System;
using MoFJsonEditor.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoFJsonEditor.Utility
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jsonObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class LootCreationConverter : JsonCreationConverter<Loot>
    {
        protected override Loot Create(Type objectType, JObject jsonObject)
        {
            string typeName = (jsonObject["Type"]).ToString();
            switch (typeName)
            {
                case "Equipment":
                    return new Equipment();
                case "Potion":
                    return new Potion();
                default:
                    return null;
            }
        }
    }
}
