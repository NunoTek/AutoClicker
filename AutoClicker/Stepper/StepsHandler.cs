using AutoClicker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Linq;
using System.Windows.Input;

namespace AutoClicker.Stepper
{
    public class StepsHandler
    {
        private Bitmap _screenCapture;
        public Bitmap ScreenCapture
        {
            get => _screenCapture;
            set
            {
                if (_screenCapture != value)
                {
                    _screenCapture = value;
                    OnPropertyChanged(nameof(ScreenCapture));
                }
            }
        }

        private int _counter;
        public int Counter
        {
            get => _counter;
            set
            {
                if (_counter != value)
                {
                    _counter = value;
                    OnPropertyChanged(nameof(Counter));
                }
            }
        }

        private int _writeCounter;
        public int WriteCounter
        {
            get => _writeCounter;
            set
            {
                if (_writeCounter != value)
                {
                    _writeCounter = value;
                    OnPropertyChanged(nameof(WriteCounter));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        


        public async Task HandleStepsAsync(List<Step> steps, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var step in steps)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    await HandleStepAsync(step, cancellationToken);

                    Task.Delay(100).Wait();

                    if (step.Childs != null && step.Childs.Count > 0 && step.Type != StepType.Condition)
                    {
                        await HandleStepsAsync(step.Childs, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task HandleStepAsync(Step step, CancellationToken ct)
        {
            Counter++;
            Logger.Log($"Executing step {Counter}: {step.Label} (Type: {step.Type})");

            try 
            {
                switch (step.Type)
                {
                    case StepType.Wait:
                        var delay = int.Parse(step.Value);
                        Logger.Log($"Waiting for {delay} milliseconds");
                        await Task.Delay(delay, ct);
                        break;

                    case StepType.MouseMove:
                        var movePos = DeserializeJson<StepPosition>(step.Value);
                        Logger.Log($"Moving mouse to ({movePos.X}, {movePos.Y})");
                        MouseHelper.MoveMouseTo(movePos.X, movePos.Y);
                        break;

                    case StepType.MouseLeftClick:
                        var leftClickPos = DeserializeJson<StepPosition>(step.Value);
                        Logger.Log($"Left click at position ({leftClickPos.X}, {leftClickPos.Y})");
                        MouseHelper.ClickAt(leftClickPos.X, leftClickPos.Y, true);
                        break;

                    case StepType.MouseRightClick:
                        var rightClickPos = DeserializeJson<StepPosition>(step.Value);
                        Logger.Log($"Right click at position ({rightClickPos.X}, {rightClickPos.Y})");
                        MouseHelper.ClickAt(rightClickPos.X, rightClickPos.Y, false);
                        break;

                    case StepType.KeyboardPress:
                        var keyPress = DeserializeJson<StepKeyPress>(step.Value);
                        Logger.Log($"Pressing key {keyPress.Key} {(keyPress.Modifiers != ModifierKeys.None ? $"+ {keyPress.Modifiers}" : "")}");
                        KeyboardHelper.SimulateKeyPress(keyPress.Key, keyPress.Modifiers);
                        break;

                    case StepType.ActivateWindow:
                        Logger.Log($"Activating window '{step.Value}'");
                        ProcessHelper.ActivateWindowByTitle(step.Value);
                        break;

                    case StepType.Increment:
                        Logger.Log("Incrementing counter");
                        WriteCounter++;
                        break;

                    case StepType.ScreenCapture:
                        ScreenCapture = ScreenHelper.TakeScreenshot();
                        break;

                    case StepType.Condition:
                        bool conditionResult = ConditionHandler(step);

                        if (step.Childs != null)
                        {
                            foreach (var child in step.Childs) {
                                if (child.Type == StepType.If) {
                                    if (conditionResult) {
                                        await HandleStepsAsync(child.Childs, ct);
                                    }
                                }
                                else if (child.Type == StepType.Else) {
                                    if (!conditionResult) {
                                        await HandleStepsAsync(child.Childs, ct);
                                    }
                                }
                                else if (child.Type == StepType.While) {
                                    while (conditionResult) {
                                        await HandleStepsAsync(child.Childs, ct);
                                        conditionResult = ConditionHandler(step);
                                    } 

                                }
                                else if (child.Type == StepType.WhileNot) {
                                    while (!conditionResult) {
                                        await HandleStepsAsync(child.Childs, ct);
                                        conditionResult = ConditionHandler(step);
                                    }
                                }
                                else {
                                    await HandleStepsAsync(new List<Step> { child }, ct);
                                }
                            }
                        }
                        break;

                    case StepType.Loop:
                        if (step?.Childs != null)
                        {
                            int iterations = int.Parse(step.Value);
                            for (int i = 0; i < iterations && !ct.IsCancellationRequested; i++)
                            {
                                await HandleStepsAsync(step.Childs, ct);
                            }
                        }
                        break;

                    case StepType.WriteCounter:
                        string writeCounterValue = WriteCounter.ToString();
                        foreach (char c in writeCounterValue)
                        {
                            Key numKey = GetNumericKey(c);
                            KeyboardHelper.SimulateKeyPress(numKey, ModifierKeys.None);
                            await Task.Delay(50);
                        }
                        break;

                    default:
                        Logger.Log($"Unhandled step type: {step.Type}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error executing step: {ex.Message}");
                throw;
            }
        }
        private Key GetNumericKey(char c)
        {
            switch (c)
            {
                case '0': return Key.NumPad0;
                case '1': return Key.NumPad1;
                case '2': return Key.NumPad2;
                case '3': return Key.NumPad3;
                case '4': return Key.NumPad4;
                case '5': return Key.NumPad5;
                case '6': return Key.NumPad6;
                case '7': return Key.NumPad7;
                case '8': return Key.NumPad8;
                case '9': return Key.NumPad9;
                default: throw new ArgumentException($"Invalid character: {c}");
            }
        }

        public bool ConditionHandler(Step condition)
        {
            if (condition is null || condition.Type != StepType.Condition || !condition.ConditionType.HasValue)
            {
                Logger.Log("Invalid condition: condition is null or misconfigured");
                return false;
            }

            const int loopMax = 1000;
            int loopCount = 0;
            bool result = false;

            do
            {
                if (loopCount > loopMax)
                {
                    Logger.Log($"Condition timeout after {loopMax} attempts");
                    throw new Exception("Condition loop timeout");
                }

                try
                {
                    switch (condition.ConditionType.Value)
                    {
                        case StepConditionType.FocusedWindowTitle:
                            var currentTitle = ProcessHelper.GetActiveWindowTitle();
                            result = condition.ConditionValue == currentTitle;
                            Logger.Log($"Checking window title: '{currentTitle}' {(result ? "matches" : "does not match")} '{condition.ConditionValue}'");
                            break;

                        case StepConditionType.PixelColor:
                            var mPos = DeserializeJson<StepPosition>(condition.Value);
                            var pixelColor = ScreenHelper.GetPixelColor(mPos.X, mPos.Y);
                            var expectedColor = int.Parse(condition.ConditionValue);
                            result = pixelColor.ToArgb() == expectedColor;
                            Logger.Log($"Checking pixel color at ({mPos.X}, {mPos.Y}): " +
                                $"Found color = #{pixelColor.ToArgb():X8} ({pixelColor.Name}), " +
                                $"Expected color = #{expectedColor:X8} " +
                                $"- {(result ? "matches" : "does not match")}");
                            break;

                        default:
                            Logger.Log($"Type of condition not recognized: {condition.ConditionType}");
                            result = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"Error checking condition: {ex.Message}");
                    throw;
                }

                if (!condition.Loop.GetValueOrDefault())
                    break;

                if (!result)
                {
                    loopCount++;
                    Logger.Log($"Condition not satisfied, new attempt ({loopCount}/{loopMax})");
                    Task.Delay(500).Wait();
                }

            } while (!result);

            Logger.Log($"Final condition result: {(result ? "True" : "False")}");
            return result;
        }


        #region Serializer

        public static string SerializeJson<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new StepJsonConverter());

            return JsonSerializer.Serialize(obj, options);
        }

        public static T DeserializeJson<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new StepJsonConverter());

            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static T Transmute<T>(dynamic obj)
        {
            return DeserializeJson<T>(SerializeJson(obj));
        }

        #endregion

    }
}
