using System;
using System.Threading.Tasks;
using Intervue.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using System.IO;

namespace Intervue.Controllers
{
    public class TranscriptionController : Controller
    {
        /// <summary>
        /// This will instantiate the SpeechViewModel for the EnableSpeechRecognition. The variable name is svm and will return the results, prompt and file name on the View.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Speech()
        {
            SpeechViewModel svm = await EnableSpeechRecognition();

            return View(svm);
        }

        /// <summary>
        /// This will download the text file based on the SpeechViewModel. The two parameters of DownloadTextFile are the two properties in the SpeechViewModel, file name and result message. This will then go to the Index View of the Home folder.
        /// </summary>
        /// <param name="svm"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Speech(SpeechViewModel svm)
        {
            DownloadTextFile(svm.FileName, svm.ResultMessage);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This method called EnableSpeechRecognition takes in the type, SpeechViewModel. It will first assign a value to prompt message. Then the SpeechFactory from the Speech SDK will have the FromSubscription function, which 'creates an instance of the speech factory with specified subscription key and region.'
        /// </summary>
        /// <returns></returns>
        private static async Task<SpeechViewModel> EnableSpeechRecognition()
        {
            SpeechViewModel svm = new SpeechViewModel
            {
                PromptMessage = "Say something..."
            };

            SpeechFactory factory = SpeechFactory.FromSubscription("a5a9e9b4c6164808be0c34ccd4d1e598", "westus");

            // Creates a SpeechRecognizer to accept audio input from the user
            SpeechRecognizer recognizer = factory.CreateSpeechRecognizer();

            // Accepts audio input from the user to recognize speech
            SpeechRecognitionResult result = await recognizer.RecognizeAsync();

            // Acts on recognized speech from audio input
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

        /// <summary>
        /// This will download the text file. First, this will look for the folderpath in the user's computer. In this method, it is the MyDocuments folder. Next, it will create a .txt. file via streamwriter into MyDocuments. It then awaits for the results to show up and write asynchronously into the text file. The output then flushes or clears buffers for current writer and causes any buffered data to be written to underlying stream.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileText"></param>
        private static async void DownloadTextFile(string fileName, string fileText)
        {
            string newDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            StreamWriter outputFile = new StreamWriter(Path.Combine(newDocPath, (fileName + ".txt")));

            await outputFile.WriteAsync(fileText);

            outputFile.Flush();
        }
    }
}