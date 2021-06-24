using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoAPIDTO.Domain.Exceptions
{
    public class EntityNotValidException : Exception
    {
        private const string MESSAGE = "Object not passed validations";
        public readonly IList<string> ValidationErrors;

        // hide empty constructor
        private EntityNotValidException()
        {
        }

        public EntityNotValidException(IEnumerable<string> errors) : base(MESSAGE)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            if (!errors.Any())
                throw new ArgumentException("Collection length must be more than zero", nameof(errors));

            // shallow copy
            ValidationErrors = errors.ToList();

        }

        public EntityNotValidException(params string[] errors) : base(MESSAGE)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            if (!errors.Any())
                throw new ArgumentException("Collection length must be more than zero", nameof(errors));

            // shallow copy
            ValidationErrors = errors.ToList();
        }
    }
}
