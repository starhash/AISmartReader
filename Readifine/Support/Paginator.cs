using AISmartReaderLibrary;
using InfinityX.Grammar;
using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using LemmaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WordNetPlugin;

namespace Readifine.Support {
    public class Paginator {
        ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LanguagePrebuilt.English);
        XRetractableBufferedTokenStream stream;
        List<XRetractionBufferState> pages;

        public delegate void RetrainRequest(string request);
        public event RetrainRequest OnRetrainRequested;

        public Paginator(string text) {
            stream = new XRetractableBufferedTokenStream(text);
            pages = new List<XRetractionBufferState>();
        }

        public void BackFill(System.Windows.Controls.RichTextBox rtb, UserPreferences preferences) {
            if (pages.Count > 1) {
                rtb.Document.Blocks.Clear();
                stream.Retract(pages[pages.Count - 2]);
                pages.RemoveAt(pages.Count - 1);
                Paragraph curr = new Paragraph();
                FlowDocument doc = new FlowDocument();
                int limit = 1024, c = 0;
                XRetractableBufferStreamPosition last = stream.Position;
                rtb.Document.Blocks.Add(curr);
                while (c < limit && stream.HasNext()) {
                    XToken token = stream.ReadToken();
                    if (last.Line < stream.Position.Line || curr == null) {
                        if (curr == null) {
                            curr = new Paragraph(new LineBreak());
                            rtb.Document.Blocks.Add(curr);
                        }
                    }
                    Run r = new Run(token.Value);
                    if (!preferences.IsWordKnown(token.Value)) {
                        r.FontStyle = FontStyles.Italic;
                        r.Background = new SolidColorBrush(Colors.LightGray);
                        r.Foreground = new SolidColorBrush(Colors.DarkRed);
                    }
                    curr.Inlines.Add(r);
                    Run r2 = new Run(((stream.IsMatchLookAhead("[A-Za-z]+")) ? " " : stream.ReadToken().Value + " "));
                    curr.Inlines.Add(r2);
                    c++;
                }
            }
        }

        public void Fill(System.Windows.Controls.RichTextBox rtb, UserPreferences preferences) {
            rtb.Document.Blocks.Clear();
            string richText = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
            FlowDocument doc = new FlowDocument();
            int limit = 1024, c = 0;
            XRetractableBufferStreamPosition last = stream.Position;
            XRetractionBufferState state = stream.StateNode();
            if(!pages.Contains(state))
                pages.Add(stream.StateNode());
            Paragraph curr = new Paragraph();
            Run currr = new Run();
            curr.Inlines.Add(currr);
            rtb.Document.Blocks.Add(curr);
            while(c < limit && stream.HasNext()) {
                XToken token = stream.ReadToken();
                if(last.Line < stream.Position.Line || curr == null) {
                    if(curr == null) {
                        curr = new Paragraph(new LineBreak());
                        rtb.Document.Blocks.Add(curr);
                    }
                }
                if(!preferences.IsWordKnown(token.Value)) {
                    Run r = new Run(token.Value);
                    HighLightWord(rtb, token.Value, r);
                    curr.Inlines.Add(r);
                    currr = new Run();
                    curr.Inlines.Add(currr);
                } else {
                    currr.Text += token.Value;
                }
                currr.Text += ((!stream.IsMatchLookAhead("[A-Za-z]+")) ? stream.ReadToken().Value + " " : " ");
                c++;
            }

        }

