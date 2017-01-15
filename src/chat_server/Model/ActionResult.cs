namespace chat_server.Model
{
    public enum ActionResult
    {
        SUCCESS = 0,
        NOT_ENOUGH_PERMISSIONS = 1,
        PRIVATE_CHAT = 2,
        USER_EXISTS = 3,
        GENERAL_FAIL = 100
    }
}