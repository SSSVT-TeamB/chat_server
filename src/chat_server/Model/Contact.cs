namespace chat_server.Model
{
    public class Contact
    {
        public int Id {get;set;}
        public User Owner {get;set;}
        public User User { get; set; }
    }
}