using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotCornersApp {
    /// <summary>
    /// Interaction logic for KeyboardShortcutReader.xaml
    /// </summary>
    public partial class KeyboardShortcutReader : Window {
        int index;
        String keyCombo;
        public KeyboardShortcutReader(int index) {
            InitializeComponent();
            this.index = index;
            if (index == 0) {
                label1.Content = "Top Left Corner";
            } else if (index == 1) {
                label1.Content = "Top Right Corner";
            } else if (index == 2) {
                label1.Content = "Bottom Left Corner";
            } else if (index == 3) {
                label1.Content = "Bottom Right Corner";
            }
            this.Show();
        }

        public void ShortcutHandler(object sender, KeyEventArgs e) {
            // The text box grabs all input.
            e.Handled = true;

            // Fetch the actual shortcut key.
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            // Ignore modifier keys.
            if (key == Key.LeftShift || key == Key.RightShift
                || key == Key.LeftCtrl || key == Key.RightCtrl
                || key == Key.LeftAlt || key == Key.RightAlt
                || key == Key.LWin || key == Key.RWin) {
                return;
            }

            // Build the shortcut key name.
            StringBuilder shortcutText = new StringBuilder();
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0) {
                shortcutText.Append("Ctrl+");
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0) {
                shortcutText.Append("Shift+");
            }
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0) {
                shortcutText.Append("Alt+");
            }
            if ((Keyboard.Modifiers & ModifierKeys.Windows) != 0) {
                shortcutText.Append("Win+");
            }
            shortcutText.Append(key.ToString());

            // Update the text box.
            textBox.Text = shortcutText.ToString();
            this.keyCombo = shortcutText.ToString();

        }

        public void onSave(object sender, RoutedEventArgs e) {
            if (index == 0) {
                Properties.Settings.Default.topLeftShortcut = keyCombo;
            } else if (index == 1) {
                Properties.Settings.Default.topRightShortcut = keyCombo;
            } else if (index == 2) {
                Properties.Settings.Default.bottomLeftShortcut = keyCombo;
            } else if (index == 3) {
                Properties.Settings.Default.bottomRightShortcut = keyCombo;
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var window = Window.GetWindow(this);
            window.KeyDown += ShortcutHandler;
        }
    }
}
