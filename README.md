# Introduction 
[Intervue](http://intervue.azurewebsite.net)

Intervue transcribes spoken audio into text and allows the user to download the results into their local machine. 
Anonymous users can also use the functionality of the site and download the results into a text file but users could also register. Once they login, they can see their previous transcriptions via blob storage.

---
## Dependencies
This application runs on .NET Core 2.1, which can be downloaded [here](https://www.microsoft.com/net/download/macos).

---
## Build
To build this app locally, install the [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/macos), and clone this repo onto your machine. 
Other applications to download are the Speech SDK, which can be downloaded [here](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/). As of this writing, the version is at 0.5.

From a terminal interface, go to where this was cloned and type the following commands:

```
cd Intervue
dotnet restore
dotnet run
```
---

## Work flow 
We followed an AGILE workflow. We would conjure up user stories and assign tasks. Once the user stories were available, we would choose on a basis of who wants to complete them. This worked out well. The way we would avoid merge conflicts are by communicating to the team what we would be working on and in what branch. Each new branch would be focused on a specific user story. And we would keep pushing into that branch. 
Once one of us completed the user story, the person working on it would initiate a pull request and let the rest of the team know when the pull request was active. This is because we agreed to have one other team member look over and approve it. 
We would then test functionality to see if the staging would still continue on working. If so, we would keep it the way it is. At the end of the day, we would create a pull request into master. Once this pull request was open, this needed approval from two different team members. This assures master would always be functional.

## Walk Through
(With Screenshots)

![Pic 1](https://#)