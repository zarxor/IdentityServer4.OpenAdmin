//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IdentityServer4.OpenAdmin.UI.Contracts
//{
//    class JsonDataContracts
//    {
//    }


//    public class Rootobject
//    {
//        public Client client { get; set; }
//        public Identityresource identityResource { get; set; }
//    }

//    public class Client
//    {
//        public Definitions definitions { get; set; }
//        public string type { get; set; }
//        public Properties4 properties { get; set; }
//        public string[] required { get; set; }
//    }

//    public class Definitions
//    {
//        public Claim Claim { get; set; }
//        public Claimsidentity ClaimsIdentity { get; set; }
//        public Secret Secret { get; set; }
//    }

//    public class Claim
//    {
//        public string[] type { get; set; }
//        public Properties properties { get; set; }
//        public string[] required { get; set; }
//    }

//    public class Properties
//    {
//        public Issuer issuer { get; set; }
//        public Originalissuer originalIssuer { get; set; }
//        public Properties1 properties { get; set; }
//        public Subject subject { get; set; }
//        public Type type { get; set; }
//        public Value value { get; set; }
//        public Valuetype valueType { get; set; }
//    }

//    public class Issuer
//    {
//        public string[] type { get; set; }
//    }

//    public class Originalissuer
//    {
//        public string[] type { get; set; }
//    }

//    public class Properties1
//    {
//        public string[] type { get; set; }
//        public Additionalproperties additionalProperties { get; set; }
//    }

//    public class Additionalproperties
//    {
//        public string[] type { get; set; }
//    }

//    public class Subject
//    {
//        public string _ref { get; set; }
//    }

//    public class Type
//    {
//        public string[] type { get; set; }
//    }

//    public class Value
//    {
//        public string[] type { get; set; }
//    }

//    public class Valuetype
//    {
//        public string[] type { get; set; }
//    }

//    public class Claimsidentity
//    {
//        public string[] type { get; set; }
//        public Properties2 properties { get; set; }
//        public string[] required { get; set; }
//    }

//    public class Properties2
//    {
//        public Authenticationtype authenticationType { get; set; }
//        public Isauthenticated isAuthenticated { get; set; }
//        public Actor actor { get; set; }
//        public Bootstrapcontext bootstrapContext { get; set; }
//        public Claims claims { get; set; }
//        public Label label { get; set; }
//        public Name name { get; set; }
//        public Nameclaimtype nameClaimType { get; set; }
//        public Roleclaimtype roleClaimType { get; set; }
//    }

//    public class Authenticationtype
//    {
//        public string[] type { get; set; }
//    }

//    public class Isauthenticated
//    {
//        public string type { get; set; }
//    }

//    public class Actor
//    {
//        public string _ref { get; set; }
//    }

//    public class Bootstrapcontext
//    {
//    }

//    public class Claims
//    {
//        public string[] type { get; set; }
//        public Items items { get; set; }
//    }

//    public class Items
//    {
//        public string _ref { get; set; }
//    }

//    public class Label
//    {
//        public string[] type { get; set; }
//    }

//    public class Name
//    {
//        public string[] type { get; set; }
//    }

//    public class Nameclaimtype
//    {
//        public string[] type { get; set; }
//    }

//    public class Roleclaimtype
//    {
//        public string[] type { get; set; }
//    }

//    public class Secret
//    {
//        public string[] type { get; set; }
//        public Properties3 properties { get; set; }
//        public string[] required { get; set; }
//    }

//    public class Properties3
//    {
//        public Description description { get; set; }
//        public Value1 value { get; set; }
//        public Expiration expiration { get; set; }
//        public Type1 type { get; set; }
//    }

//    public class Description
//    {
//        public string[] type { get; set; }
//    }

//    public class Value1
//    {
//        public string[] type { get; set; }
//    }

//    public class Expiration
//    {
//        public string[] type { get; set; }
//        public string format { get; set; }
//    }

//    public class Type1
//    {
//        public string[] type { get; set; }
//    }

