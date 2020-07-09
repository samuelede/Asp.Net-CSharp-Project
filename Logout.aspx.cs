using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elearning
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            //remove all Session data for this worker
            Session.Abandon();
            //redirect worker to the login page
            Response.Redirect("~/UserLogin.aspx");
        }
    }
}