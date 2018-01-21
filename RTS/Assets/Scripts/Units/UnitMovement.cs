using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour
{
     Transform target;
    float speed = 20;
    Vector3[] path;
    int targetIndex;
     UnitControl unitControl;
    Vector3 velocity;
    Unit selfUnit;
    NeutralAi beast;
    Grid grid;
   


    void Start()
    {
        GameObject pathfinder = GameObject.FindGameObjectWithTag("PathFinder");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        unitControl = player.GetComponent<UnitControl>();
        selfUnit = this.GetComponent<Unit>();
        
        grid = pathfinder.GetComponent<Grid>();
    }

    public void PathingStart(Vector3 target)
    {
        path = new Vector3[0];
      
        PathRequestManager.RequestPath(transform.position, target, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length != 0)
        {
            Vector3 currentWaypoint = path[0];

            while (true)
            {

                if (transform.position == new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z))
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        targetIndex = 0;
                        path = new Vector3[0];
                        
                        yield break;
                      
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.LookAt(new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z), speed * Time.deltaTime);
             
                yield return null;

            }
        }
    }
    public void Update()
    {
      
        Ray ray = new Ray(new Vector3(transform.position.x + GetComponent<Collider>().bounds.extents.x, transform.position.y, transform.position.z), -transform.up);

        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, unitControl.collisionMaskObstacle))
        {

        velocity.y = -rayHit.distance  + GetComponent<Collider>().bounds.extents.y;
        transform.Translate(velocity);

         }
    }
    public void OnDrawGizmos()
    {
         if (path != null)
         {
             for (int i = targetIndex; i < path.Length; i++)    
             {
                 Gizmos.color = Color.black;
                 Gizmos.DrawCube(path[i], Vector3.one);
                 
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}