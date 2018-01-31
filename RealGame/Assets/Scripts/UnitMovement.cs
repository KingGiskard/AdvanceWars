using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    Pathfinder path;

    public float moveSpd;

    private List<Node> p;

    private Vector3 curPos = Vector3.zero;
    private float moveAmount = 0;

	void Start ()
    {
        path = new Pathfinder();
	}

    private void Update()
    {
        if (p != null && p.Count > 0)

        {
            if (curPos == Vector3.zero)
            {
                curPos = gameObject.transform.position;
            }
            if (moveAmount == 0)
            {
                gameObject.transform.LookAt(new Vector3(p[0].x, transform.position.y, p[0].z));
                moveAmount = Vector3.Distance(curPos, new Vector3(p[0].x, transform.position.y, p[0].z)) * Time.deltaTime * moveSpd;
            }
            if (Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(p[0].x, p[0].z)) > 0.1f)
            {
                gameObject.transform.Translate(Vector3.forward * moveAmount);
            }
            else
            {
                curPos = Vector3.zero;
                moveAmount = 0;
                p.RemoveAt(0);
            }
        }
    }

	public void GetPath(Node start, Node end)
    {
        path.startPos = start;
        path.endPos = end;

        p = path.FindPath();
    }
}
