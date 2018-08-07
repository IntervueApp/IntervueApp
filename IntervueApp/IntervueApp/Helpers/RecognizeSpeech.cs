using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace IntervueApp.Helpers
{
    public class RecognizeSpeech
    {
        public async Task RecognizeSpeechAsync()
        {
            SpeechFactory factory = SpeechFactory.FromSubscription("0970fcf2c9434953ac28784103bc07da", "west-us");

            using (SpeechRecognizer recognizer = factory.CreateSpeechRecognizer())
            {
                Console.WriteLine("Please speak");

                var result = await recognizer.RecognizeAsync();

                if (result.RecognitionStatus != RecognitionStatus.Recognized)
                {
                    Console.WriteLine($"Recognition status: {result.RecognitionStatus.ToString()}");

                    if (result.RecognitionStatus == RecognitionStatus.Canceled)
                    {
                        Console.WriteLine($"There was an error: {result.RecognitionFailureReason}");
                    }
                    else
                    {
                        Console.WriteLine("Unable to recognize speech. \n");
                    }
                }
                else
                {
                    Console.WriteLine($"We recognized: {result.Text}");
                }
            }
        }
    }
}
