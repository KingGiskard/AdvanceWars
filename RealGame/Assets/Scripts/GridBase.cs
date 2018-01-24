using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    public int xMax = 12;
    public int yMax = 1;
    public int zMax = 12;

    public float xOffset = 1;
    public float yOffset = 1;
    public float zOffset = 1;

    public Node[, ,] grid;

    public GameObject gridFloorPrefab;

    public Material floorMat1;
    public Material floorMat2;

    public Vector3 startNodePos;
    public Vector3 endNodePos;

    private void Start()
    {
        grid = new Node[xMax, yMax, zMax];

        for (int i = 0; i < xMax; i++)
        {
            for (int j = 0; j < yMax; j++)
            {
                for (int k = 0; k < zMax; k++)
                {
                    float xPos = i * xOffset;
                    float yPos = j * yOffset;
                    float zPos = k * zOffset;

                    GameObject gridObj = Instantiate(gridFloorPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity) as GameObject;
                    if (((i * k) + k) % 2 == 1)
                    {
                        gridObj.GetComponent<Renderer>().material = floorMat1;
                    }
                    else
                    {
                        gridObj.GetComponent<Renderer>().material = floorMat2;
                    }

                    gridObj.transform.name = i.ToString() + " " + j.ToString() + " " + k.ToString();
                    gridObj.transform.parent = transform;

                    Node node = new Node();
                    node.x = i;
                    node.y = j;
                    node.z = k;
                    node.worldObject = gridObj;

                    grid[i, j, k] = node;
                }
            }
        }
    }

    public bool start;

    void Update()
    {
        if (start)
        {
            start = false;

            Pathfinder path = new Pathfinder();
            
        }
    }

    public Node GetNode(int x, int y, int z)
    {
        Node gotVal = null;

        if (x < xMax && x>=0 && y < yMax && y >= 0 && z < zMax && z >= 0)
        {
            gotVal = grid[x, y, z];
        }
        return gotVal;
    }

    public Node GetNodeFromVector3(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        int z = Mathf.RoundToInt(pos.z);

        Node gotVal = GetNode(x, y, z);
        return gotVal;
    }

    public static GridBase instance;
    public static GridBase GetInstance()
    {
        return instance;
    }

     void Awake()
    {
        instance = this;
    }
}