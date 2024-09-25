namespace LibraryManagement.Core.Utilities;

public readonly struct Result<T>
{
    private readonly T? _value = default;
    private readonly Exception? _exception = default;
    public T Value => _value ?? throw new InvalidOperationException("Value is not setted!");
    public Exception Exception => _exception ?? throw new InvalidOperationException("Exception is not setted!");
    public bool IsSuccessful { get; } = false;
    public bool IsFailed => !IsSuccessful;

    public Result(T value)
    {
        IsSuccessful = true;
        _value = value;
    }

    public Result(Exception exception) => _exception = exception;

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Exception exception) => new(exception);

    public TResult Match<TResult>(Func<T, TResult> ifSuccessful, Func<Exception, TResult> ifFailed) =>
        IsSuccessful ? ifSuccessful(Value) : ifFailed(Exception);
}