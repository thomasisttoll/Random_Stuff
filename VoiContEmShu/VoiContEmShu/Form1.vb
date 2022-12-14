Imports System.Speech
Imports System.Threading

Public Class Form1

    Dim WithEvents recognition As New Recognition.SpeechRecognitionEngine

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        recognition.SetInputToDefaultAudioDevice()

        Dim grammar As New Recognition.SrgsGrammar.SrgsDocument

        Dim grammarRule As New Recognition.SrgsGrammar.SrgsRule("Anti_Raid_Emergency_Shutdowner")

        Dim commands As New Recognition.SrgsGrammar.SrgsOneOf("Computer ausschalten", "Programm beenden", "Bitcoin")

        grammarRule.Add(commands)

        grammar.Rules.Add(grammarRule)

        grammar.Root = grammarRule

        recognition.LoadGrammar(New Recognition.Grammar(grammar))

        recognition.RecognizeAsync()

    End Sub


    Private Sub reco_RecognizeCompleted(ByVal sender As Object, ByVal e As System.Speech.Recognition.RecognizeCompletedEventArgs) Handles recognition.RecognizeCompleted

        recognition.RecognizeAsync()

    End Sub

    Private Sub reco_SpeechRecognized(ByVal sender As Object, ByVal e As System.Speech.Recognition.RecognitionEventArgs) Handles recognition.SpeechRecognized

        Select Case e.Result.Text

            Case "Computer ausschalten"

                Dim synth As New Synthesis.SpeechSynthesizer
                synth.SpeakAsync("Computer wird ausgeschalten.")
                Process.Start("shutdown", "-s -t 5")


            Case "Programm beenden"

                Dim synth As New Synthesis.SpeechSynthesizer
                synth.SpeakAsync("Bis zum nächsten Mal Habibi")
                Thread.Sleep("4000")
                Environment.FailFast("Holzhammermethode weil RCW-Cleanup eh niemanden kümmert")


                '    'Simply add New cases here and the associated command in form1_load
                'Case "Bitcoin"

                '    Dim synth As New Synthesis.SpeechSynthesizer
                '    synth.SpeakAsync("Alle Kryptos werden an 99ass1embler gesendet. Trololololo")



        End Select

    End Sub

End Class
