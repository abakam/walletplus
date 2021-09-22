using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Services.Helpers.Constants
{
    public static class ErrorMessages
    {
        public const string INTERNAL_ERROR_MESSAGE = "An error occurred while processing your request";
        public const string INVALID_EMAIL_PASSWORD = "The email or password is not correct";
        public const string USER_ALREADY_EXISTS = "A user with this email already exists";
        public const string RECORD_DO_EXISTS = "This record do not exists"
    }
}
