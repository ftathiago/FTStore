using System;
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

        protected void ExceptionHandler(Exception exception)
        {
            var hasMessage = !string.IsNullOrEmpty(exception.Message);
            if (hasMessage)
                AddErrorMessage(exception.Message);

            var hasInnerException = exception.InnerException != null;
            if (hasInnerException)
                ExceptionHandler(exception.InnerException);

            var isAggregateException = (exception is AggregateException);
            if (!isAggregateException)
                return;
            AggregateExceptionHandler((AggregateException)exception);
        }

        private void AggregateExceptionHandler(AggregateException exception)
        {
            AddErrorMessage("With following Inner exceptions:");
            foreach (var innerException in exception.InnerExceptions)
                ExceptionHandler(innerException);
        }
    }
}
