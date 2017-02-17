using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenST2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;



namespace GenST2
{
    public class RoleActions
    {
        internal void AddUserAndRole()
        {
            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canView" role if it doesn't already exist.
            if (!roleMgr.RoleExists("canView"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "canView" });
            }
            if (!roleMgr.RoleExists("teachers"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "teachers" });
            }
            
            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "genevieve.guinn@gmail.com",
                Email = "genevieve.guinn@gmail.com"

            };

            IdUserResult = userMgr.Create(appUser, "Gen52180!");

            // If the new "canView" user was successfully created, 
            // add the "canView" user to the "canView" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail("genevieve.guinn@gmail.com").Id, "canView"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("genevieve.guinn@gmail.com").Id, "canView");
            }

            var user = new ApplicationUser
            {
                UserName = "eguinn@majec.com",
                Email = "eguinn@majec.com"
            };
            IdUserResult = userMgr.Create(user, "Mic3645!");
            IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("eguinn@majec.com").Id, "canView");

            var teacherUsr = new ApplicationUser
            {
                UserName = "teachers@gmail.com",
                Email = "teachers@gmail.com"
            };
            IdUserResult = userMgr.Create(teacherUsr, "Tea3645!");
            IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("teachers@gmail.com").Id, "teachers");

        }
    }
}