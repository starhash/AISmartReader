﻿using org.neuroph.core;
using org.neuroph.core.data;
using org.neuroph.nnet;
using org.neuroph.nnet.learning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISmartReaderLibrary {
    public class UserPreferences {
        public int Level { get; set; }
        static string _base = Directory.GetCurrentDirectory();
        public static string Base { get { return _base; } }
        MultiLayerPerceptron predictionNN;

        public bool LoadUserPreference(int level) {

            bool b;
            if (b = (predictionNN = LoadNeuralPref()) == null) {
                UserVocabularyDeterminerPlugin test = new UserVocabularyDeterminerPlugin();
                DataSet ds = new DataSet(4, 1);
                foreach (string word in WordHighlighter.Words.Keys) {
                    ds.addRow(new DataSetRow(WordHighlighter.Words[word].Data, new double[] { (UserVocabularyDeterminerPlugin.Words[word] <= level) ? 1 : 0 }));
                }
                ds.shuffle();
                predictionNN = new MultiLayerPerceptron(4, 3, 1);
                MomentumBackpropagation mombp = new MomentumBackpropagation();
                mombp.MaxError = 0.004;
                mombp.LearningRate = 0.2;
                mombp.Momentum = 0.7;
                predictionNN.LearningRule = mombp;
                predictionNN.learn(ds);
                predictionNN.pauseLearning();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Saving neural network:");
                FlushToDisk();
            }
            predictionNN.pauseLearning();
            return true;
        }
        
        private MultiLayerPerceptron LoadNeuralPref() {
            if (File.Exists(Path.Combine(_base, "data\\user.pref"))) {
                return (MultiLayerPerceptron)NeuralNetwork.Load(Path.Combine(_base, "data\\user.pref"));
            } else {
                return null;
            }
        }

        public bool IsWordKnown(string word) {
            if (!WordHighlighter.Words.ContainsKey(word))
                return true;
            //predictionNN.Input = WordHighlighter.Words[word].Data;
            //predictionNN.calculate();
            //var result = predictionNN.Output[0];
            //return result > 0.5;
            return UserVocabularyDeterminerPlugin.Words[word] <= Level;
        }

        public void ReTrain(string word, bool knownStatus) {
            predictionNN.resumeLearning();
            DataSet ds = new DataSet(4, 1);
            ds.addRow(new DataSetRow(WordHighlighter.Words[word].Data, new double[] { (knownStatus) ? 1 : 0 }));
            predictionNN.learn(ds);
            predictionNN.pauseLearning();
        }

        public void FlushToDisk() {
            predictionNN.Save(Path.Combine(Base, "data\\user.pref"));
        }
    }
}