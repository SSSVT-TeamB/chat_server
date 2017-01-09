using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_server.Model
{
    public class Message
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }

        public Message(int id, string userName, string text)
        {
            Id = id;
            UserName = userName;
            Text = text;
        }

        public Message()
        {
        }
    }
}
