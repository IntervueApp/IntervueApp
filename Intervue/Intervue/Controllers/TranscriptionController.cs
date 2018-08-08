using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intervue.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;

namespace Intervue.Controllers
{
    public class TranscriptionController : Controller
    {
        SpeechFactory factory = SpeechFactory.FromSubscription("a5a9e9b4c6164808be0c34ccd4d1e598", "westus");

        [HttpGet]
        public async Task<IActionResult> Speech()
        {
            // Create SpeechViewModel in order to pass captured speech
            SpeechViewModel svm = new SpeechViewModel
            {
                PromptMessage = "Say something..."
            };

            // Create SpeechRecognizer to accept audio input and parse words from the audio
            SpeechRecognizer recognizer = factory.CreateSpeechRecognizer();

            // Listen for audio input from the user
            var result = await recognizer.RecognizeAsync();
            
            // Act on audio input from the user
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

            return View(svm);
        }

        [HttpPost]
        public IActionResult Speech(int id)
        {
            return RedirectToAction("Index", "Home");
        }




    }
}