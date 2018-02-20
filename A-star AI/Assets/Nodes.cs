using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Small script with all set values to calculate the most efficient path.
/// </summary>
public class Nodes{

    public bool walkable;

    public Vector3 worldPos;

    public int gCost;
    public int hCost;

    public Nodes parent;

    public int gridX;
    public int gridY;


    public Nodes(bool _walkable,Vector3 _worldPos, int _gridX,int _gridY)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
            
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
