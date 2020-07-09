using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Elearning.App_Code;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace Elearning
{
    public partial class StudentDetails : System.Web.UI.Page
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
                int CID = Int32.Parse(Session["CourseID"].ToString());
                lblEmail.Text = "";

                if (RoleID == 1) //i.e. Student
                {
                    //if request is NOT a postback
                    if (!Page.IsPostBack)
                    {
                        //create instane of middle layer business object 
                        User c = new User();
                        //retrieve tutor Course from middle layer into a DataTable
                        c.CourseID = Int32.Parse(Session["CourseID"].ToString());
                        txtCourse.Text = c.GetUserCourse();

                        DataTable dt = c.GetTutorByCourse();
                        if (dt != null)
                        {
                            //set DropDownList's data source to the DataTable                 
                            lstTutors.DataSource = dt;
                            //assign UserID database field to the value property                 
                            lstTutors.DataValueField = "UserID";
                            //assign RealName database field to the text property                 
                            lstTutors.DataTextField = "RealName";
                            //bind data 
                            lstTutors.DataBind();
                            
                        }
                        else
                        {
                            lblError.Text = "Something went wrong - cannot display students.";
                        }
                        
                        DataTable rpt = c.GetModuleList();
                        if (rpt != null)
                        {
                            //set DropDownList's data source to the DataTable                 
                            rptModules.DataSource = rpt;
                            //bind data
                            rptModules.DataBind();
                        }
                        else
                        {
                            //exception caught - display error
                            lblError.Text = "Something went wrong - cannot display students.";
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UserAccount.aspx");
                }
            }
        }

        protected void btnShowEmail_Click(object sender, EventArgs e)
        {
            //validate input
            if (lstTutors.SelectedIndex == -1)
            {
                lblError.Text = "You must select a user to display email.";
            }
            else
            {
                if (Page.IsPostBack)
                {
                    lblError.Text = ""; //remove error message
                    int user = Int32.Parse(lstTutors.SelectedValue); //convert
                    User u = new User(); //create instance of middle layer business object 
                    u.UserID = user;
                    lblEmail.Text = "";
                    lblEmail.Text = u.GetUserEmail(); //get and assign email value to textbox
                }
            }System.Threading.Thread.Sleep(1000);
        }
    }
}