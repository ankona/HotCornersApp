using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class ExitDonate : Window {
        public ExitDonate() {
            InitializeComponent();
            this.Show();
        }
        public void appClose(object sender, EventArgs e) {
            Properties.Settings.Default.popUpEnabled =(bool)!checkBox.IsChecked;
            Properties.Settings.Default.Save();
            this.Close();
            App.Current.Shutdown();
        }

        private void Hyperlink_RequestNavigateDonate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
