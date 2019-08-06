using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Data_Collector
{
    class Program
    {
        static int trainingSize = 0;
        static int[][] Xvals = new int[trainingSize][];

        static List<int> unrolledX = new List<int>();
        static void Main(string[] args)
        {

            for (int k = 0; k < trainingSize; k++) Xvals[k] = new int[1000];
            Console.WriteLine("Press any key to begin calibration  and  training...");
            Console.ReadKey();
            trainYesandNo();
            
            Console.WriteLine("Program paused wait for neural network to train");

        }





        static void trainYesandNo()
        {
            TcpClient client;
            Stream stream;
            byte[] buffer = new byte[2048];
            int bytesRead;
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": true, ""format"": ""Json"", ""blinkStrength"": false, ""eSense"": false}");


            try
            {
                client = new TcpClient("127.0.0.1", 13854); stream = client.GetStream();

                stream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

                if (stream.CanRead)
                {
                    Console.WriteLine("Running Calibration");
                    int counter = 1;
                    while (counter < 200)
                    {
                        bytesRead = stream.Read(buffer, 0, 2048);
                        string[] packets = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split('\r');

                        Console.WriteLine("\r");
                        Console.WriteLine("Packet size: " + packets.Length);

                        foreach (string s in packets)
                        {
                            Console.WriteLine(s.Length + " " + s.Contains("rawEeg"));
                            
                        }

                        counter++;
                    }
                }

                Console.WriteLine("\r Scanner calibrated press enter to begin \"YES\" training");
                Console.ReadKey();
            
                int countYES = 0;
                

                while (countYES < trainingSize/2)
                {
                    int validPacketcount = 0;
                    List<int> tempVEC = new List<int>();
                    Stopwatch sw = new Stopwatch();
                    Console.WriteLine("Press enter and think YES");
                    Console.ReadKey();



                    sw.Start();
                    double start = sw.Elapsed.TotalMilliseconds;
                    

                    while (sw.Elapsed.TotalMilliseconds - start < 250)
                    {
                        bytesRead = stream.Read(buffer, 0, 2048);
                        string[] packetz = Encoding.UTF8.GetString(buffer,0,bytesRead).Split('\r');

                        foreach (string s in packetz)
                        {
                            if (s.Contains("rawEeg") && s.Length > 10)
                            {
                                try
                                {
                                    int x = Int32.Parse(s.ToString().Substring(10, s.Length - 11));
                                    tempVEC.Add(x);
                                    validPacketcount++;
                                }
                                catch (System.FormatException e) { }
                                
                            }
                        }                       

                    }


                    if (validPacketcount < 1000)
                    {
                        Console.WriteLine("Test failed: try again - press any key when ready");
                        Console.WriteLine("Valid Packets" + validPacketcount);
                        Console.ReadKey();
                    }
                    else
                    {
                        // INSERT CODE TO ADD VECTOR TO GENERAL X MATRIX
                        tempVEC.RemoveRange(1000, tempVEC.Count - 1000);
                        foreach (int a in tempVEC) unrolledX.Add(a);

                        countYES++;
                        Console.WriteLine("Successful extraction press any key when ready ...");
                        Console.ReadKey();
                    }
                }


                Console.WriteLine("\"YES\" TRAINING COMPLETE: ENTER TO BEGIN \"NO\" TRAINING .. ");
                //INSERT INFORMATIC CODE
                Console.ReadKey();

                int countNO = 0;
                while (countNO < trainingSize/2)
                {

                    int validPacketcount = 0;
                    List<int> tempVEC = new List<int>();

                    Console.WriteLine("Think of NO and press enter..");
                    Console.ReadKey();

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    double start = sw.Elapsed.TotalMilliseconds;
                    

                    // INSERT CODE TO INITIALIZE TEMPORARY VECTOR

                    while (sw.Elapsed.TotalMilliseconds - start < 250)
                    {
                        bytesRead = stream.Read(buffer, 0, 2048);
                        string[] packetz = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split('\r');

                        foreach (string s in packetz)
                        {
                            if (s.Contains("rawEeg") && s.Length > 10)
                            {
                                try
                                {
                                    int x = Int32.Parse(s.ToString().Substring(10, s.Length - 11));
                                    tempVEC.Add(x);
                                    validPacketcount++;
                                }catch (System.FormatException e) { }
                                
                            }
                        }

                    }


                    if (validPacketcount < 1000)
                    {
                        Console.WriteLine("Test failed: try again - press any key when ready");
                        Console.ReadKey();
                    }
                    else
                    {
                        // INSERT CODE TO ADD VECTOR TO GENERAL X MATRIX
                        tempVEC.RemoveRange(1000, tempVEC.Count - 1000);
                        //Xvals[trainingSize/2 + countNO] = tempVEC.ToArray();
                        foreach (int a in tempVEC) unrolledX.Add(a);


                        countNO++;
                        Console.WriteLine("Successful extraction press any key when ready ...");
                        Console.ReadKey();
                    }
                }



                Console.WriteLine("TRAINING COMPLETE: PRESS ANY KEY TO WRITE MATRIX");
                //INSERT INFORMATIC CODE


                int count = 0;
                for (int k = 0; k < trainingSize; k++) for (int c = 0; c < 1000; c++) if (Xvals[k][c].ToString() != null) count++;

                Console.WriteLine("Total Digits: " + unrolledX.Count);

                Console.ReadKey();

                //saveMatrix();    TURN ON AND OFF FOR WRITING



                Console.WriteLine("Press to start predicting ");
                while (false) // turn on and off for analysis vs prediction
                {
                    
                    int validPacketcount = 0;
                    List<int> tempVEC = new List<int>();
                    Stopwatch sw = new Stopwatch();
                    Console.WriteLine("Think and press");
                    Console.ReadKey();



                    sw.Start();
                    double start = sw.Elapsed.TotalMilliseconds;


                    while (sw.Elapsed.TotalMilliseconds - start < 250)
                    {
                        bytesRead = stream.Read(buffer, 0, 2048);
                        string[] packetz = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split('\r');

                        foreach (string s in packetz)
                        {
                            if (s.Contains("rawEeg") && s.Length > 10)
                            {
                                try
                                {
                                    int x = Int32.Parse(s.ToString().Substring(10, s.Length - 11));
                                    tempVEC.Add(x);
                                    validPacketcount++;
                                }
                                catch (System.FormatException e) { }

                            }
                        }

                    }


                    if (validPacketcount < 1000)
                    {
                        Console.WriteLine("Test failed: try again - press any key when ready");
                        Console.WriteLine("Valid Packets" + validPacketcount);
                        Console.ReadKey();
                    }
                    else
                    {
                        // INSERT CODE TO ADD VECTOR TO GENERAL X MATRIX
                        tempVEC.RemoveRange(1000, tempVEC.Count - 1000);
                        StreamWriter streamz = new StreamWriter("C:\\Users\\Parth Chonkar\\Desktop\\Prototype\\predictdata.txt");
                        foreach (int k in tempVEC) streamz.WriteLine(k.ToString());
                        streamz.Close();


                        countYES++;
                        Console.WriteLine("Successful extraction press any key when ready ...");
                        Console.ReadKey();
                    }
                }


                Console.WriteLine("Analysis...");
                Console.ReadKey();

                StreamWriter streamzz = new StreamWriter("C:\\Users\\Parth Chonkar\\Desktop\\Prototype\\analyze.txt");

                int acount = 0;
                while (acount < 30)  // Turn off and on by switching true and false
                {
                    int validPacketcount = 0;
                    List<int> tempVEC = new List<int>();
                    Stopwatch sw = new Stopwatch();
                    Console.WriteLine("Think and press");
                    Console.ReadKey();



                    sw.Start();
                    double start = sw.Elapsed.TotalMilliseconds;


                    while (sw.Elapsed.TotalMilliseconds - start < 250)
                    {
                        bytesRead = stream.Read(buffer, 0, 2048);
                        string[] packetz = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split('\r');

                        foreach (string s in packetz)
                        {
                            if (s.Contains("rawEeg") && s.Length > 10)
                            {
                                try
                                {
                                    int x = Int32.Parse(s.ToString().Substring(10, s.Length - 11));
                                    tempVEC.Add(x);
                                    validPacketcount++;
                                }
                                catch (System.FormatException e) { }

                            }
                        }

                    }


                    if (validPacketcount < 1000)
                    {
                        Console.WriteLine("Test failed: try again - press any key when ready");
                        Console.WriteLine("Valid Packets" + validPacketcount);
                        Console.ReadKey();
                    }
                    else
                    {
                        // INSERT CODE TO ADD VECTOR TO GENERAL X MATRIX
                        tempVEC.RemoveRange(1000, tempVEC.Count - 1000);
                        
                        foreach (int k in tempVEC) streamzz.WriteLine(k.ToString());
                        


                        acount++;
                        Console.WriteLine("Successful extraction press any key when ready ...");
                        Console.ReadKey();
                    }
                }

                streamzz.Close();
                

            }
            catch (SocketException se) { }

        }


        static void saveMatrix()
        {
            // INSERT CODE TO SAVE X MATRIX AS A TEXT FILE 
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Parth Chonkar\\Desktop\\Prototype\\Data.txt");
                


                foreach (int k in unrolledX) sw.WriteLine(k); sw.Flush();

                Console.WriteLine("Done");
                Console.ReadKey();
            }
            catch (Exception e) { }
        }




       


    }
}
