using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitControl : MonoBehaviour {
    public LayerMask collisionMaskObstacle;
    public CP cP;
    public GameObject[] commandPoints;
     bool noneSelected;
    public bool multiSelect;
     bool movement;

    

    public List<Unit> units;
    public List<Unit> unitsActive;

    void Start() {
        List<Unit> units = new List<Unit>();
        List<Unit> unitsActive = new List<Unit>();
       
    }
    void UpdateActive()
    {
        foreach (Unit u in units)
        {
            if (u.selected == true && !unitsActive.Contains(u))
            {
                unitsActive.Add(u);
            }
            else if (u.selected == false && unitsActive.Contains(u))
            {
                unitsActive.Remove(u);
            }
        }
    }
    void Update () {

        UpdateActive();
    
        commandPoints = GameObject.FindGameObjectsWithTag("CP");
        if (Input.GetKeyDown(KeyCode.M))
        {
            movement = true;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            movement = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            multiSelect = false;

        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            multiSelect = true;
        }
        if (Input.GetMouseButtonDown(0) && movement && noneSelected == false)
        {
            
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, collisionMaskObstacle))
            {
                
                CP newCP = Instantiate(cP, transform.position, transform.rotation) as CP;
                
                newCP.transform.position = rayHit.point;


            }
        }
    }
    void LateUpdate()
    {
        for (int i = 0; i < commandPoints.Length; i++)
        {
            Destroy(commandPoints[i]);
        }
    }
}
