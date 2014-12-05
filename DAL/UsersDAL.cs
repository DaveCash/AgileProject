using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using System.Security.Cryptography;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class UsersDAL
    {
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            DBConnection connection = new DBConnection();

            users = connection.ExecuteTypedList<User>("SELECT * FROM UserData", User.Create, null).ToList();

            return users;
        }

        public static List<User> GetUsersByProject(int projectId)
        {
            List<User> users = new List<User>();

            DBConnection connection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });

            users = connection.ExecuteTypedList<User>("SELECT UserData.* FROM UserData INNER JOIN ProjectUsers ON UserData.UserId = ProjectUsers.UserId WHERE ProjectUsers.ProjectId = @ProjectId"
                , User.Create, parameters).ToList();

            return users;
        }

        public static User GetUserById(int userId)
        {
            User user = new User();

            DBConnection connection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@UserId", OleDbType.Integer) { Value = userId });

            user = connection.ExecuteTypedList<User>("SELECT * FROM UserData WHERE ID = @UserId", User.Create, parameters).FirstOrDefault();

            return user;
        }

        public static User GetUserByUsername(string username)
        {
            User user = new User();

            DBConnection connection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@UserName", OleDbType.VarChar) { Value = username });

            user = connection.ExecuteTypedList<User>("SELECT * FROM UserData WHERE Ucase(UserName)=Ucase(@UserName)", User.Create, parameters).FirstOrDefault();

            return user;
        }

        public static User RegisterUser(string username, string password)
        {
            User user = new User();

            string hash = CreateHash(password);

            DBConnection dbConnection = new DBConnection();
            dbConnection.AddCmd("UsernameCheck");

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@UserName", OleDbType.VarChar) { Value = username });

            dbConnection.ExecuteCmd("SELECT * FROM UserData WHERE Ucase(UserName)=Ucase(@UserName)", parameters, "UsernameCheck");

            if (!dbConnection.Reader.Read())
            {
                parameters.Add(new OleDbParameter("@Hash", OleDbType.VarChar) { Value = hash });

                dbConnection.ExecuteNonQuery("" +
                "INSERT INTO " +
                "UserData ([UserName], [UserPassword]) " +
                "VALUES (@UserName,@Hash);", parameters);

                user.UserName = username;

                dbConnection.Close();

                return user;
            }

            dbConnection.Close();

            return null;
        }

        public static User ValidateUser(string username, string password)
        {
            User user = new User();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@UserName", OleDbType.VarChar) { Value = username });

            dbConnection.ExecuteCmd("SELECT * FROM UserData WHERE Ucase(UserName)=Ucase(@UserName)", parameters);

            if (dbConnection.Reader.Read())
            {
                string correctHash = dbConnection.Reader["UserPassword"].ToString();

                if (ValidatePassword(password, correctHash))
                {
                    user = GetUserByUsername(dbConnection.Reader["UserName"].ToString());

                    dbConnection.Close();

                    return user;
                };
            }

            dbConnection.Close();

            return null;
        }

        public static string CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[24];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, 1000, 24);
            return 1000 + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[0]);
            byte[] salt = Convert.FromBase64String(split[1]);
            byte[] hash = Convert.FromBase64String(split[2]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}