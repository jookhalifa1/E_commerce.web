using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahred.CommonResult
{
     public class Result
    {

        public readonly List<Error> errors = [];

        public bool IsSuccess => errors.Count==0;

         public bool IsFailure=>!IsSuccess;

    

        //Ok 
         protected Result()
        {
            
        }

        //One Error
        protected Result( Error error)
        {
            errors.Add(error);
        }
        // List OF Errors
        protected Result( IReadOnlyList<Error> errorss)
        {
            errors.AddRange(errorss);
        }

        public static Result Ok() => new Result();

        public static Result Fail(Error error) =>new Result(error);
        public static Result Fail(List<Error> errors) => new Result(errors);







    }

    public class Result<TValue> : Result
    {
       private TValue _value;
        
        public TValue Value => IsSuccess? _value:throw new InvalidOperationException("Can Not Access The Value");

        private Result(TValue Value)
        {
            _value = Value;
        }
         private Result( List<Error> errors):base(errors)
        {
            _value = default!;
            
        }
        private Result(Error error) : base(error)
        {
            _value = default!;

        }

        public static Result<TValue> Ok( TValue value) => new Result<TValue>(value);

        public static Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static Result<TValue> Fail(List< Error> errors) => new Result<TValue>(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);

        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List< Error> errors) => Fail(errors);






    }
}
