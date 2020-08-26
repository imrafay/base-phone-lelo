export class AppConsts {

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish

    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'Phonelelo'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };

    static readonly mobileBrand = {
        Apple: 'Apple'
    }
}
