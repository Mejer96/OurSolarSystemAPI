using System;
namespace OurSolarSystemAPI.Exceptions 
{
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message)
            : base(message) { }

    }

    public class AuthenticationFailed : Exception
    {
        public AuthenticationFailed(string message)
            : base(message) { }

    }

    public class BadRequest : Exception
    {
        public BadRequest(string message)
            : base(message) { }

    }

    public class InvalidPassword : Exception
    {
        public InvalidPassword(string message)
            : base(message) { }

    }

    public class PasswordsNotMatching : Exception
    {
        public PasswordsNotMatching(string message)
            : base(message) { }
    }

    public class SomethingWentWrong : Exception
    {
        public SomethingWentWrong(string message)
            : base(message) { }
    }

}

