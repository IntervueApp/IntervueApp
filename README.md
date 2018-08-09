# Introduction 
[Intervue](http://#) 

Intervue transcribes spoken audio into text and allows the user to download the results into their local machine. 
Anonymous users can also use the functionality of the site and download the results into a text file but users could also register. Once they login, they can see their previous transcriptions via blob storage.

---
## Dependencies
This application runs on .NET Core 2.1, which can be downloaded [here](https://www.microsoft.com/net/download/macos).

---
## Build
To build this app locally, install the [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/macos), and clone this repo onto your machine. 
Other applications are the Speech SDK, which can be downloaded [here](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/). As of this writing, the version is at 0.5.

From a terminal interface, go to where this was cloned and type the following commands:

```
cd Sourcd
dotnet restore
dotnet run
```
---

## Work flow. 
User stories. What we worked on. Avoid merge conflicts. Originally tried bing.speech.api. Merged to staging based on functionality. And pull from staging to test it. Once tested out satisfactorily, push to master at end of day. Master was always functional.

## Walk Through
(With Screenshots)

![Pic 1](https://#)