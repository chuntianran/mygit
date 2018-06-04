using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//格子
public class Grid  {
    
    public int x { get; set; }
    public int y { get; set; }
    public float g { get; set; }
    public float h { get; set; }
    public float f { get { return g + h; } set { f = value; } }
    public Grid parent { get; set; }

    public Grid(int x,int y){

        this.x = x;
        this.y = y;
    }


}
