namespace LibraryManagement.Core.Utilities;

public readonly struct Result<T>
{
    private readonly T? _value;
    private readonly Exception? _exception;
    public T Value => _value ?? throw new InvalidOperationException("Value is not setted!");
    public Exception Exception => _exception ?? throw new InvalidOperationException("Exception is not setted!");
    public bool IsSuccessful { get; }
    public bool IsFailed => !IsSuccessful;

    public Result(T value)
    {
        _value = value;
        _exception = null;
        IsSuccessful = true;
    }

    public Result(Exception exception)
    {
        _value = default;
        _exception = exception;
        IsSuccessful = false;
    }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Exception exception)
    {
        return new Result<T>(exception);
    }

    public TResult Match<TResult>(Func<T, TResult> ifSuccessful, Func<Exception, TResult> ifFailed)
    {
        return IsSuccessful ? ifSuccessful(Value) : ifFailed(Exception);
    }
}