using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elearning.App_Code;

namespace Elearning
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //validate input before connecting to database
            if (txtUsername.Text.Length < 6 || txtUsername.Text.Length > 20)
            {
                lblUsernameError.Text = "Username is invalid length";
            }
            else if (txtPassword.Text.Length < 5)
            {
                lblPasswordError.Text = "Password is invalid length";
            }
            else
            {
                //create instance of middle layer business object 
                User user = new User();

                //set property, so it can be used as a parameter for the query             
                user.UserName = txtUsername.Text;
                user.UserPassword = txtPassword.Text;

                //check if username exists or password mismatch            
                if (user.UserNameExists() && user.AuthenticateUser())
                {
                    //redirect user to
                    Response.Redirect("~/UserAccount.aspx");
                }
                else
                {
                    //already exists or password does not match output error
                    lblError.Text = "Incorrect username and/or password";                 
                    
                }                
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length < 6)
            {
                //validate input before connecting to database
                lblUsernameError.Text = "Username is invalid length";
                lblError.Text = "";// remove error if input is corrected
            }
            else
            {
                lblUsernameError.Text = "";
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //validate input before connecting to database
            if (txtPassword.Text.Length < 5)
            {
                lblPasswordError.Text = "Password is invalid length";
                lblError.Text = "";// remove error if input is corrected
            }
            else
            {
                lblPasswordError.Text = "";
            }
        }
    }
}