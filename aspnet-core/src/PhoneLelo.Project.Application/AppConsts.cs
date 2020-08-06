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
    }
}
