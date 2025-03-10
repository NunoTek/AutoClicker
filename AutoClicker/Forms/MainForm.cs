using GlobalHotKey;
using AutoClicker.Stepper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using AutoClicker.Helpers;

namespace AutoClicker
{
    public partial class MainForm : Form
    {
        private StepCreatorForm creatorForm;
        private int _currentCounter = 0;
        private CancellationTokenSource _cts = null;

        public MainForm()
        {
            InitializeComponent();
            Logger.LogUpdated += Logger_LogUpdated;
            Logger.Log("Application started");
            counterValueTextBox.TextChanged += CounterValueTextBox_TextChanged;
        }

        private void Logger_LogUpdated(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateLogDisplay()));
            }
            else
            {
                UpdateLogDisplay();
            }
        }

        private void UpdateLogDisplay()
        {
            logTextBox.Text = string.Join(Environment.NewLine, Logger.GetLogs());
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void clearLogsButton_Click(object sender, EventArgs e)
        {
            Logger.Clear();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Logger.LogUpdated -= Logger_LogUpdated;
            base.OnFormClosing(e);
        }

        private void createFormBtn_Click(object sender, EventArgs e)
        {
            if (creatorForm == null || creatorForm.IsDisposed)
                creatorForm = new StepCreatorForm(textBoxSteps.Text);
            creatorForm.Show();
        }

        private void screenshotButton_Click(object sender, EventArgs e)
        {
            Bitmap bmp = ScreenHelper.TakeScreenshot();
            pictureBox1.Image = bmp;
        }

        private void stepsFormButton_Click(object sender, EventArgs e)
        {
            var userInput = this.openFileDialog1.ShowDialog();
            if (userInput == DialogResult.OK)
            {
                var filePath = this.openFileDialog1.FileName;
                var steps = File.ReadAllText(filePath);
                textBoxSteps.Text = steps;
            }
        }


        private StepsHandler _handler = null;
        private Task _handlerTask = null;
        private CancellationToken _ct = default;

        private async void startButton_Click(object sender, EventArgs e)
        {
            if (_handler != null)
            {
                MessageBox.Show("AutoClick already in progress");
                return;
            }

            var steps = new List<Step>();
            try
            {
                steps = StepsHandler.DeserializeJson<List<Step>>(textBoxSteps.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log($"Deserialization error: {ex.Message}");
                return;
            }

            startButton.Enabled = false;
            pauseButton.Enabled = true;
            continueButton.Enabled = false;
            stopButton.Enabled = true;
            counterValueTextBox.Enabled = false;

            try
            {
                _cts = new CancellationTokenSource();
                _handler = new StepsHandler();
                _handler.PropertyChanged += (s, ev) =>
                {
                    if (ev.PropertyName == nameof(StepsHandler.Counter))
                    {
                        this.Invoke((MethodInvoker)delegate {
                            if (_handler != null)
                            {
                                Logger.Log($"Step {_handler.Counter} in progress");
                            }
                        });
                    }
                    else if (ev.PropertyName == nameof(StepsHandler.WriteCounter))
                    {
                        this.Invoke((MethodInvoker)delegate {
                            if (_handler != null)
                            {
                                countLabel.Text = _handler.WriteCounter.ToString();
                            }
                        });
                    }
                    else if (ev.PropertyName == nameof(StepsHandler.ScreenCapture))
                    {
                        this.Invoke((MethodInvoker)delegate {
                            if (_handler != null)
                            {
                                pictureBox1.Image = _handler.ScreenCapture;
                            }
                        });
                    }
                };

                _handler.WriteCounter = _currentCounter;
                await _handler.HandleStepsAsync(steps, _cts.Token);
                Logger.Log("Starting step execution");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log($"Execution error: {ex.Message}");
            }
            finally
            {
                startButton.Enabled = true;
                pauseButton.Enabled = false;
                continueButton.Enabled = false;
                stopButton.Enabled = false;
                counterValueTextBox.Enabled = true;

                _handler = null;
                _handlerTask = null;
                if (_cts != null)
                {
                    _cts.Dispose();
                    _cts = null;
                }
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (_handler == null)
            {
                MessageBox.Show("AutoClick not started");
                return;
            }

            _handlerTask.Wait();
            Logger.Log("Pausing execution");

            startButton.Enabled = false;
            pauseButton.Enabled = false;
            continueButton.Enabled = true;
            stopButton.Enabled = true;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (_handler == null)
            {
                MessageBox.Show("AutoClick not started");
                return;
            }

            _handlerTask.Start();
            Logger.Log("Resuming execution");

            startButton.Enabled = false;
            pauseButton.Enabled = true;
            continueButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (_handler == null)
            {
                MessageBox.Show("AutoClick not started");
                return;
            }

            try
            {
                _cts?.Cancel();
                Logger.Log("Stopping execution");

                startButton.Enabled = true;
                pauseButton.Enabled = false;
                continueButton.Enabled = false;
                stopButton.Enabled = false;
                counterValueTextBox.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error during stop: {ex.Message}");
                MessageBox.Show($"Error during stop: {ex.Message}");
            }
        }

        private void loadStepsButton_Click(object sender, EventArgs e)
        {
            var ofd = openFileDialog1.ShowDialog();
            if (ofd == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                var steps = File.ReadAllText(filePath);
                textBoxSteps.Text = steps;
            }

            Logger.Log("Loading steps from file");
        }

        private readonly HotKeyManager _hotKeyManager = new HotKeyManager();
        private readonly HashSet<HotKey> _hotKeys = new HashSet<HotKey>();

        private void InitializeHotkeys()
        {
            ModifierKeys shortcut = System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Alt;

            var startHotKey = _hotKeyManager.Register(Key.A, shortcut);
            _hotKeys.Add(startHotKey);


            _hotKeyManager.KeyPressed += HandleHotkey;
        }

        private void HandleHotkey(object sender, KeyPressedEventArgs e)
        {
            switch (e.HotKey.Key)
            {
                case Key.A:
                    startButton_Click(sender, e);
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeHotkeys();
        }

        private void CounterValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(counterValueTextBox.Text, out int value))
            {
                _currentCounter = value;
            }
        }
    }
}
