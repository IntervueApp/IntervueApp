using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intervue.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using System.Text;
using System.IO;

namespace Intervue.Controllers
{
    public class TranscriptionController : Controller
    { 
        [HttpGet]
        public async Task<IActionResult> Speech()
        {
            SpeechViewModel svm = await EnableSpeechRecognition();            

            return View(svm);
        }

        [HttpPost]
        public IActionResult Speech(SpeechViewModel svm)
        {
            DownloadTextFile(svm.FileName, svm.ResultMessage);

            return RedirectToAction("Index", "Home");
        }

        private static async Task<SpeechViewModel> EnableSpeechRecognition()
        {
            SpeechViewModel svm = new SpeechViewModel();

            SpeechFactory factory = SpeechFactory.FromSubscription("a5a9e9b4c6164808be0c34ccd4d1e598", "westus");

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
                    svm.ResultMessage = $"{result.Text}";
                }

                return svm;
            }
        }

        private static async void DownloadTextFile(string fileName, string fileText)
        {
            string newDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            StreamWriter outputFile = new StreamWriter(Path.Combine(newDocPath, (fileName + ".txt")));

            await outputFile.WriteAsync(fileText);

            outputFile.Flush();
        }

    }
}