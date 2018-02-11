using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase
{
    public Node[,] grid;

    public int xMax;
    public int zMax;

    private float xOffset = 1;
    private float zOffset = 1;

    public GridBase(int _x_max, int _z_max)
    {
        xMax = _x_max;
        zMax = _z_max;

        grid = new Node[xMax, zMax];

        for (int i = 0; i < xMax; i++)
        {
            for (int k = 0; k < zMax; k++)
            {
                float xPos = i * xOffset;
                float zPos = k * zOffset;

                Node node = new Node();
                node.x = i;
                node.z = k;

                grid[i, k] = node;
            }
        }
    }

    public Node GetNode(int x, int z)
    {
        Node gotVal = null;

        if (x < xMax && x >= 0 && z < zMax && z >= 0)
        {
            gotVal = grid[x, z];
        }
        return gotVal;
    }

    public Node GetNodeFromVector3(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int z = Mathf.RoundToInt(pos.z);

        Node gotVal = GetNode(x, z);
        return gotVal;
    }

    public Node GetNodeFromVector2(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int z = Mathf.RoundToInt(pos.y);

        Node gotVal = GetNode(x, z);
        return gotVal;
    }
}