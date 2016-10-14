using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordNetPlugin {
    public class Plugin {
        static WordNetEngine engine = new WordNetEngine(@"C:\Users\harsh\Downloads\ptl.sys.virginia.edu\msg8u\NLP\Source\ResourceAPIs\wn3.1.dict\dict", false);
        
        public static Set<SynSet> GetSynSetsFor(string word) {
            try {
                return engine.GetSynSets(word);
            } catch (Exception) { return null; }
        }
    }
}
