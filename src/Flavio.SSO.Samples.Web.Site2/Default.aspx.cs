using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flavio.SSO.Samples.Web.Site2
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblName.Text = Request.ServerVariables["HTTP_SSO.USER"];
			lblGroups.Text = Request.ServerVariables["HTTP_SSO.GROUPS"];
		}
	}
}