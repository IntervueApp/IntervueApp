# Software Requirements

---
## Vision
Web application that uses the Microsoft Speaker Recognition API to distinguish the voice of someone talking, and send the voice data captured through the Microsoft Speech to Text API, in order to transcribe what the speaker is saying in real time.

There are many professions that require speech to be transcribed, or could benefit from a transcribing service. These range from lawyers in courtrooms and business offices, to politicians and lawmakers, to even customer support representatives in call centers.

This application can automate the tedious work of having to remember what was said multiple hours earlier in a certain interaction, and comprising emails based on what one could remember. This application can also reduce the workload of professional transcribers who must work at a frantic pace to keep up with real time conversations between individuals or groups.

This can make our interactions feel more seamless, especially when seeking assistance from customer service reps over the phone. No longer will there be a worry that the representative has forgotten something, or a detail left out from a summary email sent to the client, which could make or break a solution for the client.

Being able to keep track of who said what, in real time and with a high degree of accuracy, is paramount to a large number of jobs that workers have to perform every day. Simplifying that process allows those workers to spend more human hours working on more meaningful tasks; ones that aren’t as easily automated.

---
## Scope (In/Out)
**IN** - What will your product do. Describe the individual features that your product will do.
 - Provide a registration feature for users to create an account / login feature if they already have an account. This will give them access to the transcription functionality
 - Provide a set of pages where users can alter their account settings (calibrate/re-calibrate voice recognition data, change name, change email, add a profile picture)
 - Allow users to calibrate their voice in our application, thus setting up speaker recognition
 - Allow users to start a recording, thus initializing speech to text
 - Display text in real time as the user is speaking into their computer’s microphone
 - Display user’s name or profile picture, if one is uploaded, alongside the text, in order to differentiate who the speaker of a specific text block is
 - Display another user’s name (or profile picture) on a separate line or text block, if a different speaker begins talking into the computer’s microphone
 - Store text transcriptions in a user’s account so that they can view the transcriptions of later on
 - Allow users to delete text transcriptions of their previous recordings
 - Allow users to edit their text transcriptions manually, with their mouse and keyboard, both in real time and for previous recordings
 - Allow users to send emails containing the text transcriptions of previous recordings, by formatting the email and inputting the email address where the message will be sent, all within the web application

**OUT** - What will your product not do. These should be features that you will make very clear from the beginning that you will not do during development. HINT: Pick your battles wisely. This should only be 1 or 2 things.
 - Store voice recordings
 - Perform spell checking operations
 - Allow access to CRUD operations for users who are not logged in
 - Allow access to calibrate voice or start recordings for users who are not logged in

---
## MVP
A web application consisting of a front (HTML/CSS) and back end (C#), following the ASP.NET Core MVC standards, which allows users to transcribe their voice to text in real time. The application will use Microsoft Speaker Recognition and Speech To Text APIs. 

Users will be able to create accounts and manage their settings within the application (calibrate/re-calibrate voice recognition data, change name, change email, add a profile picture) and have these displayed in a page personalized for each user. All user account settings, along with previous saved transcription, will be saved in an SQL Database. 

Users will be able to perform full CRUD operations on only their data. Administrators will be able to perform full CRUD operations on any user’s data. Logged in users will have access to a page where they can start a recording, and upon speaking, their name (or profile picture if one exists) will be output alongside their transcribed words in real time. 

If someone else speaks into the computer’s microphone while a user has started a recording, that other person’s name will appear alongside the text block of their transcribed speech, if that other person’s speaker recognition data is present in our databases, otherwise it will show that an “unknown” speaker is responsible for that text block. Users will be able to save to their account, for future reference.

---
## Stretch
What stretch goals are you going to aim for?
1. Users will be able to alter the text transcriptions manually, with mouse and keyboard, after a recording has stopped, or for saved transcriptions.
2. Users will be able to alter the text transcriptions manually, with mouse and keyboard, in real time while the recording is happening.
3. Users will be able to format and send emails directly from within the web application, containing any of their text transcriptions.
4. Users will be able to see their rate of speech (words/minute)displayed to them as they’re talking, and on completed recordings.
5. Users will have a checkbox in their account settings page to set whether or not they want their rate of speech (words/minute) to be displayed. *** The rate of speech will still be saved in the database for each recording, but only displayed if they have opted for it in their settings. ***

---
## Functional Requirements
List the functionality of your product. This will consist of tasks such as the following:

A user can log in through standard means or via a 3rd party account
A user can calibrate/re-calibrate voice recognition data, change name, change email, and add a profile picture
A user can start a recording so that their speech will be transcribed in real time
They can save the transcription if they would like
Transcriptions are not saved by default
A user can do the following with transcriptions recorded under their account
Read previous transcription
Update transcriptions manually (mouse and keyboard)
Delete transcriptions
Send emails with transcriptions in the body of the message
A user can search by date, and see all of their transcriptions recorded that day

---
## Non-Functional Requirements
Security: Users must log in to use the service.  User can access previously stored transcriptions.  No other unauthorized users can access their account and vice versa.

Usability: The user is able to arrive on a page without encountering an error. It is possible to accomplish any task with a keyboard and mouse. 

Data Integrity: Data should be easy to understand. Data should be recorded as it was observed and at the time it was entered or updated. Data will be stored in SQL databases hosted on Azure.  Azure blob storage will be used to store user information like their voice, profile picture and text transcriptions.

---
## Data Flow
![Data flow]()

---
## Database Schema
![Database Schema]()
