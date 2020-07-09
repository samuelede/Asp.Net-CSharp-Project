using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elearning.App_Code;

namespace Elearning
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if Session has expired or user has not logged in
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            //validate input before connecting to database
            if (txtCurrentPassword.Text == "" || txtNewPassword.Text == "" || txtConfirmPassword.Text == "")
            {
                lblError.Text = "Password fields cannot be empty";
            }
            else if (txtCurrentPassword.Text.Length < 6 || txtNewPassword.Text.Length < 6)
            {
                lblError.Text = "New Password is of invalid length";
            }
            else if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
            {
                lblError.Text = "New Passwords are not the same";
            }
            else
            {
                //create instane of middle layer business object 
                User user = new User
                {
                    UserID = Int32.Parse(Session["UserID"].ToString())
                };

                string currentPassword = user.GetPasswordUsingID();
                user.UserPassword = txtCurrentPassword.Text; //assign current password to be stored and verified

                /** verify if current password is same as old plain stored password and authenticate user
                    Also verify if current password + stored Salt encrypted value is same as stored password
                    then authenticate user.*/
                if (currentPassword.Equals(txtCurrentPassword.Text) || currentPassword.Equals(user.VerifyPassword()))
                {
                    user.UserPassword = txtNewPassword.Text;//assign new password to be encrypted and stored                                                                                  
                    
                    if (user.UpdatePasswordByUserID())
                    {
                        Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Password");
                    }
                    else
                    {
                        //exception caught display error
                        lblError.Text = "Database connectoin error - Unable to Update Password";
                    }                    
                }
                else
                {
                    //display password error
                    lblError.Text = "Invalid Current Password";
                }
            }
        }

        public void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtCurrentPassword.Text.Length < 6)
            {
                lblError.Text = "Current Password is of invalid length";
            }
            else
            {
                lblError.Text = "";
            }
        }

        protected void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtNewPassword.Text.Length < 6)
            {
                lblError.Text = "New Password is of invalid length";
            }
            else
            {
                lblError.Text = "";
            }
        }

        protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtConfirmPassword.Text.Length < 6)
            {
                lblError.Text = "New Password is of invalid length";
            }
            else if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
            {
                lblError.Text = "New Passwords are not the same";
            }
            else
            {
                lblError.Text = "";
            }
        }
    }
}