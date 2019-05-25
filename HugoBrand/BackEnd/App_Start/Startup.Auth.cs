using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using BackEnd.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BackEnd
{
    public partial class Startup
    {
        //public void ConfigurationApp(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //    createRolesAndDefaultUsers();

        //}        

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

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

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        //private void createRolesAndDefaultUsers()
        //{
            
        //    using (ApplicationDbContext context = new ApplicationDbContext())
        //    {
                
        //        using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
        //        {
        //            // check whether an Admin role already exists - if it does, do nothing
        //            if (!roleManager.RoleExists("Admin"))
        //            {
        //                // Admin role does NOT exist - we need to create it
        //                IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //                role.Name = "Admin";
        //                roleManager.Create(role);
        //            }
        //        }

        //        // Now it is time to manage the users and assign roles to the user
        //        using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
        //        {
        //            // First, check if the admin user exists!
        //            if (userManager.FindByName("administrator@yourEmailHost.com") == null)
        //            {
        //                // admin user does not exist - we can create it
        //                ApplicationUser user = new ApplicationUser();
        //                user.UserName = "administrator@yourEmailHost.com";
        //                user.Email = "administrator@yourEmailHost.com";

        //                string userPWD = "P@ssw0rd_123";

        //                IdentityResult chkUser = userManager.Create(user, userPWD);

        //                //Add the admin user to the Admin role, if it was successfully created
        //                if (chkUser.Succeeded)
        //                {
        //                    IdentityResult chkRole = userManager.AddToRole(user.Id, "Admin");

        //                    if (!chkRole.Succeeded)
        //                    {
        //                        // admin user was not assigned to role, something went wrong!
        //                        // Log this information and handle it
        //                        Console.Error.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
        //                        Console.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
        //                    }
        //                }
        //                else
        //                {   // admin user was not created, something went wrong!
        //                    // Log this information and handle it
        //                    Console.Error.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
        //                    Console.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
        //                }
        //            }
        //        }

        //        using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
        //        {
        //            // check whether a shopkeeper role already exists - if it does, do nothing
        //            if (!roleManager.RoleExists("Shopkeeper"))
        //            {
        //                // Shopkeeper role does NOT exist - we need to create it
        //                IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //                role.Name = "Shopkeeper";
        //                roleManager.Create(role);
        //            }
        //        }

        //        // Now it is time to manage the users and assign roles to the user
        //        using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
        //        {
        //            // First, check if the Shopkeeper user exists!
        //            if (userManager.FindByName("shopkeeper1@yourEmailHost.com") == null)
        //            {
        //                // Shopkeeper user does not exist - we can create it
        //                ApplicationUser user = new ApplicationUser();
        //                user.UserName = "shopkeeper1@yourEmailHost.com";
        //                user.Email = "shopkeeper1@yourEmailHost.com";

        //                string userPWD = "P@ssw0rd_123";

        //                IdentityResult chkUser = userManager.Create(user, userPWD);

        //                //Add the Shopkeeper1 user to the Shopkeeper role, if it was successfully created
        //                if (chkUser.Succeeded)
        //                {
        //                    IdentityResult chkRole = userManager.AddToRole(user.Id, "Shopkeeper");

        //                    if (!chkRole.Succeeded)
        //                    {
        //                        // Shopkeeper user was not assigned to role, something went wrong!
        //                        // Log this information and handle it
        //                        Console.Error.WriteLine("An error has occured in Startup! Shopkeeper1 user was not assigned to Shopkeeper role successfully.");
        //                        Console.WriteLine("An error has occured in Startup! Shopkeeper1 user was not assigned to Shopkeeper role successfully.");
        //                    }
        //                }
        //                else
        //                {   // admin user was not created, something went wrong!
        //                    // Log this information and handle it
        //                    Console.Error.WriteLine("An error has occured in Startup! Shopkeeper1 user does not exist, but was not created successfully.");
        //                    Console.WriteLine("An error has occured in Startup! Shopkeeper user does not exist, but was not created successfully.");
        //                }
        //            }
        //        }


        //    }

        //}
    }
}