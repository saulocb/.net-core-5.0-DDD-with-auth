namespace Bouncer.Common.Exceptions
{
    public class InvalidLoginException : AppBaseException
    {
        public InvalidLoginException()
            : base("The credentials are incorrect or the account is inactive.")
        {

        }
    }
}
