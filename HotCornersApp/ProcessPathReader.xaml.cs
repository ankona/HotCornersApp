using Microsoft.Win32;
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
    /// Interaction logic for ProcessPathReader.xaml
    /// </summary>
    public partial class ProcessPathReader : Window {
        int screenIndex;
        String path;
        public ProcessPathReader(int screenIndex) {
            InitializeComponent();
            this.screenIndex = screenIndex;
            if (screenIndex == 0) {
                label1.Content = "Top Left Corner";
            } else if (screenIndex == 1) {
                label1.Content = "Top Right Corner";
            } else if (screenIndex == 2) {
                label1.Content = "Bottom Left Corner";
            } else if (screenIndex == 3) {
                label1.Content = "Bottom Right Corner";
            }
            this.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "EXE Files (*.exe)|*.exe|BAT Files (*.bat)|*.bat" };
            var result = ofd.ShowDialog();
            ofd.DefaultExt = ".exe";
            if (result == false) return;
            textBox.Text = ofd.FileName;
            if(screenIndex==0) {
                Properties.Settings.Default.topLeftProcess = ofd.FileName;
            }else if(screenIndex==1) {
                Properties.Settings.Default.topRightProcess = ofd.FileName;
            } else if(screenIndex==2) {
                Properties.Settings.Default.topRightProcess = ofd.FileName;
            } else if(screenIndex==3) {
                Properties.Settings.Default.topRightProcess = ofd.FileName;
            }
            this.path = ofd.FileName;
        }

        public void onSave(object sender, RoutedEventArgs e) {
            if (screenIndex == 0) {
                Properties.Settings.Default.topLeftShortcut = path;
            } else if (screenIndex == 1) {
                Properties.Settings.Default.topRightShortcut = path;
            } else if (screenIndex == 2) {
                Properties.Settings.Default.bottomLeftShortcut = path;
            } else if (screenIndex == 3) {
                Properties.Settings.Default.bottomRightShortcut = path;
            }
            this.Close();
        }
    }
}


