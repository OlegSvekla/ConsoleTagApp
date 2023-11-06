using ConsoleTagApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Bl.Services
{
    public class MessageReader : IMessageReader
    {
        public string ReadMessage(Stream stream, byte delimiter)
        {
            List<byte> messageBytes = new List<byte>();

            int currentByte;

            while ((currentByte = stream.ReadByte()) != -1)
            {
                if (currentByte == delimiter)
                {
                    var message = Encoding.UTF8.GetString(messageBytes.ToArray());
                    return message;
                }
                else
                {
                    messageBytes.Add((byte)currentByte);
                }
            }
            return null;
        }
    }
}
