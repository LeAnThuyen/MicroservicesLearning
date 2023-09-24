using FluentValidation.Results;

namespace Ordering.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(c => c.PropertyName, c => c.ErrorMessage)
                .ToDictionary(gr => gr.Key, gr => gr.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
