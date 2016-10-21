using org.neuroph.core;
using org.neuroph.nnet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISmartReaderLibrary {
    public class WordProperties {
        private double[] _properties = new double[4];
        private string _nlpProperties;

        public double Length { get { return _properties[0]; } }
        public double Frequency { get { return _properties[1]; } }
        public double Phenomes { get { return _properties[2]; } }
        public double Syllables { get { return _properties[3]; } }
        public string NLPProperties { get { return _nlpProperties; } set { _nlpProperties = value; } }
        public double[] Data { set { _properties = value; } get { return _properties; } }
    }

    public static class WordHighlighter {
        static Dictionary<string, WordProperties> _words = new Dictionary<string, WordProperties>();
        public static Dictionary<string, WordProperties> Words { get { return _words; } }

        static WordHighlighter() {
            try {
                LoadWords();
            } catch (FileNotFoundException fnfe) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(fnfe.Message);
            }
        }

        public static void LoadWords() {
            string[] lines = File.ReadAllLines(Path.Combine(UserPreferences.Base, "data\\words.prop"));
            foreach(string line in lines) {
                string[] fields = line.Split(',');
                _words.Add(fields[0], new WordProperties() { Data = fields.Skip(1).Take(4).Select((x) => double.Parse(x)).ToArray(), NLPProperties = fields[5] });
            }
        }
    }
}
