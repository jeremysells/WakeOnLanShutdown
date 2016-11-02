/*
    Copyright (c) 2011 Jeremy Sells
    See the file LICENSE for copying permission.
    Please read README.md before running or compiling as there is no security on this!
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Threading;

namespace WOLW_PCclient
{
    class Program
    {

        /**
         * Main Loop
         */
        static void Main(string[] args)
        {
             TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Any, 500); ;
             tcpListener.Start();

             Boolean shutdownCommencing = false;
             while (!shutdownCommencing)
             {
                Socket socketForClient = tcpListener.AcceptSocket();
                if (socketForClient.Connected)
                {
                    Console.WriteLine("Client connected");
                    NetworkStream networkStream = new NetworkStream(socketForClient);
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);
                    String theString = streamReader.ReadLine();
                    if (theString == "Shutdown pc now please")
                    {
                        System.Diagnostics.Process.Start("shutdown", "-s -t 100");
                    }

                    Console.WriteLine(theString);
                    streamReader.Close();
                    networkStream.Close();
                }
                socketForClient.Close();
                //Console.WriteLine("Trying Again...");
            }
        }
    }
}
