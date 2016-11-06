
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AISmartReaderLibrary;
using org.neuroph.nnet;
using org.neuroph.core.data;
using org.neuroph.nnet.learning;
using System.IO;
using InfinityX.Grammar;

namespace UserTest {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            UserPreferences user = new UserPreferences();
            user.LoadUserPreference(2);

            string text = File.ReadAllText(@"C:\\Users\\harsh\\Documents\\Visual Studio 2015\\Projects\\AISmartReader\\Readifine\\bin\\Debug\\data\\Pygmalion.txt");
            WordFilter filter = new WordFilter(user);
            List<TaggedWord> tokens = filter.TagDocumentText(text);
            foreach(TaggedWord word in tokens) {
                if(word.Tag == WordTag.Unknown) {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                } else {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(word.Word);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            Console.ReadKey();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}