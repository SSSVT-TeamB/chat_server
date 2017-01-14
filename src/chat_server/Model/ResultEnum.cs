namespace chat_server.Model
{
    public enum ActionResult
    {
        SUCCESS,
        GENERAL_FAIL
    }
    public enum RoomResult
    {
        SUCCESS,
        NOT_ENOUGH_PERMISSIONS,
        PRIVATE_CHAT,
        GENERAL_FAIL
    }
    public enum RegisterResult
    {
        SUCCESS,
        USER_EXISTS,
        GENERAL_FAIL
    }
    public enum PermissionResult
    {
        SUCCESS,
        NOT_ENOUGH_PERMISSIONS,
        GENERAL_FAIL
    }
}