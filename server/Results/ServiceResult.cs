namespace Verbum.API.Results;

public class ServiceResult<T> {
    public ServiceResultStatus Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public bool IsSuccess => Status == ServiceResultStatus.Success;

    public static ServiceResult<T> Success(T data) {
        return new ServiceResult<T> { Status = ServiceResultStatus.Success, Data = data };
    }

    public static ServiceResult<T> Error(ServiceResultStatus status, string message) {
        return new ServiceResult<T> { Status = status, Message = message };
    }
}