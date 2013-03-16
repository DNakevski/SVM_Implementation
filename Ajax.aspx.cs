using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null)
        {
            string action = Request.QueryString["action"].ToString();

            switch (action)
            {
                case "addPoint": AddPoint(); break;
                case "removePoint": RemovePoint(); break;
                case "clasify": Clasify(); break;
                default: break;
            }
        }
    }

    protected void Clasify()
    {
        Response.Write("success");
        Response.End();
    }

    protected void AddPoint()
    {
        double x = Convert.ToDouble(Request.Form["x"]);
        double y = Convert.ToDouble(Request.Form["y"]);
        string className = Request.Form["className"].ToString();

        Class1 c1 = (Class1)Session["Class1"];
        Class2 c2 = (Class2)Session["Class2"];

        if (className == "Class1")
        {
            if (c2.MinX >= x)
            {
                Response.Write("error");
                Response.End();
            }

            if (x < c1.MinX)
                c1.MinX = x;

            c1.points.Add(new Point(x, y));
        }

        if (className == "Class2")
        {
            if (c1.MinX <= x)
            {
                Response.Write("error");
                Response.End();
            }

            if (x > c2.MinX)
                c2.MinX = x;

            c2.points.Add(new Point(x, y));
        }

        Response.Write("success");
        Response.End();
    }

    protected void RemovePoint()
    {
        double x = Convert.ToDouble(Request.Form["x"]);
        double y = Convert.ToDouble(Request.Form["y"]);
        string className = Request.Form["className"].ToString();

        Point p = new Point(x, y);

        if (className == "Class1")
        {
            Class1 c1 = (Class1)Session["Class1"];
            foreach (Point p1 in c1.points)
            {
                if (p1.x == p.x && p1.y == p.y)
                {
                    c1.points.Remove(p1); break;
                }
            }

            c1.RecalclulateMinX();
        }

        if (className == "Class2")
        {
            Class2 c2 = (Class2)Session["Class2"];
            foreach (Point p1 in c2.points)
            {
                if (p1.x == p.x && p1.y == p.y)
                {
                    c2.points.Remove(p1); break;
                }
            }

            c2.RecalclulateMinX();
        }

        Response.Write("success");
        Response.End();
    }
}