using System;

public class InvalidNmeaException : System.Exception
{
    public InvalidNmeaException() { }

    public InvalidNmeaException(String message) : base(message) { }

    public InvalidNmeaException(String message, Exception inner) : base(message, inner) { }
}