﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ShopApp.Data;
using ShopApp.Web.Utilities;
using System;

[assembly: OwinStartup(typeof(ShopApp.Web.Startup))]
namespace ShopApp.Web
{
    public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			this.ConfigureAuth(app);
		}

		// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			// Configure the db context, user manager and signin manager to use a single instance per request
			app.CreatePerOwinContext<ShopAppDbContext>(() => ShopAppDbContext.Create());
			app.CreatePerOwinContext<ShopUserManager>(ShopUserManager.Create);
			app.CreatePerOwinContext<ShopSignInManager>(ShopSignInManager.Create);
			app.CreatePerOwinContext<ShopRoleManager>(ShopRoleManager.Create);

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			// Configure the sign in cookie
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login")
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
			// TODO [GM]: Two factor authentication
			app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

			// Enables the application to remember the second login verification factor such as phone or email.
			// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
			// This is similar to the RememberMe option when you log in.
			app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
		}
	}
}