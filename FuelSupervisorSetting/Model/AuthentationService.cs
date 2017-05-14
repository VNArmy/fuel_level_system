using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System;
using FLS.Business;
using System.Data;

namespace FuelSupervisorSetting.Model
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private class InternalUserData
        {
            public InternalUserData(string username, string email,string nonce, string hashedPassword, string[] roles)
            {
                Username = username;
                Email = email;
                Nonce = nonce;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
            public string Username
            {
                get;
                private set;
            }

            public string Email
            {
                get;
                private set;
            }
            public string Nonce
            {
                get;
                private set;
            }
            public string HashedPassword
            {
                get;
                private set;
            }

            public string[] Roles
            {
                get;
                private set;
            }
        }

        public AuthenticationService()
        {
            BusinessHelper.InitConnection();
            LoadUserList();
        }
        public void LoadUserList()
        {
            DataTable dt = BusinessHelper.ListAllUsers();
            foreach (DataRow dr in dt.Rows)
            {
                string userName = dr["username"].ToString();
                string email = dr["email"].ToString();
                string pw = dr["password"].ToString();
                string nonce = Convert.ToDateTime(dr["nonce"]).ToString("yyyy-MM-dd HH:mm:ss"); //string.Format(@"yyyy-MM-dd HH:mm:ss", dr["nonce"].ToString());
                DataTable dtRole = BusinessHelper.AssignedRoles(userName);
                string[] roles = dtRole.AsEnumerable().Select(r => r.Field<string>("Role")).ToArray();
                _users.Add(new InternalUserData(userName, email, nonce, pw, roles));
            }
        }
        private static readonly List<InternalUserData> _users = new List<InternalUserData>() 
        { 
            //new InternalUserData("Mark", "mark@company.com", 
            //"MB5PYIsbI2YzCUe34Q5ZU2VferIoI4Ttd+ydolWV0OE=", new string[] { "Administrators" }), 
            //new InternalUserData("John", "john@company.com", 
            //"hMaLizwzOQ5LeOnMuj+C6W75Zl5CXXYbwDSHWW9ZOXc=", new string[] { })
        };

        public User AuthenticateUser(string username, string clearTextPassword)
        {
            InternalUserData userData = _users.FirstOrDefault(u => u.Username.Equals(username)
                && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Nonce)));
            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new User(userData.Username, userData.Email, userData.Roles);
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // use a Stringbuilder to append the bytes from the array to create a SHA-256 hash code string.
            StringBuilder sbHash = new StringBuilder();

            // Loop through byte array of the hashed code and format each byte as a hexadecimal code.
            for (int i = 0; i < hash.Length; i++)
            {
                sbHash.Append(hash[i].ToString("x2"));
            }

            // Return the hexadecimal SHA-256 hash code string.
            return sbHash.ToString();
            //// Return the hash as a base64 encoded string to be compared to the stored password
            //return Convert.ToBase64String(hash);
        }
    }

    public class User
    {
        public User(string username, string email, string[] roles)
        {
            Username = username;
            Email = email;
            Roles = roles;
        }
        public string Username
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string[] Roles
        {
            get;
            set;
        }
    }
}