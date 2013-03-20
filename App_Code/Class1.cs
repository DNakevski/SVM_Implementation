using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Class1
{
    public string Name;
    protected double minX;

    public List<Point> supportVector;

    public List<Point> points;

    public Class1() 
    {
        points = new List<Point>();
        supportVector = new List<Point>();
    }

    public double MinX
    {
        get { return this.minX; }
        set { this.minX = value; }
    }

    public void RecalclulateMinX()
    {
        this.minX = 1000;
        foreach (Point p in points)
        {
            if (p.x < this.minX)
                this.minX = p.x;
        }
    }

    public List<Point> GetSortedPoints()
    {
        Point[] tempList = new Point[this.points.Count];
        Point tempPoint = new Point();
        this.points.CopyTo(tempList);

        for (int pass = 1; pass < tempList.Length; pass++)
            for (int i = 0; i < tempList.Length - 1; i++)
                if (tempList[i].X > tempList[i + 1].X)
                {
                    tempPoint = tempList[i];
                    tempList[i] = tempList[i + 1];
                    tempList[i + 1] = tempPoint;
                }

        return tempList.ToList();
    }

    public Point GetFirstPoint()
    {
        List<Point> tempList = new List<Point>();
        tempList = GetSortedPoints();

        return tempList[0];
    }

    public double ReturnBasicDistanceByX()
    {
        double result;
        List<Point> tempList = new List<Point>();
        tempList = GetSortedPoints();

        result = tempList[1].X - tempList[0].X;

        return result;
    }

    public double ReturnBasicDistanceByY()
    {
        double result;
        List<Point> tempList = new List<Point>();
        tempList = GetSortedPoints();

        result = tempList[1].Y - tempList[0].Y;

        return result;
    }
}