using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
namespace PortableDevices
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        private static string outputDir = "";
        private static int timeIntervalInMins = 5;
//---------------------------------------------------------------------------------------------------------
        static void Main()
        {
            ConsoleKeyInfo cki;
            bool done = false;
            DateTime now = DateTime.Now;
            Console.WriteLine(now);
            // Create a timer with a ten second interval.
            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            
            aTimer.Enabled = true;
            
             
            Console.WriteLine("Press the Esc key to exit the program.");

            while(!done)
            {   
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                    done =true;
            }
                 
        }
//----------------------------------------------------------------------------------------------
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            readCfg();

            if(outputDir == "")
            {
                Console.WriteLine("invalid output path, check the cfg file");
                return;
            }

            var collection = new PortableDeviceCollection();
            collection.Refresh();
            PortableDevice myCamera = null;
            // foreach(var device in collection)
            for (int i = 0; i < collection.Count; i++)
            {
                collection[i].Connect();
                if (collection[i].FriendlyName == "D5000")
                {
                    myCamera = collection[i];
                    break;
                }
                collection[i].Disconnect();
            }
            if (myCamera == null)
            {
                Console.WriteLine("nothing is connected?");
                collection = null;
                return;
            }


            //Console.WriteLine(myCamera.FriendlyName);

            var folder = myCamera.GetContents();
            foreach (var item in folder.Files)
            {
                DisplayObject(myCamera, item);
            }
            Console.WriteLine("--------------------------");

            myCamera.Disconnect();
            collection = null;
            myCamera = null;
        }
//----------------------------------------------------------------------------------------------
        public static void DisplayObject(PortableDevice device, PortableDeviceObject portableDeviceObject)
        {
            Console.WriteLine(portableDeviceObject.Name);
            if (portableDeviceObject is PortableDeviceFolder)
            {
                DisplayFolderContents(device, (PortableDeviceFolder) portableDeviceObject);
            }
        }
//----------------------------------------------------------------------------------------------
        public static void readCfg()
        {
            try
            {
                using (StreamReader reader = new StreamReader("cfg.txt"))
                {
                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tokens = line.Split('|');
                        if (tokens[0] == "outputDir")
                        {
                            outputDir = tokens[1];
                            outputDir += DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
                        }
                        else if (tokens[0] == "interval")
                        {
                            try
                            {
                                timeIntervalInMins = Convert.ToInt32(tokens[1]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Input string is not a sequence of digits.");
                                timeIntervalInMins = 5;
                            }
                            aTimer.Interval = timeIntervalInMins * (1000 * 60);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Could not open cfg.txt. make sure its int he same dir as the exe");
            }
          
        }
//---------------------------------------------------------------------------------------------------------
        public static void DisplayFolderContents(PortableDevice device, PortableDeviceFolder folder)
        {
            foreach (var item in folder.Files)
            {
                //Console.WriteLine("item: " + item);
                if (item is PortableDeviceFolder)
                {
                    //Console.WriteLine("Folder: " + item.Id);
                    DisplayFolderContents(device, (PortableDeviceFolder)item);
                }
                if (item is PortableDeviceFile)
                {
                    if (!Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }
                    Console.WriteLine("file: " + item.Name);
                    //device.DownloadFile((PortableDeviceFile)item, @"K:\testCam\");
                    device.DownloadFile((PortableDeviceFile)item, outputDir);
                    //once its downloaded, delete the copy on the camnera
                    device.DeleteFile((PortableDeviceFile)item);
                }
            }
        }
    }
}