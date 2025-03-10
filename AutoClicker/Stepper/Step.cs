using System.Collections.Generic;
using System.Windows.Input;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.Linq;

namespace AutoClicker.Stepper
{
    public class Step
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Label { get; set; }
        public StepType Type { get; set; }
        public string Value { get; set; }
        public List<Step> Childs { get; set; }
        
        public StepConditionType? ConditionType { get; set; }
        public string ConditionValue { get; set; }
        public bool? Loop { get; set; }
    }

    public class StepPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum StepType
    {
        Wait = 0,
        Condition,
        Increment,

        MouseRightClick,
        MouseLeftClick,
        MouseMove,

        //KeyboardType,
        KeyboardPress,
        WriteCounter,
        //KeyboardRelease,
        //KeyboardHold,
        If,
        Else,
        Loop,
        While,
        WhileNot,

        ScreenCapture,
        ActivateWindow
    }

    public enum StepConditionType
    {
        FocusedWindowTitle,
        PixelColor,
    }

    public class StepKeyPress
    {
        public Key Key { get; set; }
        public ModifierKeys Modifiers { get; set; }
    }

    public class StepJsonConverter : JsonConverter<Step>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Step).IsAssignableFrom(typeToConvert);
        }

        public override Step Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;
                var jsonString = root.GetRawText();

                var newOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                newOptions.Converters.Add(new JsonStringEnumConverter());
                return JsonSerializer.Deserialize<Step>(jsonString, newOptions);
            }
        }

        public override void Write(Utf8JsonWriter writer, Step value, JsonSerializerOptions options)
        {
            var newOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            
            newOptions.Converters.Add(new JsonStringEnumConverter());

            var jsonString = JsonSerializer.Serialize(value, value.GetType(), newOptions);
            using (var document = JsonDocument.Parse(jsonString))
            {
                    document.RootElement.WriteTo(writer);
            }
        }
    }
}
