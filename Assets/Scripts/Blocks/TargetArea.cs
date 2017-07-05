﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public GameObject gridSource;
    
    public TargetBlock[,] grids; // true enabled, false disabled.
    
    int height;
    int width;
    
    Vector2 baseloc;
    Vector2 halfloc;
    
    public bool completed { get
        {
            for(int i=0; i<height; i++)
                for(int j=0; j<width; j++)
                    if(grids[i,j] && !grids[i,j].established) return false;
            return true;
        }}
    
    
    void Start() 
    {
        Pattern p = Pattern.randomTarget;
        height = p.height;
        width = p.width;
        
        baseloc = new Vector2(- width * 0.5f, height * 0.5f);
        halfloc = new Vector2( 0.5f, -0.5f);
        
        grids = new TargetBlock[height, width]; // form left-top corner to right-bottom corner.
        
        for(int i=0; i<height; i++)
            for(int j=0; j<width; j++)
                if(p[i,j])
                {
                    GameObject g = Instantiate(gridSource, this.gameObject.transform);
                    g.name = "grid(" + i + "," + j + ")";
                    g.transform.localPosition = new Vector2(i, -j) + baseloc + halfloc;
                    grids[i,j] = g.GetComponent<TargetBlock>();
                }
                else
                {
                    grids[i,j] = null;
                }
    }
    
    void Update()
    {
    }
    
    public struct Point
    {
        public int x,y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    
    public Point GetLoc(Vector2 loc)
    {
        Vector2 relloc = loc - (Vector2)this.gameObject.transform.position;
        Vector2 indloc = relloc - baseloc;
        return new Point(Mathf.FloorToInt(indloc.x), Mathf.FloorToInt(-indloc.y));
    }
    
    bool InRange(Point v) { return v.x >= 0 && v.x < width && v.y >= 0 && v.y < height; }
    
    public TargetBlock GetNearestBlock(Vector2 loc)
    {
        Point v = GetLoc(loc);
        if(InRange(v)) return grids[v.x,v.y];
        else return null;
    }
    
    public void ReportCompleted()
    {
        /// TODO!!!
    }
}
