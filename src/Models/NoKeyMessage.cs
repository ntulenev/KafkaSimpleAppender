namespace Models
{
    public class NoKeyMessage : MessageBase<object>
    {
        public NoKeyMessage(string payload) : base(payload)
        {

        }
    }
}
