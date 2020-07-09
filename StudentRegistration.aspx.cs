using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Elearning.App_Code;
using System.Security.Cryptography;
using System.Text;

namespace Elearning
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if request is NOT a postback
            if (!Page.IsPostBack)
            {
                //create instane of middle layer business object 
                Course crs = new Course();
                //retrieve departments using middle layer into a DataTable 
                DataTable dt = crs.getAllCourses();

                //check if query was successful             
                if (dt != null)
                {
                    //set DropDownList's data source to the DataTable                 
                    ddlCourses.DataSource = dt;
                    //assign DepartmentID database field to the value property                 
                    ddlCourses.DataValueField = "CourseID";
                    //assign DepartmentName database field to the text property                 
                    ddlCourses.DataTextField = "CourseName";
                    //bind data 
                    ddlCourses.DataBind();
                }
                else
                {
                    // exception thrown - display error
                    lblError.Text = "Something went wrong - cannot display courses.";
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //validate input
            if (txtUsername.Text.Length < 6 || txtUsername.Text.Length > 20)
            {
                lblError.Text = "Username must be at least 6 characters long.";
            }
            else if (txtUsername.Text.Equals(txtPassword.Text))
            {
                lblError.Text = "Username and Password must be different.";
            }
            else if (txtPassword.Text.Length < 5)
            {
                lblError.Text = "Password must be at least 5 characters long.";
            }
            else if (!txtConfirmPassword.Text.Equals(txtPassword.Text))
            {
                lblError.Text = "Please confirm password.";
            }
            else if (txtRealName.Text.Equals(""))
            {
                lblError.Text = "Please enter your full name.";
            }
            else if (txtEmailAddress.Text.Equals("") || !txtEmailAddress.Text.Contains("dmu1.ac.uk"))
            {
                lblError.Text = "Invalid email address - hint: email@dmu1.ac.uk";
            }
            else
            {
                //create instane of middle layer business object 
                User user = new User();

                //set property, so it can be used as a parameter for the query             
                user.UserName = txtUsername.Text;

                //check if username exists             
                if (user.UserNameExists())
                {
                    //already exists so output error 
                    lblError.Text = "Username already exists, please select another";
                }
                else
                {
                    //set properties, so it can be used as a parameter for the query           
                    user.UserName = txtUsername.Text;
                    user.UserPassword = txtPassword.Text;
                    user.RealName = txtRealName.Text;
                    user.EmailAddress = txtEmailAddress.Text;
                    user.CourseID = Int32.Parse(ddlCourses.SelectedValue);
                    user.RoleID = 1; //hard coding the role ID for student 
                                                         
                    //attempt to add a worker and test if it is successful           
                    if (user.AddUser())
                    {
                        //redirect user to login page 
                        Response.Redirect("~/UserLogin.aspx?Registration=Success");
                    }
                    else
                    {
                        //exception thrown so display error 
                        lblError.Text = "Database connection error - failed to insert record.";
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtUsername.Text.Equals("") || txtUsername.Text.Length < 6 || txtUsername.Text.Length > 20)
            {
                lblError.Text = "Username must be at least 6 characters long.";
            }
            else
            {
                lblError.Text = "";
            }
        }
        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtPassword.Text.Length < 5)
            {
                lblError.Text = "Password must be at least 5 characters long.";
            }
            else
            {
                lblError.Text = "";
            }
        }

        protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (!txtConfirmPassword.Text.Equals(txtPassword.Text))
            {
                lblError.Text = "Please confirm password.";
            }
            else
            {
                lblError.Text = "";
            }
        }

        protected void txtRealName_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtRealName.Text.Length < 4)
            {
                lblError.Text = "Please enter your full name.";
            }
            else
            {
                lblError.Text = "";
            }
        }

        protected void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            //validate input
            if (txtEmailAddress.Text.Equals("") || !txtEmailAddress.Text.Contains("dmu1.ac.uk"))
            {
                lblError.Text = "Invalid email address - hint: email@dmu1.ac.uk";
            }
            else
            {
                lblError.Text = "";
            }
        }
    }
}