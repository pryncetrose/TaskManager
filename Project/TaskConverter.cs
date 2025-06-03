using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskManagementApp
{
    public class TaskConverter : JsonConverter<BaseTask>
    {
        public override BaseTask Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                string taskType = root.GetProperty("TaskType").GetString();

                return taskType switch
                {
                    "Work" => JsonSerializer.Deserialize<WorkTask>(root.GetRawText(), options),
                    "Personal" => JsonSerializer.Deserialize<PersonalTask>(root.GetRawText(), options),
                    _ => throw new NotSupportedException($"Unsupported task type: {taskType}")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, BaseTask value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
