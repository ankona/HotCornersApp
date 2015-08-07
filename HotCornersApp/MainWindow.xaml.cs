using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using WindowsInput.Native;
using System.IO;
using System.Diagnostics;

namespace HotCornersApp {
    public partial class MainWindow : Window {
        private NotifyIcon ni;
        private WindowsInput.InputSimulator kb = new WindowsInput.InputSimulator();
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool LockWorkStation();
        public MainWindow() {
            InitializeComponent();
            checkBox2.IsChecked = Properties.Settings.Default.runOnStartup;
            checkBox1.IsChecked = Properties.Settings.Default.runInTray;
            checkBox3.IsChecked = Properties.Settings.Default.startMinimized;
            checkBox4.IsChecked = Properties.Settings.Default.charmsCornerDisabled;
            comboBox0.SelectedIndex = Array.IndexOf((Array)this.Resources["ComboBoxItems"], Properties.Settings.Default.topLeft);
            comboBox1.SelectedIndex = Array.IndexOf((Array)this.Resources["ComboBoxItems"], Properties.Settings.Default.topRight);
            comboBox2.SelectedIndex = Array.IndexOf((Array)this.Resources["ComboBoxItems"], Properties.Settings.Default.bottomLeft);
            comboBox3.SelectedIndex = Array.IndexOf((Array)this.Resources["ComboBoxItems"], Properties.Settings.Default.bottomRight);
            if(!Properties.Settings.Default.topLeftProcess.Equals("")) {
                label0.Content = Properties.Settings.Default.topLeftProcess;
            }
            if (!Properties.Settings.Default.topRightProcess.Equals("")) {
                label1.Content = Properties.Settings.Default.topRightProcess;
            }
            if (!Properties.Settings.Default.bottomLeftProcess.Equals("")) {
                label2.Content = Properties.Settings.Default.bottomLeftProcess;
            }
            if (!Properties.Settings.Default.bottomRightProcess.Equals("")) {
                label3.Content = Properties.Settings.Default.bottomRightProcess;
            }
            if (!Properties.Settings.Default.topLeftShortcut.Equals("")) {
                label0.Content = Properties.Settings.Default.topLeftShortcut;
            }
            if (!Properties.Settings.Default.topRightShortcut.Equals("")) {
                label1.Content = Properties.Settings.Default.topRightShortcut;
            }
            if (!Properties.Settings.Default.bottomLeftShortcut.Equals("")) {
                label2.Content = Properties.Settings.Default.bottomLeftShortcut;
            }
            if (!Properties.Settings.Default.bottomRightShortcut.Equals("")) {
                label3.Content = Properties.Settings.Default.bottomRightShortcut;
            }
            this.ResizeMode = System.Windows.ResizeMode.CanMinimize;
            ni = new NotifyIcon();
            ni.Icon = Properties.Resources.expose;
            ni.Visible = true;
            ni.BalloonTipText = "Application running in background, minimized to tray";
            ni.BalloonTipTitle = "HotCornersApp";
            ni.Text = "HotCornersApp: Double click to open";

            ContextMenuStrip TrayIconContextMenu = new ContextMenuStrip();
            ToolStripMenuItem CloseMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem AboutMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();
            TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {AboutMenuItem,
            CloseMenuItem});
            TrayIconContextMenu.Name = "TrayIconContextMenu";
            TrayIconContextMenu.Size = new System.Drawing.Size(153, 70);

            CloseMenuItem.Name = "CloseMenuItem";
            CloseMenuItem.Size = new System.Drawing.Size(152, 22);
            CloseMenuItem.Text = "Exit the App";
            CloseMenuItem.Click += new EventHandler(this.CloseApp);

            AboutMenuItem.Name = "AboutMenuItem";
            AboutMenuItem.Size = new System.Drawing.Size(152, 22);
            AboutMenuItem.Text = "About";
            AboutMenuItem.Click += new EventHandler(this.AboutApp);

            TrayIconContextMenu.ResumeLayout(false);
            ni.ContextMenuStrip = TrayIconContextMenu;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args) {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
            if (checkBox3.IsChecked == true) {
                this.Hide();
                this.WindowState = WindowState.Minimized;
            } else {
                this.Show();
                this.WindowState = WindowState.Normal;
            }
            if(checkBox4.IsChecked==true) {
                disableCharmsHotTriggers();
            }
            callMainLoop();
        }

