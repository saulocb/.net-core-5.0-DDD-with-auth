using System;

namespace Bouncer.Common.InternalObjects
{
    public abstract class BaseResult
    {
        public string Message { get; set; }
        public bool HasError { get; set; }
        public Exception Ex { get; set; }

        public BaseResult()
        {
            HasError = false;
        }

        public BaseResult(string errorMessage, Exception ex = null)
        {
            HasError = true;
            Message = errorMessage;
            Ex = ex;
        }
    }

    public class AppResult<T> : BaseResult
    {
        public T Result { get; set; }

        public AppResult() : base()
        {
        }

        public AppResult(T result) : base()
        {
            Result = result;
        }

        public AppResult(string errorMessage, Exception ex = null) : base(errorMessage, ex)
        {
        }

        public AppResult(T result, string errorMessage, Exception ex = null) : base(errorMessage, ex)
        {
            Result = result;
        }
    }

    public class AppResult : AppResult<object>
    {
        public AppResult() : base()
        {
        }

        public AppResult(object result) : base(result)
        {
        }

        public AppResult(string errorMessage, Exception ex = null) : base(errorMessage, ex)
        {
        }

        public AppResult(object result, string errorMessage, Exception ex = null) : base(result, errorMessage, ex)
        {
        }
    }
}
