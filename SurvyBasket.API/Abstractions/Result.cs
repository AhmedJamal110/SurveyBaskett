namespace SurveyBasket.API.Abstractions
{
    public class Result
    {
        public Result( bool isSucess , Error error)
        {
            if ((isSucess && error != Error.None) || (!isSucess && error == Error.None))
                throw new InvalidOperationException();

            IsSuccess = isSucess;
            Error = error;
            
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error = default!;

        public static Result Success()
            => new(true, Error.None);

        public static Result Failure(Error error)
            => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value)
            => new(value, true, Error.None);

        public static Result<TValue> Failure<TValue>(Error error)
            => new(default!, false, error);


    }


    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public Result(TValue value ,  bool isSucess, Error error) : base(isSucess, error)
        {
            _value = value;
        }

        public TValue Value
            => IsSuccess ? _value! : throw new InvalidOperationException("Failure Result Cant have Value");
    
    }
}