//    public class Properties4
//    {
//        public Enabled enabled { get; set; }
//        public Clientid clientId { get; set; }
//        public Protocoltype protocolType { get; set; }
//        public Clientsecrets clientSecrets { get; set; }
//        public Requireclientsecret requireClientSecret { get; set; }
//        public Clientname clientName { get; set; }
//        public Description1 description { get; set; }
//        public Clienturi clientUri { get; set; }
//        public Logouri logoUri { get; set; }
//        public Requireconsent requireConsent { get; set; }
//        public Allowrememberconsent allowRememberConsent { get; set; }
//        public Allowedgranttypes allowedGrantTypes { get; set; }
//        public Requirepkce requirePkce { get; set; }
//        public Allowplaintextpkce allowPlainTextPkce { get; set; }
//        public Allowaccesstokensviabrowser allowAccessTokensViaBrowser { get; set; }
//        public Redirecturis redirectUris { get; set; }
//        public Postlogoutredirecturis postLogoutRedirectUris { get; set; }
//        public Frontchannellogouturi frontChannelLogoutUri { get; set; }
//        public Frontchannellogoutsessionrequired frontChannelLogoutSessionRequired { get; set; }
//        public Backchannellogouturi backChannelLogoutUri { get; set; }
//        public Backchannellogoutsessionrequired backChannelLogoutSessionRequired { get; set; }
//        public Allowofflineaccess allowOfflineAccess { get; set; }
//        public Allowedscopes allowedScopes { get; set; }
//        public Alwaysincludeuserclaimsinidtoken alwaysIncludeUserClaimsInIdToken { get; set; }
//        public Identitytokenlifetime identityTokenLifetime { get; set; }
//        public Accesstokenlifetime accessTokenLifetime { get; set; }
//        public Authorizationcodelifetime authorizationCodeLifetime { get; set; }
//        public Absoluterefreshtokenlifetime absoluteRefreshTokenLifetime { get; set; }
//        public Slidingrefreshtokenlifetime slidingRefreshTokenLifetime { get; set; }
//        public Consentlifetime consentLifetime { get; set; }
//        public Refreshtokenusage refreshTokenUsage { get; set; }
//        public Updateaccesstokenclaimsonrefresh updateAccessTokenClaimsOnRefresh { get; set; }
//        public Refreshtokenexpiration refreshTokenExpiration { get; set; }
//        public Accesstokentype accessTokenType { get; set; }
//        public Enablelocallogin enableLocalLogin { get; set; }
//        public Identityproviderrestrictions identityProviderRestrictions { get; set; }
//        public Includejwtid includeJwtId { get; set; }
//        public Claims1 claims { get; set; }
//        public Alwayssendclientclaims alwaysSendClientClaims { get; set; }
//        public Clientclaimsprefix clientClaimsPrefix { get; set; }
//        public Pairwisesubjectsalt pairWiseSubjectSalt { get; set; }
//        public Userssolifetime userSsoLifetime { get; set; }
//        public Usercodetype userCodeType { get; set; }
//        public Devicecodelifetime deviceCodeLifetime { get; set; }
//        public Allowedcorsorigins allowedCorsOrigins { get; set; }
//        public Properties5 properties { get; set; }
//    }

//    public class Enabled
//    {
//        public string type { get; set; }
//    }

//    public class Clientid
//    {
//        public string[] type { get; set; }
//    }

//    public class Protocoltype
//    {
//        public string[] type { get; set; }
//    }

//    public class Clientsecrets
//    {
//        public string[] type { get; set; }
//        public Items1 items { get; set; }
//    }

//    public class Items1
//    {
//        public string _ref { get; set; }
//    }

//    public class Requireclientsecret
//    {
//        public string type { get; set; }
//    }

//    public class Clientname
//    {
//        public string[] type { get; set; }
//    }

//    public class Description1
//    {
//        public string[] type { get; set; }
//    }

//    public class Clienturi
//    {
//        public string[] type { get; set; }
//    }

//    public class Logouri
//    {
//        public string[] type { get; set; }
//    }

//    public class Requireconsent
//    {
//        public string type { get; set; }
//    }

//    public class Allowrememberconsent
//    {
//        public string type { get; set; }
//    }

//    public class Allowedgranttypes
//    {
//        public string[] type { get; set; }
//        public Items2 items { get; set; }
//    }

//    public class Items2
//    {
//        public string[] type { get; set; }
//    }

//    public class Requirepkce
//    {
//        public string type { get; set; }
//    }

//    public class Allowplaintextpkce
//    {
//        public string type { get; set; }
//    }

//    public class Allowaccesstokensviabrowser
//    {
//        public string type { get; set; }
//    }

//    public class Redirecturis
//    {
//        public string[] type { get; set; }
//        public Items3 items { get; set; }
//    }

//    public class Items3
//    {
//        public string[] type { get; set; }
//    }

//    public class Postlogoutredirecturis
//    {
//        public string[] type { get; set; }
//        public Items4 items { get; set; }
//    }

//    public class Items4
//    {
//        public string[] type { get; set; }
//    }

//    public class Frontchannellogouturi
//    {
//        public string[] type { get; set; }
//    }

//    public class Frontchannellogoutsessionrequired
//    {
//        public string type { get; set; }
//    }

//    public class Backchannellogouturi
//    {
//        public string[] type { get; set; }
//    }

