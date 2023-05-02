namespace TelegramBotApplication.Exceptions
{

    [Serializable]
    public class TelegramTokenNotFoundException : Exception
    {
        public TelegramTokenNotFoundException() { }
        public TelegramTokenNotFoundException(string message) : base(message) { }
        public TelegramTokenNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected TelegramTokenNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