        private void disableCharmsHotTriggers() {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true);
            if (registryKey.GetValue("EdgeUI") != null) {
                registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell\\EdgeUI", true);
                if(registryKey.GetValue("DisableTLcorner")==null) {
                registryKey.CreateSubKey("DisableTLcorner");
                registryKey.SetValue("DisableTLcorner", Convert.ToInt32("1"));
                } else {
                    registryKey.SetValue("DisableTLcorner", Convert.ToInt32("1"));
                }
                if(registryKey.GetValue("DisableCharmsHint")==null) {
                    registryKey.CreateSubKey("DisableCharmsHint");
                    registryKey.SetValue("DisableCharmsHint", Convert.ToInt32("1"));
                } else {
                    registryKey.SetValue("DisableCharmsHint", Convert.ToInt32("1"));
                }
            } else {
                registryKey.CreateSubKey("EdgeUI");
                registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell\\EdgeUI", true);
                if (registryKey.GetValue("DisableTLcorner") == null) {
                    registryKey.CreateSubKey("DisableTLcorner");
                    registryKey.SetValue("DisableTLcorner", Convert.ToInt32("1"));
                } else {
                    registryKey.SetValue("DisableTLcorner", Convert.ToInt32("1"));
                }
                if (registryKey.GetValue("DisableCharmsHint") == null) {
                    registryKey.CreateSubKey("DisableCharmsHint");
                    registryKey.SetValue("DisableCharmsHint", Convert.ToInt32("1"));
                } else {
                    registryKey.SetValue("DisableCharmsHint", Convert.ToInt32("1"));
                }
            }
               
