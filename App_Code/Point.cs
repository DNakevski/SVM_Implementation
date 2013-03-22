using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Point
{
    public double x;
    public double y;

    public Point() { }

    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public double X
    {
        get { return this.x; }
        set { this.x = value; }
    }

    public double Y
    {
        get { return this.y; }
        set { this.y = value; }
    }

    public double CalculateDistance(Point p1, Point p2)
    {
        double result = 0;
        double dx = 0;
        double dy = 0;

        if(p1.X > p2.X)
        {
            dx = Math.Pow((p1.X - p2.X), 2);
        }
        else
        {
            dx = Math.Pow((p2.X - p1.X), 2);
        }

        if (p1.Y > p2.Y)
        {
            dy = Math.Pow((p1.Y - p2.Y), 2);
        }
        else
        {
            dy = Math.Pow((p2.Y - p1.Y), 2);
        }

        result = Math.Sqrt(dx + dy);

        return result;
    }

    public Point ReturnMiddlePoint(Point p1, Point p2)
    {
        double _x = ((p1.X) + (p2.X)) / 2;
        double _y = ((p1.Y) + (p2.Y)) / 2;

        Point middlePoint = new Point(_x, _y);


        return middlePoint;
    }
}