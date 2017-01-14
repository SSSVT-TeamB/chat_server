using Newtonsoft.Json;

namespace chat_server.Model
{
    public class Login
    {
        [JsonIgnoreAttribute]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}