            registryKey.Close();
        }

        private void AboutApp(object sender, EventArgs e) {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

        public int mainMethodLoop() {
            while (true) {
                Point point = GetMousePosition();
                int xRes = (int)SystemParameters.VirtualScreenWidth - 1;
                int yRes = (int)SystemParameters.PrimaryScreenHeight - 1;

                if (point.X == 0 && point.Y == 0) {
                    return 0;
                } else if (point.X == xRes && point.Y == 0) {
                    return 1;
                } else if (point.X == 0 && point.Y == yRes) {
                    return 2;
                } else if (point.X == xRes && point.Y == yRes) {
                    return 3;
                }
                return 4;
            }
        }

        public void actionMapping(int index) {
            if (index == 4) {
                return;
            } else if (index == 1) {
                if (comboBox1.SelectedItem != null) {
                    executeAction(comboBox1.SelectedItem.ToString(), index);
                }
            } else if (index == 2) {
                if (comboBox2.SelectedItem != null) {
                    executeAction(comboBox2.SelectedItem.ToString(), index);
                }
            } else if (index == 3) {
                if (comboBox3.SelectedItem != null) {
                    executeAction(comboBox3.SelectedItem.ToString(), index);
                }
            } else if (index == 0) {
                if (comboBox0.SelectedItem != null) {
                    executeAction(comboBox0.SelectedItem.ToString(), index);
                }
            }
        }
        public async void callMainLoop() {
            while (true) {
                var slowTask = Task<int>.Factory.StartNew(() => mainMethodLoop());
                await slowTask;
                actionMapping(slowTask.Result);
                if (slowTask.Result == 4) {
                    await Task.Delay(75);
                } else {
                    await Task.Delay(500);
                }
            }
        }
        public void executeAction(String index, int hotCornerIndex) {
            if (index.Equals("Lock Screen")) {
                LockWorkStationSafe();
            } else if (index.Equals("Show Desktop")) {
                ShowDesktop();
            } else if (index.Equals("Task View")) {
                showTaskView();
            } else if (index.Equals("Keyboard Shortcut")) {
                if (hotCornerIndex == 0) {
                    List<List<VirtualKeyCode>> resultantKeys = processKeyboardShortcutAsString(Properties.Settings.Default.topLeftShortcut);
                    if (resultantKeys[0].Count == 3) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1], resultantKeys[0][2] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 2) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 1) {
                        kb.Keyboard.ModifiedKeyStroke(resultantKeys[0][0], resultantKeys[1][0]);
                    }
                }
                if (hotCornerIndex == 1) {
                    List<List<VirtualKeyCode>> resultantKeys = processKeyboardShortcutAsString(Properties.Settings.Default.topRightShortcut);
                    if (resultantKeys[0].Count == 3) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1], resultantKeys[0][2] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 2) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 1) {
                        kb.Keyboard.ModifiedKeyStroke(resultantKeys[0][0], resultantKeys[1][0]);
                    }
                }
                if (hotCornerIndex == 2) {
                    List<List<VirtualKeyCode>> resultantKeys = processKeyboardShortcutAsString(Properties.Settings.Default.bottomLeftShortcut);
                    if (resultantKeys[0].Count == 3) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1], resultantKeys[0][2] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 2) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 1) {
                        kb.Keyboard.ModifiedKeyStroke(resultantKeys[0][0], resultantKeys[1][0]);
                    }
                }
                if (hotCornerIndex == 3) {
                    List<List<VirtualKeyCode>> resultantKeys = processKeyboardShortcutAsString(Properties.Settings.Default.bottomRightShortcut);
                    if (resultantKeys[0].Count == 3) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1], resultantKeys[0][2] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 2) {
                        kb.Keyboard.ModifiedKeyStroke(new[] { resultantKeys[0][0], resultantKeys[0][1] }, resultantKeys[1][0]);
                    } else if (resultantKeys[0].Count == 1) {
                        kb.Keyboard.ModifiedKeyStroke(resultantKeys[0][0], resultantKeys[1][0]);
                    }
                }
            } else if(index.Equals("Run Program")) {
                if (hotCornerIndex == 0) {
                    if (!File.Exists(Properties.Settings.Default.topLeftProcess)) {
                        System.Windows.MessageBox.Show("The program.exe file does not exist! Cannot launch.");
                        return;
                    }
                    Process.Start(Properties.Settings.Default.topLeftProcess);
                }
                if (hotCornerIndex == 1) {
                    if (!File.Exists(Properties.Settings.Default.topRightProcess)) {
                        System.Windows.MessageBox.Show("The program.exe file does not exist! Cannot launch.");
                        return;
                    }
                    Process.Start(Properties.Settings.Default.topRightProcess);
                }
                if (hotCornerIndex == 2) {
                    if (!File.Exists(Properties.Settings.Default.bottomLeftProcess)) {
                        System.Windows.MessageBox.Show("The program.exe file does not exist! Cannot launch.");
                        return;
                    }
                    Process.Start(Properties.Settings.Default.bottomLeftProcess);
                }
                if (hotCornerIndex == 3) {
                  if (!File.Exists(Properties.Settings.Default.topRightProcess)) {
                        System.Windows.MessageBox.Show("The program.exe file does not exist! Cannot launch.");
                        return;
                    }
                    Process.Start(Properties.Settings.Default.topRightProcess);

                }
            }
        }

        private List<List<VirtualKeyCode>> processKeyboardShortcutAsString(string shortcut) {
            List<List<VirtualKeyCode>> result = new List<List<VirtualKeyCode>>();
            List<VirtualKeyCode> listModifiers = new List<VirtualKeyCode>();
            List<VirtualKeyCode> listKey = new List<VirtualKeyCode>();
            if (shortcut.Contains("Ctrl")) {
                listModifiers.Add(VirtualKeyCode.CONTROL);
                shortcut = shortcut.Replace("Ctrl", "");
            }
            if (shortcut.Contains("Shift")) {
                listModifiers.Add(VirtualKeyCode.SHIFT);
                shortcut = shortcut.Replace("Shift", "");
            }
            if (shortcut.Contains("Alt")) {
                listModifiers.Add(VirtualKeyCode.MENU);
                shortcut = shortcut.Replace("Alt", "");
            }
            if (shortcut.Contains("Win")) {
                listModifiers.Add(VirtualKeyCode.LWIN);
                shortcut = shortcut.Replace("Win", "");
            }
            shortcut = shortcut.Replace("+", "");
            KeyConverter k = new KeyConverter();
            Key key = (Key)k.ConvertFromString(shortcut);
            listKey.Add((VirtualKeyCode)KeyInterop.VirtualKeyFromKey(key));
            result.Add(listModifiers);
            result.Add(listKey);
            return result;
        }

        public System.Windows.Point GetMousePosition() {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }

        private void CloseApp(object sender, EventArgs e) {
            ni.Visible = false;
            Properties.Settings.Default.runOnStartup = (bool)checkBox2.IsChecked;
            Properties.Settings.Default.runInTray = (bool)checkBox1.IsChecked;
            Properties.Settings.Default.startMinimized = (bool)checkBox3.IsChecked;
            Properties.Settings.Default.charmsCornerDisabled = (bool)checkBox4.IsChecked;
            Properties.Settings.Default.topLeft = (String)comboBox0.SelectedItem;
            Properties.Settings.Default.topRight = (String)comboBox1.SelectedItem;
            Properties.Settings.Default.bottomLeft = (String)comboBox2.SelectedItem;
            Properties.Settings.Default.bottomRight = (String)comboBox3.SelectedItem;
            Properties.Settings.Default.Save();
            if(Properties.Settings.Default.popUpEnabled) {
                ExitDonate donate = new ExitDonate();
            } else {
                App.Current.Shutdown();
            }

        }

        void LockWorkStationSafe() {
            bool result = LockWorkStation();
            if (result == false) {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        void showTaskView() {
            try {
                kb.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
            } catch (Exception e) {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
        void ShowDesktop() {
            Shell32.Shell winShell = new Shell32.Shell();
            winShell.MinimizeAll();

        }
        public void buttonClicked(object sender, RoutedEventArgs e) {

            Properties.Settings.Default.runOnStartup = (bool)checkBox2.IsChecked;
            Properties.Settings.Default.runInTray = (bool)checkBox1.IsChecked;
            Properties.Settings.Default.startMinimized = (bool)checkBox3.IsChecked;
            Properties.Settings.Default.topLeft = (String)comboBox0.SelectedItem;
            Properties.Settings.Default.topRight = (String)comboBox1.SelectedItem;
            Properties.Settings.Default.bottomLeft = (String)comboBox2.SelectedItem;
            Properties.Settings.Default.bottomRight = (String)comboBox3.SelectedItem;
            Properties.Settings.Default.Save();
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (checkBox2.IsChecked == true) {
                registryKey.SetValue("HotCornersApp", System.Reflection.Assembly.GetExecutingAssembly().Location);
            } else {
                if (registryKey.GetValue("HotCornersApp") != null) {
                    registryKey.DeleteValue("HotCornersApp");
                }
            }
            registryKey.Close();


            if (!Properties.Settings.Default.topLeftProcess.Equals("")) {
                label0.Content = Properties.Settings.Default.topLeftProcess;
            }
            if (!Properties.Settings.Default.topRightProcess.Equals("")) {
                label1.Content = Properties.Settings.Default.topRightProcess;
            }
            if (!Properties.Settings.Default.bottomLeftProcess.Equals("")) {
                label2.Content = Properties.Settings.Default.bottomLeftProcess;
            }
            if (!Properties.Settings.Default.bottomRightProcess.Equals("")) {
                label3.Content = Properties.Settings.Default.bottomRightProcess;
            }
            if (!Properties.Settings.Default.topLeftShortcut.Equals("")) {
                label0.Content = Properties.Settings.Default.topLeftShortcut;
            }
            if (!Properties.Settings.Default.topRightShortcut.Equals("")) {
                label1.Content = Properties.Settings.Default.topRightShortcut;
            }
            if (!Properties.Settings.Default.bottomLeftShortcut.Equals("")) {
                label2.Content = Properties.Settings.Default.bottomLeftShortcut;
            }
            if (!Properties.Settings.Default.bottomRightShortcut.Equals("")) {
                label3.Content = Properties.Settings.Default.bottomRightShortcut;
            }
            if(checkBox4.IsChecked==true) {
                disableCharmsHotTriggers();
            }
            if (checkBox1.IsChecked == true) {
                this.Hide();
                ni.ShowBalloonTip(10000);
            }
        }
        protected override void OnStateChanged(EventArgs e) {
            if (WindowState == System.Windows.WindowState.Minimized && checkBox1.IsChecked == true) {
                this.Hide();
                //ni.ShowBalloonTip(10000);
            }
            base.OnStateChanged(e);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            if (checkBox1.IsChecked == true) {
                Hide();
                ni.ShowBalloonTip(10000);
                e.Cancel = true;
            }
        }

        private void comboBox0_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Keyboard Shortcut")) {
                if (Properties.Settings.Default.topLeftShortcut.Length == 0) {
                    KeyboardShortcutReader reader = new KeyboardShortcutReader(0);
                    label0.Content = Properties.Settings.Default.topLeftShortcut;
                }
            }
            else if((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Run Program")) {
                if (Properties.Settings.Default.topLeftProcess.Length == 0) {
                    ProcessPathReader reader = new ProcessPathReader(0);
                }
                label0.Content = Properties.Settings.Default.topLeftProcess;
            } else {
                Properties.Settings.Default.topLeftShortcut = "";
                Properties.Settings.Default.topLeftProcess = "";
                Properties.Settings.Default.Save();
                label0.Content = "";
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Keyboard Shortcut")) {
                if (Properties.Settings.Default.topRightShortcut.Length == 0) {
                    KeyboardShortcutReader reader = new KeyboardShortcutReader(1);
                    label1.Content = Properties.Settings.Default.topRightShortcut;
                }
            } else if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Run Program")) {
                if (Properties.Settings.Default.topRightProcess.Length == 0) {
                    ProcessPathReader reader = new ProcessPathReader(1);
                }
                label1.Content = Properties.Settings.Default.topRightProcess;
            } else {
                Properties.Settings.Default.topRightShortcut = "";
                Properties.Settings.Default.topRightProcess = "";
                Properties.Settings.Default.Save();
                label1.Content = "";
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Keyboard Shortcut")) {
                if (Properties.Settings.Default.bottomLeftShortcut.Length == 0) {
                    KeyboardShortcutReader reader = new KeyboardShortcutReader(2);
                    label2.Content = Properties.Settings.Default.bottomLeftShortcut;
                }
            } else if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Run Program")) {
                if (Properties.Settings.Default.bottomLeftProcess.Length == 0) {
                    ProcessPathReader reader = new ProcessPathReader(2);
                }
                label2.Content = Properties.Settings.Default.bottomLeftProcess;
            } else {
                Properties.Settings.Default.bottomLeftShortcut = "";
                Properties.Settings.Default.bottomLeftProcess = "";
                Properties.Settings.Default.Save();
                label2.Content = "";
            }
        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Keyboard Shortcut")) {
                if (Properties.Settings.Default.bottomRightShortcut.Length == 0) {
                    KeyboardShortcutReader reader = new KeyboardShortcutReader(3);
                    label2.Content = Properties.Settings.Default.bottomRightShortcut;
                }
            } else if ((sender as System.Windows.Controls.ComboBox).SelectedItem != null && (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString().Equals("Run Program")) {
                if (Properties.Settings.Default.bottomRightProcess.Length == 0) {
                    ProcessPathReader reader = new ProcessPathReader(3);
                }
                label2.Content = Properties.Settings.Default.bottomRightProcess;
            } else {
                Properties.Settings.Default.bottomRightShortcut = "";
                Properties.Settings.Default.bottomRightProcess = "";
                Properties.Settings.Default.Save();
                label3.Content = "";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            string caption = "HotCornersApp Help";
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            System.Windows.MessageBox.Show("1)Select the functions for HotCorners\n2)Select Save\n3)Enjoy the App! \n4)Dont' forget to rate us and buy addon to support us.", caption, buttons, icon);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.runOnStartup = false;
            Properties.Settings.Default.runInTray = false;
            Properties.Settings.Default.startMinimized = false;
            Properties.Settings.Default.topLeft = "";
            Properties.Settings.Default.topRight = "";
            Properties.Settings.Default.bottomLeft = "";
            Properties.Settings.Default.bottomRight = "";
            Properties.Settings.Default.bottomLeftProcess = "";
            Properties.Settings.Default.bottomRightProcess = "";
            Properties.Settings.Default.topLeftProcess = "";
            Properties.Settings.Default.topRightProcess = "";
            Properties.Settings.Default.bottomLeftShortcut = "";
            Properties.Settings.Default.bottomRightShortcut = "";
            Properties.Settings.Default.topLeftShortcut = "";
            Properties.Settings.Default.topRightShortcut = "";
            Properties.Settings.Default.popUpEnabled = true;
            Properties.Settings.Default.Save();
            label0.Content = "";
            label1.Content = "";
            label2.Content = "";
            label3.Content = "";
            label0.Content = "";
            label1.Content = "";
            label2.Content = "";
            label3.Content = "";
            checkBox2.IsChecked = false;
            checkBox1.IsChecked = false;
            checkBox3.IsChecked = false;
            checkBox4.IsChecked = false;
            comboBox0.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

        }
    }
}
