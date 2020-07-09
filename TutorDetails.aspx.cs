using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Elearning.App_Code;

namespace Elearning
{
    public partial class TutorDetails : System.Web.UI.Page
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

                if (RoleID == 2) //i.e. Tutor
                {
                    //if request is NOT a postback
                    if (!Page.IsPostBack)
                    {
                        //create instane of middle layer business object 
                        User c = new User();
                        //retrieve tutor Course from middle layer into a DataTable
                        c.CourseID = CID;
                        txtCourse.Text = c.GetUserCourse();

                        DataTable dt = c.GetStudentsByCourse();
                        if (dt != null)
                        {                           
                            //set DropDownList's data source to the DataTable                 
                            lstStudents.DataSource = dt;
                            //assign UserID database field to the value property                 
                            lstStudents.DataValueField = "UserID";
                            //assign RealName database field to the text property                 
                            lstStudents.DataTextField = "RealName";
                            //bind data 
                            lstStudents.DataBind();
                        }                        
                        else
                        {
                            // exception caught - display error
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

        protected void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (lstStudents.SelectedIndex == -1)
            {
                //user not selected display erro message
                lblSuccess.Text = "";
                lblError.Text = "You must select a user to delete.";
            }
            else
            {
                //if request is NOT a postback
                if (Page.IsPostBack)
                {   
                    //assign selected student in list box to UID
                    int UID = Int32.Parse(lstStudents.SelectedItem.Value);
                    //create instane of middle layer business object 
                    User user = new User();
                    //delete user using method from middle layer class
                    if (user.DeleteUser(UID))
                    {
                        //output message if successful
                        lblError.Text = "";
                        lblSuccess.Text = "Student successfully deleted";                        
                    }
                    else
                    {
                        //exception caught - display error
                        lblError.Text = "Database error - user not deleted";
                    }
                    LoadUserList();                    
                }
                else
                {
                    //remove error messages
                    lblSuccess.Text = "";
                    lblError.Text = "";
                }
                
            }            
        }

        private void LoadUserList()
        {
            //create instane of middle layer business object 
            User td = new User();
            
            td.CourseID = Int32.Parse(Session["CourseID"].ToString());
            //retrieve tutors students from middle layer into a DataTable 
            DataTable dt = td.GetTutorByCourse();
            //txtCourse.Text = td.getUserCourse();

            //check if query was successful             
            if (dt != null)
            {
                //set DropDownList's data source to the DataTable                 
                lstStudents.DataSource = dt;
                //assign UserID database field to the value property                 
                lstStudents.DataValueField = "UserID";
                //assign RealName database field to the text property                 
                lstStudents.DataTextField = "RealName";
                //bind data 
                lstStudents.DataBind();
            }
            else
            {
                //exception caught - display error
                lblError.Text = "Something went wrong - cannot display students.";
            }
        }
    }
}