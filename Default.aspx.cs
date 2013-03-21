using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Class1 c1 = new Class1();
        c1.points.Add(new Point(40, 20));
        c1.MinX = 40;

        Class2 c2 = new Class2();
        c2.points.Add(new Point(10, 20));
        c2.MinX = 10;

        Session["Class1"] = c1;
        Session["Class2"] = c2;
    }
}