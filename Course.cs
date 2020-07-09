using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Elearning.App_Code
{
    public class Course
    {
        private DatabaseConnection dataConn;
        public int CourseID { get; set; }

        public Course()
        {
            dataConn = new DatabaseConnection();
        }

        public DataTable getAllCourses()
        {
            string command = "SELECT * FROM Courses";
            return dataConn.executeReader(command);
        }
                
        public DataTable getTutorCourse(int CourseID)
        {
            dataConn.addParameter("@CourseID", CourseID);
            string command = "SELECT CourseName FROM Courses WHERE CourseID=@CourseID";

            DataTable table = dataConn.executeReader(command);
            
            if (table.Rows.Count > 0)
            {
                //HttpContext.Current.Session["CourseID"] = table.Rows[0]["CourseID"].ToString();
                HttpContext.Current.Session["CourseName"] = table.Rows[0]["CourseName"].ToString();

                //return true;
            }
            else
            {
                //return false;
            }
                    
            
            return dataConn.executeReader(command);
        }

        public DataTable getStudentCourses()
        {
            dataConn.addParameter("@CourseID", CourseID);
            string command = "SELECT ModuleName, ModuleCode " +
                                "FROM Modules " +
                                "INNER JOIN CourseModules " +
                                "ON Modules.ModuleID = CourseModules.ModuleID " +
                                "WHERE CourseID=@CourseID " +
                                "ORDER BY ModuleName";
            return dataConn.executeReader(command);
        }
    }
}