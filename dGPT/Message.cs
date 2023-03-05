using System;

namespace dGPT
{
    public enum MessageSide
    {
        Me,
        You,
        Typing,
        Error
    }

    public class Message
    {
        public Message(string model)
        {
            Timestamp = DateTime.Now.ToString("ddd, HH:mm") + " " + model;
        }

        public string Text { get; set; }

        public string Timestamp { get; set; }

        public MessageSide Side { get; set; }
        public MessageSide PrevSide { get; set; }
    }
}