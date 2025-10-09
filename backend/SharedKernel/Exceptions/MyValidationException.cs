using SharedKernel.Common;

namespace SharedKernel.Exceptions
{
    public class MyValidationException : BaseException
    {
        public override System.Net.HttpStatusCode StatusCode => System.Net.HttpStatusCode.BadRequest;
        public MyValidationException(string message)
            : base(message) { }
        public MyValidationException(string message, Exception innerException)
            : base(message, innerException) { }
        public MyValidationException()
            : base("Validation exception.") { }
        public MyValidationException(Exception innerException)
            : base("Validation exception", innerException) { }
    }
}
