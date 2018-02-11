using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    GridBase gridBase;
    public Node startPos;
    public Node endPos;

    public List<Node> FindPath()
    {
        return FindActualPath(startPos, endPos);
    }

    public List<Node> FindActualPath(Node start, Node end)
    {
        List<Node> foundPath = new List<Node>();

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    if (!currentNode.Equals(openSet[i]))
                    {
                        currentNode = openSet[i];
                    }
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.Equals(end))
            {
                foundPath = RetracePath(start, currentNode);
                break;
            }

            foreach (Node neighbor in GetNeighbors(currentNode, true))
            {
                if (!closedSet.Contains(neighbor))
                {
                    float newMoveCost = currentNode.gCost + GetDistance(currentNode, neighbor);

                    if (newMoveCost < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMoveCost;
                        neighbor.hCost = GetDistance(neighbor, end);

                        neighbor.parentNode = currentNode;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
        return foundPath;
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();

        return path;
    }

    private List<Node> GetNeighbors(Node node, bool getVertNeighbors = false)
    {
        List<Node> gotList = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            if (x == 0)
            {

            }
            else
            {
                Node searchPos = new Node();

                searchPos.x = node.x + x;
                searchPos.z = node.z;

                Node newNode = GetNeighborNode(searchPos, node);

                if (newNode != null)
                {
                    gotList.Add(newNode);
                }
            }
        }
        for (int z = -1; z <= 1; z++)
        {
            if (z == 0)
            {

            }
            else
            {
                Node searchPos = new Node();

                searchPos.x = node.x;
                searchPos.z = node.z + z;

                Node newNode = GetNeighborNode(searchPos, node);

                if (newNode != null)
                {
                    gotList.Add(newNode);
                }
            }
        }
        return gotList;
    }

    private Node GetNeighborNode(Node adjPos, Node curNodePos)
    {
        Node gotVal = null;

        Node node = gridBase.GetNode(adjPos.x, adjPos.z);

        if (node != null && node.isWalkable)
        {
            gotVal = node;
        }
        return gotVal;
    }

    private int GetDistance(Node pos1, Node pos2)
    {
        int xDist = Mathf.Abs(pos1.x - pos2.x);
        int zDist = Mathf.Abs(pos1.z - pos2.z);

        if (xDist > zDist)
        {
            return 14 * zDist + 10 * (xDist - zDist);
        }
        return 14 * xDist + 10 * (zDist - xDist);
    }
}
