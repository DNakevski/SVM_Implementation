using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Class2
{
    public string Name;
    protected double minX;

    public List<Point> points;
    
    public Class2() 
    {
        points = new List<Point>();
    }

    public double MinX
    {
        get { return this.minX; }
        set { this.minX = value; }
    }

    public void RecalclulateMinX()
    {
        this.minX = -1000;
        foreach (Point p in points)
        {
            if (p.x > this.minX)
                this.minX = p.x;
        }
    }
}