//    public class Backchannellogoutsessionrequired
//    {
//        public string type { get; set; }
//    }

//    public class Allowofflineaccess
//    {
//        public string type { get; set; }
//    }

//    public class Allowedscopes
//    {
//        public string[] type { get; set; }
//        public Items5 items { get; set; }
//    }

//    public class Items5
//    {
//        public string[] type { get; set; }
//    }

//    public class Alwaysincludeuserclaimsinidtoken
//    {
//        public string type { get; set; }
//    }

//    public class Identitytokenlifetime
//    {
//        public string type { get; set; }
//    }

//    public class Accesstokenlifetime
//    {
//        public string type { get; set; }
//    }

//    public class Authorizationcodelifetime
//    {
//        public string type { get; set; }
//    }

//    public class Absoluterefreshtokenlifetime
//    {
//        public string type { get; set; }
//    }

//    public class Slidingrefreshtokenlifetime
//    {
//        public string type { get; set; }
//    }

//    public class Consentlifetime
//    {
//        public string[] type { get; set; }
//    }

//    public class Refreshtokenusage
//    {
//        public string type { get; set; }
//        public int[] _enum { get; set; }
//    }

//    public class Updateaccesstokenclaimsonrefresh
//    {
//        public string type { get; set; }
//    }

//    public class Refreshtokenexpiration
//    {
//        public string type { get; set; }
//        public int[] _enum { get; set; }
//    }

//    public class Accesstokentype
//    {
//        public string type { get; set; }
//        public int[] _enum { get; set; }
//    }

//    public class Enablelocallogin
//    {
//        public string type { get; set; }
//    }

//    public class Identityproviderrestrictions
//    {
//        public string[] type { get; set; }
//        public Items6 items { get; set; }
//    }

//    public class Items6
//    {
//        public string[] type { get; set; }
//    }

//    public class Includejwtid
//    {
//        public string type { get; set; }
//    }

//    public class Claims1
//    {
//        public string[] type { get; set; }
//        public Items7 items { get; set; }
//    }

//    public class Items7
//    {
//        public string _ref { get; set; }
//    }

//    public class Alwayssendclientclaims
//    {
//        public string type { get; set; }
//    }

//    public class Clientclaimsprefix
//    {
//        public string[] type { get; set; }
//    }

//    public class Pairwisesubjectsalt
//    {
//        public string[] type { get; set; }
//    }

//    public class Userssolifetime
//    {
//        public string[] type { get; set; }
//    }

//    public class Usercodetype
//    {
//        public string[] type { get; set; }
//    }

//    public class Devicecodelifetime
//    {
//        public string type { get; set; }
//    }

//    public class Allowedcorsorigins
//    {
//        public string[] type { get; set; }
//        public Items8 items { get; set; }
//    }

//    public class Items8
//    {
//        public string[] type { get; set; }
//    }

//    public class Properties5
//    {
//        public string[] type { get; set; }
//        public Additionalproperties1 additionalProperties { get; set; }
//    }

//    public class Additionalproperties1
//    {
//        public string[] type { get; set; }
//    }

//    public class Identityresource
//    {
//        public string type { get; set; }
//        public Properties6 properties { get; set; }
//        public string[] required { get; set; }
//    }

//    public class Properties6
//    {
//        public Required required { get; set; }
//        public Emphasize emphasize { get; set; }
//        public Showindiscoverydocument showInDiscoveryDocument { get; set; }
//        public Enabled1 enabled { get; set; }
//        public Name1 name { get; set; }
//        public Displayname displayName { get; set; }
//        public Description2 description { get; set; }
//        public Userclaims userClaims { get; set; }
//        public Properties7 properties { get; set; }
//    }

//    public class Required
//    {
//        public string type { get; set; }
//    }

//    public class Emphasize
//    {
//        public string type { get; set; }
//    }

//    public class Showindiscoverydocument
//    {
//        public string type { get; set; }
//    }

//    public class Enabled1
//    {
//        public string type { get; set; }
//    }

//    public class Name1
//    {
//        public string[] type { get; set; }
//    }

//    public class Displayname
//    {
//        public string[] type { get; set; }
//    }

//    public class Description2
//    {
//        public string[] type { get; set; }
//    }

//    public class Userclaims
//    {
//        public string[] type { get; set; }
//        public Items9 items { get; set; }
//    }

//    public class Items9
//    {
//        public string[] type { get; set; }
//    }

//    public class Properties7
//    {
//        public string[] type { get; set; }
//        public Additionalproperties2 additionalProperties { get; set; }
//    }

//    public class Additionalproperties2
//    {
//        public string[] type { get; set; }
//    }

//}
