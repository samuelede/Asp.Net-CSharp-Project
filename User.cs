using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;

namespace Elearning.App_Code
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserSalt { get; set; }
        public string UserHashedPassword { get; set; }
        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public int CourseID { get; set; }
        public int RoleID { get; set; }

        private DatabaseConnection dataConn;
        
        public User()
        {
            dataConn = new DatabaseConnection();
        }

        public bool UserNameExists()
        {
            //assign input username and check if it exist
            dataConn.addParameter("@UserName", UserName);
            string command = "Select COUNT(UserName) FROM Users WHERE UserName = @UserName";

            int result = dataConn.executeScalar(command); //result of count  
            return result > 0 || result == -1; //if record found or exception caught 
        }

        public bool AuthenticateUser()
        {
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", UserPassword);

            string command = "SELECT UserID, UserPassword, SaltPassword, RealName, CourseID, RoleID FROM Users " +
                            "WHERE UserName=@UserName";

            DataTable table = dataConn.executeReader(command);
            
            UserSalt = table.Rows[0]["SaltPassword"].ToString();
            EncryptPassword();
            //verifyPassword();
            
            if (UserPassword.Equals(table.Rows[0]["UserPassword"].ToString()) || UserHashedPassword.Equals(table.Rows[0]["UserPassword"].ToString()))
            {
                HttpContext.Current.Session["UserID"] = table.Rows[0]["UserID"].ToString();
                HttpContext.Current.Session["RealName"] = table.Rows[0]["RealName"].ToString();
                HttpContext.Current.Session["CourseID"] = table.Rows[0]["CourseID"].ToString();
                HttpContext.Current.Session["RoleID"] = table.Rows[0]["RoleID"].ToString();

                return true;
            }
            else
            {
                return false;
            }
        }

        public string VerifyPassword()
        {
            dataConn.addParameter("@UserID", UserID);//get UserID
            //get Salt stored
            string command = "Select SaltPassword FROM Users WHERE UserID=@UserID";
            DataTable table = dataConn.executeReader(command);

            //assign UserSalt retrieved
            UserSalt = table.Rows[0]["SaltPassword"].ToString();
            
            EncryptPassword();//encrypt password

            return UserHashedPassword;
        }
        public bool EncryptPassword()
        {      
            string data = UserPassword + UserSalt;//combine raw password and salt

            Encoding en = Encoding.Default;//get Encoding list 
            byte[] input = en.GetBytes(data);//convert new password combination to bytes

            //The hash to sign. 
            byte[] hash;
            SHA256 sha256 = SHA256.Create();

            hash = sha256.ComputeHash(input);//new hash password

            //Convert hash in byte to string
            string hashpassword = Convert.ToBase64String(hash);
            UserHashedPassword = hashpassword;
            
            return true;
        }   
        
        public string GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            UserSalt = Convert.ToBase64String(salt);//generate salt
            return UserSalt;
        }
        public bool UpdatePasswordByUserID()
        {
            //update encrypted password with new salt
            UserSalt = GenerateSalt();
            EncryptPassword();
            dataConn.addParameter("@UserPassword", UserHashedPassword);
            dataConn.addParameter("@SaltPassword", UserSalt);
            dataConn.addParameter("@UserID", UserID);

            string command = "UPDATE Users SET UserPassword=@UserPassword, SaltPassword=@SaltPassword WHERE UserID=@UserID";

            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }
        public string GetPasswordUsingID()
        {
            dataConn.addParameter("@UserID", UserID);

            string command = "Select UserPassword FROM Users WHERE UserID=@UserID";

            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["UserPassword"].ToString();
            }
            else
            {
                return "";
            }
        }

        public bool GetSaltUsingID()
        {
            
            dataConn.addParameter("@UserID", UserID);

            string command = "Select SaltPassword FROM Users WHERE UserID=@UserID";

            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                UserSalt = table.Rows[0]["SaltPassword"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddUser()
        {
            GenerateSalt();//get salt value
            EncryptPassword();//encrypt and get new password

            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", UserHashedPassword);
            dataConn.addParameter("@UserSalt", UserSalt);
            dataConn.addParameter("@RealName", RealName);
            dataConn.addParameter("@EmailAddress", EmailAddress);
            dataConn.addParameter("@CourseID", CourseID);
            dataConn.addParameter("@RoleID", RoleID);


            string command = "INSERT INTO Users (UserName, UserPassword, SaltPassword, RealName, EmailAddress, CourseID, RoleID) " +
                            "VALUES (@UserName, @UserPassword, @UserSalt, @RealName, @EmailAddress, @CourseID, @RoleID)";


            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected 
        }
        public DataTable GetStudentsByCourse()
        {
            dataConn.addParameter("@CourseID", CourseID);
            string command = "SELECT UserID, RealName from Users WHERE CourseID=@CourseID AND RoleID = 1";
            return dataConn.executeReader(command);
        }
        public DataTable GetTutorByCourse()
        {
            dataConn.addParameter("@CourseID", CourseID);
            string command = "SELECT UserID, RealName from Users WHERE CourseID=@CourseID AND RoleID = 2";
            return dataConn.executeReader(command);
        }
        public DataTable GetModuleList()
        {
            dataConn.addParameter("@CourseID", CourseID);
            string command = "SELECT ModuleCode, ModuleName FROM Modules " +
                "INNER JOIN CourseModules On Modules.ModuleID = CourseModules.ModuleID " +
                "WHERE CourseID = @CourseID " +
                "ORDER BY ModuleCode";
            
            return dataConn.executeReader(command);
        }

        public string GetUserCourse()
        {
            dataConn.addParameter("@CourseID", CourseID);
            string command = "SELECT CourseName from Courses WHERE CourseID=@CourseID";
            
            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["CourseName"].ToString();
            }
            else
            {
                return "Not Found - Database Error";
            }
        }

        public bool DeleteUser(int UserID)
        {
            dataConn.addParameter("@UserID", UserID);
            string command = "DELETE FROM Users WHERE UserID=@UserID";
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }              

        public bool UpdateTutorCourse()
        {
            dataConn.addParameter("@UserID", UserID);
            dataConn.addParameter("@CourseID", CourseID);

            string command = "UPDATE Users SET CourseID=@CourseID WHERE UserID =@UserID";
            HttpContext.Current.Session["CourseID"] = CourseID;
            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        public string GetUserEmail()
        {
            dataConn.addParameter("@UserID", UserID);

            string command = "SELECT EmailAddress FROM Users WHERE UserID=@UserID";

            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["EmailAddress"].ToString();
            }
            else
            {
                return "";
            }
        }
    }
}

    

