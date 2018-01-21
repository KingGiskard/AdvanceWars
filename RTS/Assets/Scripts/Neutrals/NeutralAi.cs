using UnityEngine;
using System.Collections;

public class NeutralAi : MonoBehaviour {
    Vector3 velocity;
    bool hunting;
    bool walkingToPoint;
    UnitMovement unitMovement;
    int xOffset;
    int yOffset;
    bool firstTime;
    float startTime;
    float endTime;
    UnitControl unitControl;
    Unit target;

    void Start()
    {
       
        unitControl = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitControl>();
        unitMovement = this.GetComponent<UnitMovement>();
    }
    void check()
    {
        foreach (Unit unit in unitControl.units)
        {

            print(hunting);
            if (Vector3.Distance(unit.transform.position, transform.position) < 200)
            {
             
                target = unit;
                hunting = true;
            }
        }
    }
    void Update()
    {
     
     
       
        if (hunting)
        {
           
                if (Physics.Linecast(transform.position, target.transform.position))
            {
                {
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y + this.gameObject.GetComponent<Collider>().bounds.extents.y, target.transform.position.z));
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), 20*Time.deltaTime);
                }
                   
            }
                 Ray ray = new Ray(new Vector3(transform.position.x + GetComponent<Collider>().bounds.extents.x, transform.position.y, transform.position.z), -transform.up);

            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, unitControl.collisionMaskObstacle))
            {

                velocity.y = -rayHit.distance + GetComponent<Collider>().bounds.extents.y;
                transform.Translate(velocity);
            }
        }
        if (hunting == false && (firstTime == true || Time.time >= endTime))
        {
            startTime = Time.time;

            check();
           


        }
       
            
       
    }
}
