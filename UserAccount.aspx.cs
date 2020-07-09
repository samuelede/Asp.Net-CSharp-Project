using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elearning
{
    public partial class UserAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if Session has expired or user has not logged in
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                //retrieve neccessary session data, casting into variables
                string RealName = (string)Session["RealName"];
                int RoleID = Int32.Parse(Session["RoleID"].ToString());

                //assign the user's real name to the welcome label
                lblWelcome.Text = "Welcome " + RealName + ".";
                lblUserAccount.Text = "Student Account";

                if (RoleID == 2) //i.e. Tutor Account
                {
                    //make tutor only button visible
                    btnUpdateTutorCourse.Visible = true;
                    lblTutorChangePassword.Visible = true;

                    //change Text and PostBack Url properties for admin
                    btnUserDetails.Text = "Tutor Details";
                    lblUserAccount.Text = "Tutor Account";
                    btnUserDetails.PostBackUrl = "~/TutorDetails.aspx";
                }

                if (Request.QueryString.HasKeys())
                {
                    //display associated success message after page redirect
                    if (Request.QueryString["UpdateSuccess"].Equals("Password"))
                    {
                        lblUpdateSuccess.Text = "You successfully changed your password";
                    }
                    else if (Request.QueryString["UpdateSuccess"].Equals("Course"))
                    {
                        lblUpdateSuccess.Text = "You successfully changed your course";
                    }
                }

                if (Page.IsPostBack)
                {
                    //remove success or error message
                    lblUpdateSuccess.Text = "";
                }
            }
        }
    }
}