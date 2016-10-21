using InfinityX.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LemmaSharp;

namespace AISmartReaderLibrary {
    public class WordFilter {
        UserPreferences preferences;

        public WordFilter(UserPreferences pref) {
            preferences = pref;
        }

        public List<TaggedWord> TagDocumentText(string text) {
            List<TaggedWord> list = new List<TaggedWord>();
            XRetractableBufferedTokenStream stream = new XRetractableBufferedTokenStream(text);
            Regex regex = new Regex("[A-za-z]+");
            LemmatizerPrebuiltCompact com = new LemmatizerPrebuiltCompact(LanguagePrebuilt.English);
            while (stream.HasNext()) {
                XToken token = stream.ReadToken();
                string word = token.Value;
                word = com.Lemmatize(word);
                if(regex.IsMatch(word)) {
                    if (!preferences.IsWordKnown(word))
                        list.Add(new TaggedWord() { Tag = WordTag.Unknown, Word = token.Value });
                    else
                        list.Add(new TaggedWord() { Tag = WordTag.Known, Word = token.Value });
                } else {
                    list.Add(new TaggedWord() { Tag = WordTag.Known, Word = token.Value });
                }
            }
            return list;
        }
    }

    public class TaggedWord {
        public WordTag Tag { get; set; }
        public string Word { get; set; }
    }

    public enum WordTag {
        Known,
        Unknown
    }
}
