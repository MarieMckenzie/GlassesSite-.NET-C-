﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using _1602967_Milestone1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _1602967_Milestone1
{
    public partial class CreateRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* if (!Request.IsSecureConnection)
             {
                 string url = ConfigurationManagger.AppSetings["UnSeurePath"]
                     + "CreateRole.aspx";
                 Response.Redirect(url);
             }*/
          var ctx = ContextBoundObject.GetOwinContext();


                createRolesandUsers();
        }

        //public void Configuration(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //    createRolesandUsers();
        //}

        // In this method we will create default User roles and Admin user for login    
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();

                role.Name = "admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();

                user.UserName = "Sirisha@utech.edu.jm";
                user.Email = "Sirisha@utech.edu.jm";

                string userPWD = "Sirisha.Sirisha.18";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, role.Name);

                }

                // Add user to Role
                //var idResult = UserManager.AddToRole(user.Id, role.Name);
                UserManager.AddToRole(user.Id, role.Name);
            }


            //// creating Creating Manager role     
            //if (!roleManager.RoleExists("Manager"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Manager";
            //    roleManager.Create(role);

            //}

            //// creating Creating Employee role     
            //if (!roleManager.RoleExists("Employee"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Employee";
            //    roleManager.Create(role);

            //}
        }
    }
}