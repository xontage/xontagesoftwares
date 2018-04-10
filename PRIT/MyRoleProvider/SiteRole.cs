using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PRIT.MyRoleProvider
{
    public class SiteRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            PRITEntities db = new PRITEntities();
            //string data = db.tbl_Login.Where(x => x.UserName == username).FirstOrDefault().RoleId;
            //string[] result = { data };
            //return result;        

            var innerJoin = from s in db.tbl_Registration // outer sequence
                            join st in db.tbl_UserRole //inner sequence 
                            on s.RoleId equals st.RoleId // key selector 
                            where(s.UserName == username)
                            select new
                            { // result selector 
                                //UserName = s.UserName,
                                RolName = st.RolName
                            };
            string aa = "";
            foreach (var item in innerJoin)
            {
                aa= item.RolName.ToString();
            }

            string[] result = { aa };
            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}