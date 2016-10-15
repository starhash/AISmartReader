using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using LemmaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordNetPlugin;

namespace UserTest {
    public partial class Form1 : Form {
        ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.English);

        public class WordSet {
            public string Word;
            public string Properties;
        }

        Random r;
        List<WordSet> _words;
        int testIndex = -1;
        int testCount = 20;

        List<RadioButton> answers;
        Dictionary<WordSet, bool> questions;
        WordSet curr = null;
        int answer = -1;
        int sel = -1;

        public Form1() {
            InitializeComponent();
            string[] lines = File.ReadAllLines(@"C:\Users\harsh\Documents\Visual Studio 2015\Projects\AISmartReader\AISmartReader\Pramodith\words-lim-len4_output.csv");
            _words = new List<WordSet>();
            foreach(string s in lines) {
                int idx = s.IndexOf(',');
                _words.Add(new WordSet() { Word = s.Substring(0, idx), Properties = s.Substring(idx + 1) });
            }
            r = new Random();
            questions = new Dictionary<WordSet, bool>();
            answers = new List<RadioButton>();
            answers.Add(radioButton1);
            answers.Add(radioButton2);
            answers.Add(radioButton3);
            answers.Add(radioButton4);
        }

        public Tuple<WordSet, Set<SynSet>> GetNonEmpty() {
            WordSet w;
            Set<SynSet> s;
            do {
                w = _words[r.Next(_words.Count)];
                s = Plugin.GetSynSetsFor(lmtz.Lemmatize(w.Word.ToLower()));
            } while (s.Count == 0);
            return new Tuple<WordSet, Set<SynSet>>(w, s);
        }

        public void LoadQuestion() {
            if (testIndex < testCount) {
                List<Tuple<WordSet, Set<SynSet>>> four = new List<Tuple<WordSet, Set<SynSet>>>();
                for (int i = 0; i < 4; i++) {
                    four.Add(GetNonEmpty());
                }
                radioButton1.Text = four[0].Item2.ElementAt(0).Gloss;
                radioButton2.Text = four[1].Item2.ElementAt(0).Gloss;
                radioButton3.Text = four[2].Item2.ElementAt(0).Gloss;
                radioButton4.Text = four[3].Item2.ElementAt(0).Gloss;
                answer = r.Next(4);
                curr = four[answer].Item1;
                word.Text = curr.Word;
                submit.Enabled = true;
                start.Enabled = false;
            }
        }

        private void start_Click(object sender, EventArgs e) {
            if(testIndex < testCount) {
                LoadQuestion();
            } else {
                word.Text = "Test Completed.";
                Calculate();
            }
        }

        public void Calculate() {
            Dictionary<string, bool> qfilter = new Dictionary<string, bool>();
            foreach (WordSet s in questions.Keys)
                qfilter.Add(s.Word+" = "+ s.Properties.Substring(s.Properties.LastIndexOf(',') + 1), questions[s]);
            Dictionary<WordSet, bool> filter = questions.Where((x) => x.Value == false).ToDictionary((x) => x.Key, (y) => y.Value, questions.Comparer);
            List<int> labels = filter.Keys.Select((x) => int.Parse(x.Properties.Substring(x.Properties.LastIndexOf(',')+1))).ToList();
            int mode = labels.GroupBy(v => v).OrderByDescending(g => g.Count()).First().Key;
            word.Text = "Test Complete. Level : " + mode;
        }

        private void submit_Click(object sender, EventArgs e) {
            if (sel >= 0) {
                RadioButton r = answers[sel];
                bool b = sel == answer;
                if(b) {
                    ans.Text = "OK";
                } else {
                    ans.Text = "Wrong";
                }
                questions.Add(curr, b);
                submit.Enabled = false;
                start.Enabled = true;
                start.Text = "Next";
                testIndex++;
                LoadQuestion();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            sel = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            sel = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            sel = 2;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            sel = 3;
        }
    }
}
