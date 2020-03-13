using System.Collections.Generic;
using System.Linq;

namespace FTStore.App.Services.Impl
{
    public abstract class ServiceBase
    {
        private List<string> _errorMessages;

        public string GetErrorMessages()
        {
            return string.Join(". ", ErrorMessages);
        }

        public bool IsValid
        {
            get
            {
                return !ErrorMessages.Any();
            }
        }
        private List<string> ErrorMessages
        {
            get
            {
                return _errorMessages ?? (_errorMessages = new List<string>());
            }
        }


        protected void ClearErrors()
        {
            ErrorMessages.Clear();
        }

        protected void AddErrorMessage(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }
    }
}