﻿using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class InputController
{
    public long currentValue = 0;

    public void Begin(string ipAddress, int port)
    {
        // Give the network stuff its own special thread
        var thread = new Thread(() =>
        {
            // This class makes it super easy to do network stuff
            var client = new TcpClient();

            // Change this to your devices real address
            client.Connect(ipAddress, port);
            var stream = new StreamReader(client.GetStream());

            // We'll read values and buffer them up in here
            var buffer = new List<byte>();
            while (client.Connected)
            {
                // Read the next byte
                var read = stream.Read();

                // We split readings with a carriage return, so check for it 
                if (read == 13)
                {
                    // Once we have a reading, convert our buffer to a string, since the values are coming as strings
                    var str = Encoding.ASCII.GetString(buffer.ToArray());

                    str = str.Trim();
                    str = str.Replace(" ", "");

                    // We assume that they're floats
                    currentValue = long.Parse(str);

                    // Clear the buffer ready for another reading
                    buffer.Clear();
                }
                else
                    // If this wasn't the end of a reading, then just add this new byte to our buffer
                    buffer.Add((byte)read);
            }
        });

        thread.Start();
    }
}