using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SearchTicketApp.Exceptions
{
    public class ModelNotValidException : Exception
    {
        public ICollection<string> ModelErrorMessages { get; private set; }

        public ModelNotValidException(string message) : base(message)
        {
            
        }

        public ModelNotValidException(ModelStateDictionary modelState)
        {
            this.ModelErrorMessages = modelState.Root.Errors.
                Select(e => e.ErrorMessage).
                ToList();
        }

        public static void ThrowIfModelStateNotValid(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                throw new ModelNotValidException(modelState);
        }
    }
}
