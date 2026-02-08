namespace E_commerce.Sahred.CommonResult
{
     public class Error
    {
         private Error(string description, string code, ErrorType type)
        {
            Description = description;
            Code = code;
            Type = type;
        }

        public string Description { get;  }

        public string Code { get;  }


        public ErrorType  Type { get;  }



         public static Error Failure (string Code="General.Failure",string description="General Failure Has Occurred")
        {
            return new Error(Code, description, ErrorType.Failure);

        }
        public static Error Validation(string Code = "General.Validation", string description = "General  Validation Has Occurred")
        {
            return new Error(Code, description, ErrorType.Validation);

        }
       public static Error NotFound(string Code = "General.NotFound", string description = "Request Not Found ")
        {
            return new Error(Code, description, ErrorType.NotFound);

        }
        public static Error UnAuthorized(string Code = "General.UnAuthorized", string description = "You Are Not Authorized")
        {
            return new Error(Code, description, ErrorType.UnAuthorized);

        }
         public static Error Forbidden(string Code = "General.Forbidden", string description = "You do Not Permission to  Access this EndPoint ")
        {
            return new Error(Code, description, ErrorType.Forbidden);

        }
        public static Error InvalidCredentials(string Code = "General.InvalidCredentials", string description = "General InvalidCredentials Has Occurred")
        {
            return new Error(Code, description, ErrorType.InvalidCredentials);

        }

    }
}