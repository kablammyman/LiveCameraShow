# LiveCameraShow
LiveCameraShow is a C# app I made for a party a few years back...most of the interesting code is probably from MSDN or some other internet source.

Ya see, I was hosting a NYE party, and I wanted a "photo booth" to be a fun thing to help us ring in the New Year. I originally thought I could just plug in my DSLR (a fancy camera) into my laptop and have a batch script copy files from the camera to the HDD. Wrong! Windows sees my camera as a device, not a drive...aka there is no drive letter. That means the conventional ways to move a file was not going to work for this. Anyway, doing some searching I found some C# code to read files from devices like cameras, so I used that as the backbone of this app. 

So, this app will copy all the files on the camera every X mins, and make a new directory on the computer and place the photos there. The directory name will be the time stamp (IIRC) At the time, I used it with the Inspirational Screen Saver project and had it display the images on my TV.

There may be some issues with this app, I don’t remember since I haven’t used it since that NYE party a few years ago.
It was originally compiled with visual studio 12, but I did a quick compile in visual studio 15 before uploading the code to git. Even though it compiled, I got a lot of warnings.
