namespace FootballPlayerManagerApi.Objects;

public class BaseResponse<T>
{
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public T Result { get; set; }
}