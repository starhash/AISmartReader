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
        private double[] _properties = new double[2];
        private int _cluster;

        public double Syllables { get { return _properties[0]; } }
        public double Frequency { get { return _properties[1]; } }
        public int Cluster { get { return _cluster; } set { _cluster = value; } }
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
            string[] lines = File.ReadAllLines(Path.Combine(UserPreferences.Base, "data\\clustered.prop"));
            int i = 0;
            foreach (string line in lines) {
                try {
                    string[] fields = line.Split(',');
                    var skip1take2 = fields.Skip(1).Take(2);
                    var skip3take1 = fields.Skip(3).Take(1);
                    _words.Add(fields[0], new WordProperties() {
                        Data = skip1take2.Select((x) => double.Parse(x)).ToArray(),
                        Cluster = int.Parse(skip3take1.ElementAt(0))
                    });
                } catch (Exception e) {
                    i++;
                }
            }
        }
    }
}
