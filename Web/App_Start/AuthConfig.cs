namespace Sattelite.Web.App_Start
{
    using DotNetOpenAuth.AspNet.Clients;
    using DotNetOpenAuth.OpenId.RelyingParty;
    using Microsoft.Web.WebPages.OAuth;

    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "85CmzrPeNbfDBLqQDhZWvJvj2",
                consumerSecret: "X6dW6my1ypjUAebxZVINn2QIrFrvJ2oe5hddLgi827JOv5nFjF");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "960531237445623",
                appSecret: "cd7f8a90861694ae5e252bfe6fd6a699");


            OAuthWebSecurity.RegisterGoogleClient();
            //var extraData = new Dictionary<string, object>();
            //extraData.Add("clientId", "000-000.apps.googleusercontent.com");
            //extraData.Add("clientSecret", "00000000000");
            //OAuthWebSecurity.RegisterGoogleClient("google", extraData);

            OAuthWebSecurity.RegisterYahooClient();

            var MyOpenIdClient = new OpenIdClient("myopenid", WellKnownProviders.MyOpenId);
            OAuthWebSecurity.RegisterClient(MyOpenIdClient, "myOpenID", null);
        }

        //http://www.asp.net/mvc/tutorials/mvc-5/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on#goog    
    }
}