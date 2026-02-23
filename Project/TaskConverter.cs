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
                if (!root.TryGetProperty("TaskType", out JsonElement taskTypeElem))
                    throw new JsonException("Missing TaskType property.");

                string? taskType = taskTypeElem.GetString();
                if (string.IsNullOrWhiteSpace(taskType))
                    throw new JsonException("TaskType is null or empty.");

                return taskType switch
                {
                    "Work" => JsonSerializer.Deserialize<WorkTask>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize WorkTask."),
                    "Personal" => JsonSerializer.Deserialize<PersonalTask>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize PersonalTask."),
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
