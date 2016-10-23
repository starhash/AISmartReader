using AISmartReaderLibrary;
using Microsoft.Win32;
using Readifine.Support;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for BookViewer.xaml
    /// </summary>
    public partial class BookViewer : Page {
        Paginator paginator;
        UserPreferences pref;

        public BookViewer() {
            InitializeComponent();
            pref = new UserPreferences();
            if((pref.LoadUserPreference())) {
                load.IsEnabled = true;
            } else {
                UserTest test = new UserTest();
                test.ShowDialog();
                pref = new UserPreferences();
                if (pref.LoadUserPreference(test.Level)) {
                    
                }
            }
        }

        private void load_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog open = new OpenFileDialog();
            if(open.ShowDialog().Value) {
                paginator = new Paginator(File.ReadAllText(open.FileName));
                paginator.OnRetrainRequested += Paginator_OnRetrainRequested;
                paginator.Fill(doc, pref);
            }
        }

        private void Paginator_OnRetrainRequested(string request) {
            pref.ReTrain(request, true);
        }

        private void next_Click(object sender, RoutedEventArgs e) {
            paginator.Fill(doc, pref);
        }

        private void prev_Click(object sender, RoutedEventArgs e) {
            paginator.BackFill(doc, pref);
        }
    }
}
