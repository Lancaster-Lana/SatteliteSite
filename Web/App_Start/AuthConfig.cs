namespace Sattelite.Web.App_Start
{
    using DotNetOpenAuth.AspNet.Clients;
    using DotNetOpenAuth.OpenId.RelyingParty;
    using Microsoft.Web.WebPages.OAuth;
    using Owin;
    using System.Collections.Generic;

    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "daX1XtCiUTPh5ie4xuY22A",
                consumerSecret: "RjXEDwwsifrcO3EweTcAJvuNxZSiqgUUnNwdnjTqPo");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "960531237445623",
                appSecret: "cd7f8a90861694ae5e252bfe6fd6a699");

            //var extraData = new Dictionary<string, object>();
            //extraData.Add("clientId", "000-000.apps.googleusercontent.com");
            //extraData.Add("clientSecret", "00000000000");

            //OAuthWebSecurity.RegisterGoogleClient("google", extraData);
            OAuthWebSecurity.RegisterGoogleClient();
            OAuthWebSecurity.RegisterYahooClient();

            var MyOpenIdClient = new OpenIdClient("myopenid", WellKnownProviders.MyOpenId);
            OAuthWebSecurity.RegisterClient(MyOpenIdClient, "myOpenID", null);
        }

         /*
        //http://www.asp.net/mvc/tutorials/mvc-5/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on#goog
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            app.UseGoogleAuthentication(
                 clientId: "000-000.apps.googleusercontent.com",
                 clientSecret: "00000000000");
        }
     */
    }
}