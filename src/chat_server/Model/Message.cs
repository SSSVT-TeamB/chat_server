﻿namespace chat_server.Model
{
    public class Message
    {
        public int Id { get; set; }

        public User Sender { get; set; }

        public string Text { get; set; }

        public Message(User sender, string text)
        {
            Sender = sender;
            Text = text;
        }

        public Message()
        {
        }
    }
}
