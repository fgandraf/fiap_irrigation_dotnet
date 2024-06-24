namespace Irrigation.Core;

public class OperationResult(bool success = false, string message = null)
{
    public bool Success { get; protected set; } = success;
    public string Message { get; protected set; } = message;

    public static OperationResult SuccessResult(string message = "OK", bool success = true) => new(success : success, message: message);
    public static OperationResult FailureResult(string message = "") => new(success : false, message : message);
}

public class OperationResult<T> : OperationResult
{
    public T Value { get; private set; }

    private OperationResult(bool success, T value = default, string message = null) : base(success, message)
        => Value = value;

    public static OperationResult<T> SuccessResult(T model) => new(success: true, value: model, message: "OK");
    public new static OperationResult<T> FailureResult(string message = "") => new(success: false, value: default, message: message);
}
