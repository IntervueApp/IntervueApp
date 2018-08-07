using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntervueApp.Helpers;
using Microsoft.CognitiveServices.Speech;
using IntervueApp.Models.ViewModels;

namespace IntervueApp.Controllers
{
    public class TranscriptionController : Controller
    {
        SpeechFactory factory = SpeechFactory.FromSubscription("a5a9e9b4c6164808be0c34ccd4d1e598", "westus");

        [HttpGet]
        public async Task<IActionResult> Speech()
        {
            SpeechViewModel svm = new SpeechViewModel();

            // Creates an instance of a speech factory with specified
            // subscription key and service region. Replace with your own subscription key
            // and service region (e.g., "westus").
            // Creates a speech recognizer.
            using (var recognizer = factory.CreateSpeechRecognizer())
            {
                svm.PromptMessage = "Say something...";

                // Performs recognition.
                // RecognizeAsync() returns when the first utterance has been recognized, so it is suitable 
                // only for single shot recognition like command or query. For long-running recognition, use
                // StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeAsync();

                // Checks result.
                if (result.RecognitionStatus != RecognitionStatus.Recognized)
                {
                    Console.WriteLine($"Recognition status: {result.RecognitionStatus.ToString()}");
                    if (result.RecognitionStatus == RecognitionStatus.Canceled)
                    {
                        svm.ResultMessage = $"There was an error, reason: {result.RecognitionFailureReason}";
                    }
                    else
                    {
                        svm.ResultMessage = "No speech could be recognized.\n";
                    }
                }
                else
                {
                    svm.ResultMessage = $"We recognized: {result.Text}";
                }
            }

            return View(svm);
        }

        [HttpPost]
        public IActionResult Speech(int id)
        {
            return RedirectToAction("Index", "Home");
        }
        

    }
}