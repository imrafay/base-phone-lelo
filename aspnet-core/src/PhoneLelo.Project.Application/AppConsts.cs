namespace PhoneLelo.Project
{
    public class AppConsts
    {
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";
        public const string DefaultUserPassword = "123qwe";
        public const string DefaultUserName = "Seller";
        public const string DefaultPhoneNumberCode = "0000";

        //Rostering from olx.
        public const string OlxBaseUrl = "https://www.olx.com.pk";
        public const string OlxUrlParameters = "/api/locations";
        public const string OlxTownsParameters = "&hideAddressComponents=true";


        public const int RelatedProductAdPriceLimit = 10000;
        public const int RelatedProductAdPageSize = 12;

        public class ErrorMessage
        {
            public const string NotFound = "Not found.";
            public const string IdMustBeProvided = "Id must be provided.";
            public const string GeneralErrorMessage = "Some thing went wrong.";
        }
    }
}
