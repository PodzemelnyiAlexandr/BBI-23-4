using Classes;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.IO;

namespace Serializers
{
    public abstract class MySerializer
    {
        public abstract void Read(string fileName);
        public abstract void Write(FestivalCalendar calendar, string fileName);
    }

    public class MyJsonSerializer : MySerializer
    {
        public override void Read(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            List<Festival> festivals = JsonSerializer.Deserialize<List<Festival>>(jsonString, new JsonSerializerOptions
            {
                Converters = { new FestivalConverter() }
            });

            FestivalCalendar calendar = new FestivalCalendar();
            foreach (var festival in festivals)
            {
                calendar.AddFestival(festival);
            }
            calendar.ShowFestivals();
        }

        public override void Write(FestivalCalendar calendar, string fileName)
        {
            string jsonString = JsonSerializer.Serialize(calendar.Festivals, new JsonSerializerOptions
            {
                Converters = { new FestivalConverter() },
                WriteIndented = true
            });
            File.WriteAllText(fileName, jsonString);
        }
    }

    public class FestivalConverter : JsonConverter<Festival>
    {
        public override Festival Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement element = doc.RootElement;
                string type = element.GetProperty("Type").GetString();
                switch (type)
                {
                    case nameof(MusicFestival):
                        return JsonSerializer.Deserialize<MusicFestival>(element.GetRawText(), options);
                    case nameof(ComicsFestival):
                        return JsonSerializer.Deserialize<ComicsFestival>(element.GetRawText(), options);
                    case nameof(FoodFestival):
                        return JsonSerializer.Deserialize<FoodFestival>(element.GetRawText(), options);
                    default:
                        throw new NotSupportedException($"Unknown type: {type}");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Festival value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Type", value.GetType().Name);
            writer.WritePropertyName("Data");
            JsonSerializer.Serialize(writer, (object)value, options);
            writer.WriteEndObject();
        }
    }
}