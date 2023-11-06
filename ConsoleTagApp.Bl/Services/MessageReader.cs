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
        //private readonly Stream _stream;
        //private readonly byte _delimiter;

        //public MessageReader(Stream stream, byte delimiter)
        //{
        //    _stream = stream;
        //    _delimiter = delimiter;
        //}
        public string ReadMessage(Stream stream, byte delimiter)
        {
            List<byte> messageBytes = new List<byte>();
            int currentByte;
            int number = 0;
            while ((currentByte = stream.ReadByte()) != -1)
            {
                if (currentByte == delimiter)
                {
                    var messageText = Encoding.UTF8.GetString(messageBytes.ToArray());
                    string message = $"Вот ваше сообщение под номером {number}: {messageText}";
                    return message;
                    number++;// Вывести сообщение в консоль
                    messageBytes.Clear(); // Очистить буфер для следующего сообщения
                }
                else
                {
                    messageBytes.Add((byte)currentByte);
                }
            }
            return null;
        }

        //public string ReadMessage(Stream stream, byte delimiter)
        //{
        //    List<byte> messageBytes = new List<byte>();
        //    int currentByte;
        //    while ((currentByte = stream.ReadByte()) != -1)
        //    {
        //        if (currentByte == delimiter)
        //        {
        //            break;
        //        }
        //        messageBytes.Add((byte)currentByte);
        //    }

        //    return Encoding.UTF8.GetString(messageBytes.ToArray());
        //}
    }
}
