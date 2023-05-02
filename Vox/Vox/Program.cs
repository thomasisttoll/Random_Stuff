using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vox
{
    internal class Program
    {
        static SpeechRecognitionEngine recognition = new SpeechRecognitionEngine();

        static void Main(string[] args)
        {
            recognition.SetInputToDefaultAudioDevice();

            SrgsDocument grammar = new SrgsDocument();
            SrgsRule grammarRule = new SrgsRule("Anti_Raid_Emergency_Shutdowner");
            SrgsOneOf commands = new SrgsOneOf("Computer ausschalten", "Programm beenden", "Bitcoin");

            grammarRule.Add(commands);
            grammar.Rules.Add(grammarRule);
            grammar.Root = grammarRule;

            recognition.LoadGrammar(new Grammar(grammar));
            recognition.RecognizeAsync(RecognizeMode.Multiple);

            recognition.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizedHandler);
            recognition.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(RecognizeCompletedHandler);

            Console.WriteLine("Sprechen Sie einen der folgenden Befehle:");
            Console.WriteLine("- Computer ausschalten");
            Console.WriteLine("- Programm beenden");
            Console.WriteLine("- Bitcoin");

            Console.ReadLine();
        }

        static void RecognizeCompletedHandler(object sender, RecognizeCompletedEventArgs e)
        {
            recognition.RecognizeAsync(RecognizeMode.Multiple);
        }

        static void SpeechRecognizedHandler(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Computer ausschalten":
                    SpeechSynthesizer synth1 = new SpeechSynthesizer();
                    synth1.SpeakAsync("Computer wird ausgeschaltet.");
                    Process.Start("shutdown", "-s -t 50");
                    break;

                case "Programm beenden":
                    SpeechSynthesizer synth2 = new SpeechSynthesizer();
                    synth2.SpeakAsync("Bis zum nächsten Mal Habibi");
                    Thread.Sleep(4000);
                    Environment.FailFast("Holzhammermethode weil RCW-Cleanup eh niemanden kümmert");
                    break;

                case "Bitcoin":
                    SpeechSynthesizer synth3 = new SpeechSynthesizer();
                    synth3.SpeakAsync("Alle Kryptos werden an 99ass1embler gesendet. Trololololo");
                    break;
            }
        }
    }
}
