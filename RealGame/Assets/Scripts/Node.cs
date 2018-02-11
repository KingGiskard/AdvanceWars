using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parentNode;

    public GameObject worldObject;

    public bool isWalkable = true;

    public int x;
    public int z;

    public float hCost;
    public float gCost;

    public float fCost
    {
        get
        {
            return hCost + gCost;
        }
    }
    public NodeType nodeType;
    public enum NodeType
    {
        air,
        ground
    }
}
