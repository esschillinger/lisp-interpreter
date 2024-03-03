public class RuntimeError : Exception
{
    //public Token token;
    //fixmemaybe In Java, the super() method was called to invoke the parents class's constructor. In C# , I invoked the Exception constructor explicitly

    public Token Token
    {
        get;
    }
    public RuntimeError(Token token, String message) : base(message)
    {
        Token = token;
    }
}
