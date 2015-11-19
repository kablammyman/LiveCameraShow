# LiveCameraShow
A c# app i made for a party a few years back...most of the code is prob from MSDN or something

Ya see, I was hosting a NYE party, and I wanted  a "photo booth" to be a fun thing to help us ring in the new year. 
I orignally thought I could just plug in my DSLR and have a batch script copy files from the camera to the HDD. Wrong!
Windows sees my camera as a device, not a drive...aka there is no drive letter. Anyway, doing some searching I found some
C# code and it worked well. 

So, what this will do is ever X mins, copy all the files on the camera and make a new directory on the HDD. The directory
name will be the time stamp (IIRC) At the time, I used it with the Insprrational Screen Saver project and had it 
display the images on my TV

There may be some issues with this app, i dont rembeer, I havent used it since that NYE party a few years ago.

It was compiled with visual studio 12, but it should also compile with visual studio 15 (but you get a lot of warnings)
