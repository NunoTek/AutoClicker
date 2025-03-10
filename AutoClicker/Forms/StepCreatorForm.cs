using GlobalHotKey;
using AutoClicker.Forms;
using AutoClicker.Helpers;
using AutoClicker.Stepper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoClicker
{
    public partial class StepCreatorForm : Form
    {
        private bool isDragging = false;
        private Step _copiedStep = null;

        public StepCreatorForm(string initialSteps = "")
        {
            InitializeComponent();
            UpdateContextMenu();
            if (!string.IsNullOrEmpty(initialSteps))
            {
                LoadInitialSteps(initialSteps);
            }
        }

        private void LoadInitialSteps(string stepsJson)
        {
            try
            {
                _steps = StepsHandler.DeserializeJson<List<Step>>(stepsJson);
                stepsTextBox.Text = stepsJson;
                UpdateTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading steps: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            var sfd = saveFileDialog1.ShowDialog();
            if (sfd == DialogResult.OK)
            {
                var filePath = saveFileDialog1.FileName;
                System.IO.File.WriteAllText(filePath, stepsTextBox.Text);
            }
        }

        #region " Stepper "

        private List<Step> _steps = new List<Step>();

        private void InitStepper()
        {
            if (_steps == null)
                _steps = new List<Step>();

            stepsTreeView.ItemDrag += StepsTreeView_ItemDrag;
            stepsTreeView.DragEnter += StepsTreeView_DragEnter;
            stepsTreeView.DragOver += StepsTreeView_DragOver;
            stepsTreeView.DragDrop += StepsTreeView_DragDrop;
        }

        private void AddNewStep(Step step)
        {
            InitStepper();

            if (stepsTreeView.SelectedNode != null)
            {
                var parentStep = stepsTreeView.SelectedNode.Tag as Step;
                if (parentStep != null)
                {
                    if (parentStep.Childs == null)
                        parentStep.Childs = new List<Step>();

                    parentStep.Childs.Add(step);
                }
            }
            else
            {
                _steps.Add(step);
            }

            SaveStepper();
        }

        private void UpdateContextMenu()
        {
            if (treeViewContextMenu != null)
            {
                var pasteItem = treeViewContextMenu.Items.Cast<ToolStripMenuItem>()
                    .FirstOrDefault(item => item.Text == "Paste");
                    
                if (pasteItem != null)
                {
                    pasteItem.Visible = _copiedStep != null;
                }
            }
        }

        private void UpdateTreeView()
        {
            stepsTreeView.Nodes.Clear();
            foreach (var step in _steps)
            {
                AddStepToTree(step, null);
            }
            stepsTreeView.ExpandAll();
        }

        private void AddStepToTree(Step step, TreeNode parentNode)
        {
            var node = new TreeNode(step.Label)
            {
                Tag = step
            };

            if (parentNode == null)
                stepsTreeView.Nodes.Add(node);
            else
                parentNode.Nodes.Add(node);

            if (step.Childs != null)
            {
                foreach (var child in step.Childs)
                {
                    AddStepToTree(child, node);
                }
            }
        }

        private void deleteStepButton_Click(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null)
            {
                MessageBox.Show("Please select a step to delete");
                return;
            }

            var stepToDelete = stepsTreeView.SelectedNode.Tag as Step;
            if (stepToDelete == null) return;

            RemoveStepRecursively(_steps, stepToDelete);
            SaveStepper();
            UpdateTreeView();
        }

        private void onRename_Click(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null) return;

            var step = stepsTreeView.SelectedNode.Tag as Step;
            if (step == null) return;

            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Rename step";
                inputDialog.Size = new Size(300, 150);
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;

                var textBox = new TextBox();
                textBox.Text = step.Label;
                textBox.Location = new Point(10, 20);
                textBox.Size = new Size(260, 20);

                var okButton = new Button();
                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(110, 60);

                inputDialog.Controls.Add(textBox);
                inputDialog.Controls.Add(okButton);
                inputDialog.AcceptButton = okButton;

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    step.Label = textBox.Text;
                    SaveStepper();
                }
            }
        }

        private void onCopy_Click(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null) return;

            var stepToCopy = stepsTreeView.SelectedNode.Tag as Step;
            if (stepToCopy == null) return;

            _copiedStep = CloneStep(stepToCopy);
            UpdateContextMenu();
            Logger.Log($"Step '{stepToCopy.Label}' copied");
        }

        private void onPaste_Click(object sender, EventArgs e)
        {
            if (_copiedStep == null)
            {
                MessageBox.Show("No step has been copied");
                return;
            }

            if (stepsTreeView.SelectedNode == null)
            {
                _steps.Add(CloneStep(_copiedStep));
            }
            else
            {
                var targetStep = stepsTreeView.SelectedNode.Tag as Step;
                if (targetStep == null) return;

                if (targetStep.Childs == null)
                    targetStep.Childs = new List<Step>();

                targetStep.Childs.Add(CloneStep(_copiedStep));
            }

            SaveStepper();
            Logger.Log($"Step '{_copiedStep.Label}' pasted");
        }

        private Step CloneStep(Step originalStep)
        {
            var newStep = new Step
            {
                Id = Guid.NewGuid().ToString(),
                Label = originalStep.Label,
                Type = originalStep.Type,
                Value = originalStep.Value,
                ConditionType = originalStep.ConditionType,
                ConditionValue = originalStep.ConditionValue,
                Loop = originalStep.Loop
            };

            if (originalStep.Childs != null)
            {
                newStep.Childs = new List<Step>();
                foreach (var child in originalStep.Childs)
                {
                    newStep.Childs.Add(CloneStep(child));
                }
            }

            return newStep;
        }

        private bool RemoveStepRecursively(List<Step> steps, Step stepToDelete)
        {
            if (steps.Any(s => s.Id == stepToDelete.Id))
            {
                steps.RemoveAll(s => s.Id == stepToDelete.Id);
                return true;
            }

            foreach (var step in steps)
            {
                if (step.Childs != null && RemoveStepRecursively(step.Childs, stepToDelete))
                    return true;
            }

            return false;
        }

        private void SaveStepper()
        {
            stepsTextBox.Text = StepsHandler.SerializeJson(_steps);
            UpdateTreeView();
        }

        private void commandListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            string text = commandListBox.Items[e.Index].ToString();

            if (text.EndsWith(":"))
            {
                using (Font boldFont = new Font(e.Font, FontStyle.Bold))
                {
                    e.Graphics.DrawString(text, boldFont, new SolidBrush(e.ForeColor), e.Bounds);
                }
            }
            else
            {
                e.Graphics.DrawString(text, e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void onWaitStepClick(object sender, EventArgs e)
        {
            AddNewStep(new Step
            {
                Label = "Wait",
                Type = StepType.Wait,
                Value = "1000"
            });
        }

        private void onIncrementStepClick(object sender, EventArgs e)
        {
            AddNewStep(new Step
            {
                Label = "Increment",
                Type = StepType.Increment
            });
        }

        private void onCheckPixelStepClick(object sender, EventArgs e)
        {
            var pos = MouseHelper.GetMousePosition();
            var pixelColor = ScreenHelper.GetPixelColor(pos.X, pos.Y).ToArgb().ToString();
            AddNewStep(new Step
            {
                Label = "Check Pixel Color",
                Type = StepType.Condition,
                Value = StepsHandler.SerializeJson(new StepPosition
                {
                    X = pos.X,
                    Y = pos.Y
                }),
                ConditionType = StepConditionType.PixelColor,
                ConditionValue = pixelColor,
                Loop = false,
            });
        }

        private void onCheckFocusedWindowTitleStepClick(object sender, EventArgs e)
        {
            AddNewStep(new Step
            {
                Label = "Check Focused Window Title",
                Type = StepType.Condition,
                Value = string.Empty,
                ConditionType = StepConditionType.FocusedWindowTitle,
                ConditionValue = ProcessHelper.GetActiveWindowTitle(),
                Loop = false
            });
        }

        private void onMouseMoveStepClick(object sender, EventArgs e)
        {
            var pos = MouseHelper.GetMousePosition();
            AddNewStep(new Step
            {
                Label = "Mouse Move",
                Type = StepType.MouseMove,
                Value = StepsHandler.SerializeJson(new StepPosition
                {
                    X = pos.X,
                    Y = pos.Y
                })
            });
        }

        private void onMouseLeftClickStepClick(object sender, EventArgs e)
        {
            var pos = MouseHelper.GetMousePosition();
            AddNewStep(new Step
            {
                Label = "Mouse Left Click",
                Type = StepType.MouseLeftClick,
                Value = StepsHandler.SerializeJson(new StepPosition
                {
                    X = pos.X,
                    Y = pos.Y
                })
            });
        }

        private void onMouseRightClickStepClick(object sender, EventArgs e)
        {
            var pos = MouseHelper.GetMousePosition();
            AddNewStep(new Step
            {
                Label = "Mouse Right Click",
                Type = StepType.MouseRightClick,
                Value = StepsHandler.SerializeJson(new StepPosition
                {
                    X = pos.X,
                    Y = pos.Y
                })
            });
        }

        private void onKeyboardPressStepClick(object sender, EventArgs e)
        {
            using (var keyCaptureForm = new KeyCaptureForm())
            {
                if (keyCaptureForm.ShowDialog() == DialogResult.OK)
                {
                    AddNewStep(new Step
                    {
                        Label = "Keyboard Press",
                        Type = StepType.KeyboardPress,
                        Value = StepsHandler.SerializeJson(new StepKeyPress
                        {
                            Key = keyCaptureForm.CapturedKey,
                            Modifiers = keyCaptureForm.CapturedModifiers
                        })
                    });
                }
            }
        }

        private void onActivateWindowStepClick(object sender, EventArgs e)
        {
            using (var windowSelector = new WindowSelectorForm())
            {
                if (windowSelector.ShowDialog() == DialogResult.OK)
                {
                    AddNewStep(new Step
                    {
                        Label = "Activate Window",
                        Type = StepType.ActivateWindow,
                        Value = windowSelector.SelectedWindowTitle
                    });
                }
            }
        }

        private void onWriteCounterStepClick(object sender, EventArgs e)
        {
            AddNewStep(new Step
            {
                Label = "Write Counter",
                Type = StepType.WriteCounter,
                Value = string.Empty
            });
        }

        private void onIfStepClick(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null)
            {
                MessageBox.Show("Please select a parent condition");
                return;
            }

            var parentStep = stepsTreeView.SelectedNode.Tag as Step;
            if (parentStep == null || parentStep.Type != StepType.Condition)
            {
                MessageBox.Show("\"If\" must be a child of a condition");
                return;
            }

            if (parentStep.Childs == null)
                parentStep.Childs = new List<Step>();

            var ifStep = new Step
            {
                Label = "If",
                Type = StepType.If
            };

            parentStep.Childs.Add(ifStep);
            SaveStepper();
        }

        private void onElseStepClick(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null)
            {
                MessageBox.Show("Please select a parent condition");
                return;
            }

            var parentStep = stepsTreeView.SelectedNode.Tag as Step;
            if (parentStep == null || parentStep.Type != StepType.Condition)
            {
                MessageBox.Show("\"Else\" must be a child of a condition");
                return;
            }

            if (parentStep.Childs == null)
                parentStep.Childs = new List<Step>();

            if (parentStep.Childs.Any(x => x.Type == StepType.Else))
            {
                MessageBox.Show("Une condition ne peut avoir qu'un seul bloc Else");
                return;
            }

            var elseStep = new Step
            {
                Label = "Else",
                Type = StepType.Else
            };

            parentStep.Childs.Add(elseStep);
            SaveStepper();
        }

        private void onWhileStepClick(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null)
            {
                MessageBox.Show("Please select a parent condition");
                return;
            }

            var parentStep = stepsTreeView.SelectedNode.Tag as Step;
            if (parentStep == null || parentStep.Type != StepType.Condition)
            {
                MessageBox.Show("\"While\" must be a child of a condition");
                return;
            }

            if (parentStep.Childs == null)
                parentStep.Childs = new List<Step>();

            var whileStep = new Step
            {
                Label = "While",
                Type = StepType.While
            };

            parentStep.Childs.Add(whileStep);
            SaveStepper();
        }

        private void onWhileNotStepClick(object sender, EventArgs e)
        {
            if (stepsTreeView.SelectedNode == null)
            {
                MessageBox.Show("Please select a parent condition");
                return;
            }

            var parentStep = stepsTreeView.SelectedNode.Tag as Step;
            if (parentStep == null || parentStep.Type != StepType.Condition)
            {
                MessageBox.Show("\"While not\" must be a child of a condition");
                return;
            }

            if (parentStep.Childs == null)
                parentStep.Childs = new List<Step>();

            var whileNotStep = new Step
            {
                Label = "While not",
                Type = StepType.WhileNot
            };

            parentStep.Childs.Add(whileNotStep);
            SaveStepper();
        }

        private void onLoopStepClick(object sender, EventArgs e)
        {
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Number of iterations";
                inputDialog.Size = new Size(300, 150);
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;

                var textBox = new TextBox();
                textBox.Text = "1";
                textBox.Location = new Point(10, 20);
                textBox.Size = new Size(260, 20);

                var okButton = new Button();
                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(110, 60);

                inputDialog.Controls.Add(textBox);
                inputDialog.Controls.Add(okButton);
                inputDialog.AcceptButton = okButton;

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(textBox.Text, out int iterations) && iterations > 0)
                    {
                        AddNewStep(new Step
                        {
                            Label = $"For ({iterations} times)",
                            Type = StepType.Loop,
                            Value = iterations.ToString()
                        });
                    }
                }
            }
        }

        private void StepsTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void StepsTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void StepsTreeView_DragOver(object sender, DragEventArgs e)
        {
            // Obtenir le nœud cible
            Point targetPoint = stepsTreeView.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = stepsTreeView.GetNodeAt(targetPoint);

            // Mettre en surbrillance le nœud cible
            if (targetNode != null)
            {
                stepsTreeView.SelectedNode = targetNode;
            }
        }

        private void StepsTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (isDragging) return;

            stepsTreeView.BeginUpdate();
            isDragging = true;

            try
            {
                // Obtenir le nœud source et cible
                TreeNode sourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                Point targetPoint = stepsTreeView.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = stepsTreeView.GetNodeAt(targetPoint);

                if (sourceNode != null && !IsAncestor(sourceNode, targetNode))
                {
                    Step sourceStep = sourceNode.Tag as Step;
                    Step targetStep = targetNode?.Tag as Step;

                    // Supprimer l'étape source de son parent actuel
                    RemoveStepRecursively(_steps, sourceStep);

                    if (targetNode == null)
                    {
                        // Ajouter à la racine
                        _steps.Add(sourceStep);
                    }
                    else
                    {
                        // Ajouter comme enfant de la cible
                        if (targetStep.Childs == null)
                            targetStep.Childs = new List<Step>();
                        targetStep.Childs.Add(sourceStep);
                    }

                    SaveStepper();
                }
            }
            finally
            {
                stepsTreeView.EndUpdate();
                isDragging = false;
            }
        }
        private void stepsTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            var node = stepsTreeView.GetNodeAt(e.X, e.Y);
            if (node == null)
            {
                stepsTreeView.SelectedNode = null;
            }
        }

        private bool IsAncestor(TreeNode node, TreeNode potentialAncestor)
        {
            if (potentialAncestor == null) return false;
            if (node == potentialAncestor) return true;

            TreeNode parent = potentialAncestor.Parent;
            while (parent != null)
            {
                if (parent == node) return true;
                parent = parent.Parent;
            }
            return false;
        }


        #endregion


        #region " Hotkeys "

        private void StepCreatorForm_Load(object sender, EventArgs e)
        {
            InitializeHotkeys();
        }

        private readonly HotKeyManager _hotKeyManager = new HotKeyManager();
        private readonly HashSet<HotKey> _hotKeys = new HashSet<HotKey>();

        private void InitializeHotkeys()
        {
            ModifierKeys shortcut = System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Alt;

            var waitHotKey = _hotKeyManager.Register(Key.W, shortcut);
            _hotKeys.Add(waitHotKey);

            var incrementHotKey = _hotKeyManager.Register(Key.I, shortcut);
            _hotKeys.Add(incrementHotKey);

            var checkPixelConditionHotKey = _hotKeyManager.Register(Key.P, shortcut);
            _hotKeys.Add(checkPixelConditionHotKey);

            var checkFocusedWindowTitleConditionHotKey = _hotKeyManager.Register(Key.T, shortcut);
            _hotKeys.Add(checkFocusedWindowTitleConditionHotKey);

            var mouseMoveHotKey = _hotKeyManager.Register(Key.M, shortcut);
            _hotKeys.Add(mouseMoveHotKey);

            var mouseLeftClickHotKey = _hotKeyManager.Register(Key.L, shortcut);
            _hotKeys.Add(mouseLeftClickHotKey);

            var mouseRightClickHotKey = _hotKeyManager.Register(Key.R, shortcut);
            _hotKeys.Add(mouseRightClickHotKey);

            var keyboardPressHotkey = _hotKeyManager.Register(Key.K, shortcut);
            _hotKeys.Add(keyboardPressHotkey);

            var activateWindowHotkey = _hotKeyManager.Register(Key.F, shortcut);
            _hotKeys.Add(activateWindowHotkey);

            _hotKeyManager.KeyPressed += HandleHotkey;
        }

        private void HandleHotkey(object sender, KeyPressedEventArgs e)
        {
            switch (e.HotKey.Key)
            {
                case Key.W:
                    onWaitStepClick(sender, e);
                    break;
                case Key.I:
                    onIncrementStepClick(sender, e);
                    break;
                case Key.P:
                    onCheckPixelStepClick(sender, e);
                    break;
                case Key.T:
                    onCheckFocusedWindowTitleStepClick(sender, e);
                    break;
                case Key.M:
                    onMouseMoveStepClick(sender, e);
                    break;
                case Key.L:
                    onMouseLeftClickStepClick(sender, e);
                    break;
                case Key.R:
                    onMouseRightClickStepClick(sender, e);
                    break;
                case Key.K:
                    onKeyboardPressStepClick(sender, e);
                    break;
                case Key.F:
                    onActivateWindowStepClick(sender, e);
                    break;
            }
        }

        private void StepCreatorForm_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Désinscription de l'événement
                _hotKeyManager.KeyPressed -= HandleHotkey;

                // Libération des raccourcis
                foreach (var hotKey in _hotKeys)
                {
                    _hotKeyManager.Unregister(hotKey);
                }

                // Libération du manager
                _hotKeyManager.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la fermeture : {ex.Message}");
            }
        }

        #endregion

        private void commandListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void commandListBox_DoubleClick(object sender, EventArgs e)
        {
            if (commandListBox.SelectedIndex == -1)
                return;

            if (commandListBox.SelectedItem.ToString().EndsWith(":"))
                return;

            var selectedNode = stepsTreeView.SelectedNode;

            switch (commandListBox.SelectedIndex)
            {
                case 1: // Wait
                    onWaitStepClick(sender, e);
                    break;
                case 2: // Increment
                    onIncrementStepClick(sender, e);
                    break;
                case 3: // Mouse Move
                    onMouseMoveStepClick(sender, e);
                    break;
                case 4: // Mouse Left Click
                    onMouseLeftClickStepClick(sender, e);
                    break;
                case 5: // Mouse Right Click
                    onMouseRightClickStepClick(sender, e);
                    break;
                case 6: // Keyboard Press
                    onKeyboardPressStepClick(sender, e);
                    break;
                case 7:
                    onActivateWindowStepClick(sender, e);
                    break;
                case 8: // Write Counter
                    onWriteCounterStepClick(sender, e);
                    break;
                case 10: // Check Pixel Condition
                    onCheckPixelStepClick(sender, e);
                    break;
                case 11: // Check Focused Window Title
                    onCheckFocusedWindowTitleStepClick(sender, e);
                    break;
                case 13: // If
                    onIfStepClick(sender, e);
                    break;
                case 14: // Else
                    onElseStepClick(sender, e);
                    break;
                case 15: // While
                    onWhileStepClick(sender, e);
                    break;
                case 16: // While not
                    onWhileNotStepClick(sender, e);
                    break;
                case 18: // Loop
                    onLoopStepClick(sender, e);
                    break;
            }
        }
    }
}
