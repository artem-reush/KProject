namespace KProject.Api.Contract.Response
{
    /// <summary>
    /// Базовая модель ответа с данными
    /// </summary>
    public class BaseResponseModel<TResult> : BaseResponseModel
    {
        public TResult Result { get; set; }
    }

    /// <summary>
    /// Базовая модель ответа
    /// </summary>
    public class BaseResponseModel
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
    }
}
