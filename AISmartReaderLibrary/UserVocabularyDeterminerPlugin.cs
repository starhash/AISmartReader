using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using LemmaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordNetPlugin;

namespace AISmartReaderLibrary {
    public class UserVocabularyDeterminerPlugin {
        ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.English);
        
        Random r;
        static Dictionary<string, int> _words;
        public static Dictionary<string, int> Words { get { return _words; } }
        int testIndex = -1;
        int testCount = 20;

        List<string> answers;
        Dictionary<string, bool> questions;
        string curr = null;
        int answer = -1;
        int sel = -1;

        static UserVocabularyDeterminerPlugin() {
            string[] lines = File.ReadAllLines(Path.Combine(UserPreferences.Base, "data\\clustered.prop"));
            _words = new Dictionary<string, int>();
            foreach (string s in lines) {
                int idx = s.IndexOf(',');
                _words.Add(s.Substring(0, idx), int.Parse(s.Substring(idx + 1).Trim()));
            }
        }

        public UserVocabularyDeterminerPlugin(int testCount = 20) {
            r = new Random();
            questions = new Dictionary<string, bool>();
            answers = new List<string>();
            this.testCount = testCount;
        }

        public Tuple<string, Set<SynSet>> GetNonEmpty() {
            string w;
            Set<SynSet> s;
            do {
                w = _words.Keys.ElementAt(r.Next(_words.Count));
                s = Plugin.GetSynSetsFor(lmtz.Lemmatize(w.ToLower()));
            } while (s.Count == 0);
            return new Tuple<string, Set<SynSet>>(w, s);
        }

        public void LoadQuestion() {
            if (testIndex < testCount) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write((questions.Count + 1) + ". ");
                List<Tuple<string, Set<SynSet>>> four = new List<Tuple<string, Set<SynSet>>>();
                for (int i = 0; i < 4; i++) {
                    four.Add(GetNonEmpty());
                }
                answer = r.Next(4);
                curr = four[answer].Item1;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[ " + four[answer].Item1 + " ] ?");
                Console.ForegroundColor = ConsoleColor.Blue;
                for (int i = 0; i < 4; i++) {
                    string gloss = four[i].Item2.ElementAt(0).Gloss;
                    if (gloss.IndexOf("; \"") != -1)
                        gloss = gloss.Substring(0, gloss.IndexOf("; \""));
                    Console.WriteLine("    " + (i + 1) + ".\t" + gloss);
                }
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\nEnter [1-4]: ");
            }
        }

        public Question GetQuestion() {
            if (testIndex < testCount) {
                Question question = new Question();
                List<Tuple<string, Set<SynSet>>> four = new List<Tuple<string, Set<SynSet>>>();
                for (int i = 0; i < 4; i++) {
                    four.Add(GetNonEmpty());
                }
                for (int i = 0; i < 4; i++) {
                    string gloss = four[i].Item2.ElementAt(0).Gloss;
                    if (gloss.IndexOf("; \"") != -1)
                        gloss = gloss.Substring(0, gloss.IndexOf("; \""));
                    question.Options.Add(gloss);
                }
                answer = r.Next(4);
                question.Answer = answer;
                curr = four[answer].Item1;
                question.Word = curr;
                return question;
            }
            return null;
        }

        public void PutResult(string word, int option) {
            bool res = option == answer;
            questions.Add(word, res);
            testIndex++;
        }

        private void Next() {
            if (testIndex < testCount) {
                LoadQuestion();
                sel = int.Parse(Console.ReadLine())-1;
                bool b = sel == answer;
                if (b) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct");
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong");
                }
                questions.Add(curr, b);
                testIndex++;
            } else {
                Console.Write("Test Completed.");
                Calculate();
            }
        }

        public int Test(int count) {
            for(int i = 0; i < count; i++) {
                Next();
            }
            return Calculate();
        }

        public int Calculate() {
            int[] count = new int[5];
            questions.Keys.ToList().ForEach(x => {
                count[Words[x]]++;
            });
            Dictionary<string, bool> filter = questions.Where((x) => x.Value == false).ToDictionary((x) => x.Key, (y) => y.Value, questions.Comparer);
            List<int> labels = filter.Keys.Select((x) => _words[x]).ToList();
            int[] mcount = new int[5];
            for(int i = 0; i < 5; i++) {
                mcount[i] = labels.Where(x => x == i).Count();
            }
            int mode = mcount.Max();
            //int mode = labels.GroupBy(v => v).OrderByDescending(g => g.Count()).First().Key;
            return mode;
        }
    }

    public class Question {
        public string Word { get; set; }
        public List<string> Options { get; set; }
        public int Answer { get; set; }

        public Question() {
            Options = new List<string>();
        }

    }
}
