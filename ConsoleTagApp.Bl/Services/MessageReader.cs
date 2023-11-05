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
        private readonly Stream _stream;
        private readonly byte _delimiter;

        public MessageReader(Stream stream, byte delimiter)
        {
            _stream = stream;
            _delimiter = delimiter;
        }

        public string ReadMessage()
        {
            List<byte> messageBytes = new List<byte>();
            int currentByte;
            while ((currentByte = _stream.ReadByte()) != -1)
            {
                if (currentByte == _delimiter)
                {
                    break;
                }
                messageBytes.Add((byte)currentByte);
            }

            return Encoding.UTF8.GetString(messageBytes.ToArray());
        }
    }
}
