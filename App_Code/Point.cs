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
}