        public void HighLightWord(RichTextBox rtb, string word, Run r) {
            r.FontStyle = FontStyles.Italic;
            r.Background = new SolidColorBrush(Colors.LightGray);
            r.Foreground = new SolidColorBrush(Colors.DarkRed);

            ToolTip t = new ToolTip();
            t.FontFamily = new FontFamily("Segoe UI");
            t.FontSize = 12;
            if (WordHighlighter.Words.ContainsKey(word)) {
                Set<SynSet> sets = Plugin.GetSynSetsFor(lmtz.Lemmatize(word));
                if (sets.Count == 0) return;
                SynSet s = sets.ElementAt(0);
                t.Content = word + "\n" + s.Gloss;
                r.ToolTip = t;
            }

            r.MouseEnter += (object sender, System.Windows.Input.MouseEventArgs e) => {
                Run run = (Run)sender;
                
                string lmz = lmtz.Lemmatize(run.Text);
                var synsets = Plugin.GetSynSetsFor(lmz);
                string pops = "";
                Popup pop = new Popup();
                Point point = e.GetPosition(rtb);
                TextPointer loc = rtb.GetPositionFromPoint(point, true);
                rtb.CaretPosition = loc;
                TextPointer loc2 = loc.GetPositionAtOffset(-1);
                TextRange tr = new TextRange(loc2, loc);
                while (tr != null && tr.Text != " ") {
                    loc = loc2;
                    loc2 = loc.GetPositionAtOffset(-1);
                    if (loc != null && loc2 != null)
                        tr = new TextRange(loc, loc2);
                    else
                        break;
                }
                rtb.CaretPosition = loc;
                Point poffset = rtb.CaretPosition.GetCharacterRect(LogicalDirection.Forward).Location;
                pop.PlacementTarget = rtb;
                pop.Placement = PlacementMode.Relative;
                pop.Effect = new System.Windows.Media.Effects.DropShadowEffect() { BlurRadius = 8, Color = Color.FromArgb(80, 0, 0, 0), ShadowDepth = 8 };
                Border b = new Border();
                b.MinWidth = 128;
                b.BorderBrush = new SolidColorBrush(Colors.Black);
                b.BorderThickness = new Thickness(1);
                b.Background = new SolidColorBrush(Colors.LightYellow);
                b.Padding = new Thickness(4);
                Grid g = new Grid();
                TextBlock tb = new TextBlock() { Text = r.Text, Margin = new Thickness(4, 4, -1, -1), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                g.Children.Add(tb);
                Button no = new Button() { Content = "×", FontFamily = new FontFamily("Segoe UI Symbol"), Width = 24, Height = 24, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(-1, 4, 4, -1), BorderBrush = null, BorderThickness = new Thickness(1) };
                g.Children.Add(no);
                no.Tag = r.Text;
                no.Click += (object senderr, RoutedEventArgs ev) => {
                    Button se = (Button)senderr;
                    OnRetrainRequested?.Invoke((string)se.Tag);
                };
                ListBox l = new ListBox();
                l.Margin = new Thickness(4, 44, 4, 4);
                l.Height = 64;
                g.Children.Add(l);
                new Action((/* LOAD SYN */) => {
                    if (synsets.Count != 0) {
                        foreach (var syn in synsets) {
                            var rep = syn.Words[0];
                            if (lmtz.Lemmatize(rep) != lmz) {
                                pops = rep;
                            } else {
                                rep = syn.Gloss;
                                if (rep.IndexOf(';') != -1) rep = rep.Substring(0, rep.IndexOf(';'));
                                pops = run.Text + " (" + rep + ")";
                            }
                            l.Items.Add(pops);
                        }
                        run.Foreground = new SolidColorBrush(Colors.MediumBlue);
                    } else {
                        pops = run.Text + "?";
                        run.Foreground = new SolidColorBrush(Colors.Red);
                        l.Items.Add(pops);
                    }
                    //new TextBlock() { Text = pops, Background = new SolidColorBrush(Colors.LightYellow), Foreground = new SolidColorBrush(Colors.Black) }
                }).Invoke();
                b.Child = g;
                pop.Child = b;
                int x = (int)Math.Round(point.X - poffset.X) + 5;
                int y = (int)Math.Round(point.Y - poffset.Y) + 5;
                pop.HorizontalOffset = point.X - x;
                pop.VerticalOffset = point.Y - y;
                pop.IsOpen = true;
                pop.Tag = new { Word = r.Text, Run = r };
                pop.MouseLeave += (object senders, System.Windows.Input.MouseEventArgs evt) => {
                    Popup popp = (Popup)senders;
                    dynamic bt = ((dynamic)popp.Tag);
                    Run runt = bt.Run;
                    runt.Text = bt.Word;
                    runt.Background = new SolidColorBrush(Colors.LightGray);
                    runt.Foreground = new SolidColorBrush(Colors.DarkRed);
                    popp.IsOpen = false;
                };
            };
        }
    }
}
