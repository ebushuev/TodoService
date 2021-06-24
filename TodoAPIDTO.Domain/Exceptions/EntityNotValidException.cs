using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoAPIDTO.Domain.Exceptions
{
    public class EntityNotValidException : Exception
    {
        private const string MESSAGE = "Object not passed validations";
        private readonly IList<string> _errors;

        // hide empty constructor
        private EntityNotValidException()
        {
        }

        public EntityNotValidException(IEnumerable<string> errors)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            if (!errors.Any())
                throw new ArgumentException("Collection length must be more than zero", nameof(errors));

            // shallow copy
            _errors = errors.ToList();

        }

        public EntityNotValidException(params string[] errors)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            if (!errors.Any())
                throw new ArgumentException("Collection length must be more than zero", nameof(errors));

            // shallow copy
            _errors = errors.ToList();
        }
    }
}
