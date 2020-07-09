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
    public partial class UpdateTutorCourse : System.Web.UI.Page
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
                        Course c = new Course();
                        
                        //create instane of middle layer business object 
                        User td = new User();

                        //retrieve tutors students from middle layer into a DataTable 
                        DataTable dt = td.GetTutorByCourse();

                        //pass CourseId from session into business object
                        td.CourseID = Int32.Parse(Session["CourseID"].ToString());

                        //display user course in Textbox
                        txtCourse.Text = td.GetUserCourse();

                        DataTable dtc = c.getAllCourses();
                        if (dtc != null)
                        {     
                            //set DropDownList's data source to the DataTable                 
                            lstCourses.DataSource = dtc;
                            //assign UserID database field to the value property                 
                            lstCourses.DataValueField = "CourseID";
                            //assign RealName database field to the text property                 
                            lstCourses.DataTextField = "CourseName";
                            //bind data 
                            lstCourses.DataBind();
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

        protected void btnUpdateCourse_Click(object sender, EventArgs e)
        {
            //validate input
            if (lstCourses.SelectedIndex == -1)
            {
                lblError.Text = "You must select a course to update";
            }
            else if(lstCourses.SelectedItem.Value.Equals(Session["CourseID"].ToString()))
            {
                lblError.Text = "Selected Course is the same as Current Course.";
            }
            else
            {
                if (Page.IsPostBack)
                {
                    //assign selected course in list box to cid
                    int cid = Int32.Parse(lstCourses.SelectedItem.Value);
                    //create instance of middle layer business object
                    User tc = new User();
                    //delete course using method from middle layer class
                    tc.CourseID = cid;
                    //retrive current user id and pass to middle layer class
                    tc.UserID = Int32.Parse(Session["UserID"].ToString());

                    if (tc.UpdateTutorCourse())
                    {
                        //redirect and display success message 
                        Response.Redirect("UserAccount.aspx?UpdateSuccess=Course");
                    }
                    else
                    {
                        //exception caught display error
                        lblError.Text = "Database connection error - update not successful";
                    }
                }                   
            }
            System.Threading.Thread.Sleep(1000);
        }
    }
}