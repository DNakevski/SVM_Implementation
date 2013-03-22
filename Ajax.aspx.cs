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
        Class1 c1 = (Class1)Session["Class1"];
        Class2 c2 = (Class2)Session["Class2"];

        if (c1.points.Count < 2 || c2.points.Count < 2)
        {
            Response.Write("NoPoints");
            Response.End();
        }

        double x1 = c1.ReturnBasicDistanceByX();
        double x2 = c2.ReturnBasicDistanceByX();

        double dx = 0;
        double dy = 0;

        if (x1 < x2)
        {
            dy = c1.ReturnBasicDistanceByY();

            dx = x1;
            if (dy < 0)
            {
                dx = x1 * (-1);
                dy = dy * (-1);
            }
        }
        else
        {
            dy = c2.ReturnBasicDistanceByY();
            dx = x2;
            if (dy < 0)
            {
                dx = x2 * (-1);
                dy = dy * (-1);
            }
        }


        Point firstPoint = c1.GetFirstPoint();
        Point startPoint = new Point(firstPoint.X - (dx), firstPoint.Y - dy);

        Point firstPoint2 = c2.GetFirstPoint();
        Point startPoint2 = new Point(firstPoint2.X - (dx), firstPoint2.Y - dy);

        string series1 = "[";
        string series2 = "[";

        Point minYPoint = FindSmallestY(c1.points, c2.points);
        Point maxYPoint = FindLargestY(c1.points, c2.points);

        series1 += "[" + startPoint.X + ", " + startPoint.Y + "],";
        series2 += "[" + startPoint2.X + ", " + startPoint2.Y + "],";


        while (startPoint.Y <= maxYPoint.Y)
        {

            startPoint = new Point(startPoint.X + dx, startPoint.Y + dy);
            series1 += "[" + startPoint.X + ", " + startPoint.Y + "],";

            startPoint2 = new Point(startPoint2.X + dx, startPoint2.Y + dy);
            series2 += "[" + startPoint2.X + ", " + startPoint2.Y + "],";
        }

        series1 = series1.Substring(0, series1.Length - 1);
        series1 += "]";
        series2 = series2.Substring(0, series2.Length - 1);
        series2 += "]";


        Point p = new Point();
        Point middlePoint = p.ReturnMiddlePoint(startPoint, startPoint2);

        string series3 = "[["+ middlePoint.X +", "+ middlePoint.Y +"],";
        dx = dx * (-1);
        while (middlePoint.Y >= minYPoint.Y)
        {
            middlePoint = new Point(middlePoint.X + dx, middlePoint.Y - Math.Abs(dy));
            series3 += "[" + middlePoint.X + ", " + middlePoint.Y + "],";
        }

        series3 = series3.Substring(0, series3.Length - 1);
        series3 += "]";

        Response.Write(series1 + "#" + series2 + "#" + series3);
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


    protected Point FindSmallestY(List<Point> l1, List<Point> l2)
    {
        List<Point> unionList = l1.Union(l2).ToList();
        Point min = unionList[0];

        foreach (Point p in unionList)
        {
            if (p.Y < min.Y)
                min = p;
        }

        return min;
    }

    protected Point FindLargestY(List<Point> l1, List<Point> l2)
    {
        List<Point> unionList = l1.Union(l2).ToList();
        Point max = unionList[0];

        foreach (Point p in unionList)
        {
            if (p.Y > max.Y)
                max = p;
        }

        return max;
    }
}