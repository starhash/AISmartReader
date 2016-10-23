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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AISmartReaderLibrary;

namespace Readifine {
    /// <summary>
    /// Interaction logic for UserTest.xaml
    /// </summary>
    public partial class UserTest : Window {
        public int Level { get; set; }

        UserVocabularyDeterminerPlugin plug;

        public delegate void CompleteEvent(UserVocabularyDeterminerPlugin plug, EventArgs e);
        public event CompleteEvent OnTestCompleted;

        public UserTest() {
            InitializeComponent();
            plug = new UserVocabularyDeterminerPlugin();
            LoadQuestion();       
        }

        public void LoadQuestion() {
            Question q = plug.GetQuestion();
            if(q == null) {
                Level = plug.Calculate();
                OnTestCompleted?.Invoke(plug, new EventArgs());
                Close();
                return;
            }
            word.Content = q.Word;
            options.Children.Clear();
            q.Options.ForEach((x) => {
                RadioButton rb = new RadioButton();
                TextBlock tb = new TextBlock();
                tb.Text = x;
                tb.TextWrapping = TextWrapping.Wrap;
                rb.Content = tb;
                options.Children.Add(rb);
                rb.Click += Rb_Click;
                Thickness th = rb.Margin;
                th.Right = 10;
                rb.Margin = th;
                rb.Tag = q;
                yes.Tag = q;
                no.Tag = q;
            });
        }

        private void Rb_Click(object sender, RoutedEventArgs e) {
            RadioButton but = (RadioButton)sender;
            Question q = (Question)but.Tag;
            int ans = q.Options.IndexOf((string)but.Content);
            plug.PutResult(q.Word, ans);
            LoadQuestion();
        }

        private void yes_Click(object sender, RoutedEventArgs e) {
            Question q = (Question)yes.Tag;
            plug.PutResult(q.Word, q.Answer);
            LoadQuestion();
        }

        private void no_Click(object sender, RoutedEventArgs e) {
            Question q = (Question)no.Tag;
            plug.PutResult(q.Word, (q.Answer + 1) % 4);
            LoadQuestion();
        }
    }
}
