namespace FastMeiliSync.Shared.Exceptions;

public sealed class DatabaseTransactionException : Exception
{
    public DatabaseTransactionException() { }

    public DatabaseTransactionException(string message)
        : base(message) { }

    public DatabaseTransactionException(string message, Exception? innerException)
        : base(message, innerException) { }
}
