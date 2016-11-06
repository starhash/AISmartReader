using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Readifine {
    /// <summary>
    /// Interaction logic for Context.xaml
    /// </summary>
    public partial class Context : Page {
        string text;
        public Context() {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog().Value) {
                text = File.ReadAllText(ofd.FileName);
            }

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e) {
            try {
                ProcessStartInfo pythonInfo = new ProcessStartInfo();
                Process python;
                pythonInfo.FileName = @"python";
                pythonInfo.Arguments = @"'C:\Users\harsh\Documents\Visual Studio 2015\Projects\AISmartReader\AISmartReader\Anmol\test.py'";
                pythonInfo.CreateNoWindow = false;
                pythonInfo.UseShellExecute = true;

                Console.WriteLine("Python Starting");
                python = Process.Start(pythonInfo);
                python.WaitForExit();
                python.Close();
                Console.WriteLine("Python Exiting");

            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}
