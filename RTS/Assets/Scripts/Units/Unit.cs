using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    public bool selected = false;
    public bool movingTowardsPoint = false;
    GameObject targetCP;
    [HideInInspector]
    public Vector3 target;

    Rect screenBoxPos;
    UnitControl unitControl;
    BoundingBox boundingBox;
    public UnitMovement unitMovement;
    public Vector2 targetOffset;
    Vector3 velocity;

    void Start()
    {

        
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        unitControl = player.GetComponent<UnitControl>();
        boundingBox = player.GetComponent<BoundingBox>();
        unitMovement = this.GetComponent<UnitMovement>();
        
        unitControl.units.Add(this);
    }

  

    void BoxTest()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenBoxPos = new Rect(screenPos.x - 10, Screen.height - (screenPos.y + 10), 20, 20);
        if (boundingBox.bounds.Contains(screenBoxPos.position))
        {
     
            selected = true;
            movingTowardsPoint = true;
        }
    }
  
    void OnMouseDown()
    {
       
        if (unitControl.multiSelect == true)
        {
            {
                foreach (Unit unit in unitControl.units)
                {
                    unit.selected = false;

                }
            }
            if (selected == true)
            {
                selected = false;
             
            }
            else
            {
                selected = true;
                movingTowardsPoint = true;
            }
            movingTowardsPoint = true;
        }
     if(unitControl.multiSelect == false)
        {
            if (selected == true)
            {
                selected = false;
                
            }
            else
            {
                selected = true;
                movingTowardsPoint = true;
            }
        }
    }

    void Update()
    {





        if (selected == true)
        {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        if (GameObject.FindGameObjectsWithTag("CP").Length != 0 && selected == true)
        {
            
            GameObject targetCP = GameObject.FindGameObjectWithTag("CP");
            Vector3 target = new Vector3 (targetCP.transform.position.x + targetOffset.x, targetCP.transform.position.z, targetCP.transform.position.z + targetOffset.y);
            unitMovement.PathingStart(target);
            Destroy(targetCP);
           
        }
 
    }
}
   
