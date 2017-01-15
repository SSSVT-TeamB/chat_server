namespace chat_server.Model
{
    public class Connection
    {
        public string ConnectionId {get;set;}
        public Connection(string connectionId)
        {
            ConnectionId = connectionId;
        }
        public Connection()
        {
            
        }
    }
}