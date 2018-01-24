using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    GridBase gridBase;

    public Camera mainCam;

    private Node tempStart = null;
    private Node tempEnd = null;

    private GameObject selectedUnit = null;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider.gameObject.tag == "Unit")
            {
                gridBase = GridBase.GetInstance();
                tempStart = gridBase.GetNodeFromVector3(hit.collider.gameObject.transform.position);
                selectedUnit = hit.collider.gameObject;
            }
            else if (hit.collider.gameObject.tag == "Ground")
            {
                gridBase = GridBase.GetInstance();
                if (tempStart != null)
                {
                    tempEnd = gridBase.GetNodeFromVector3(hit.collider.gameObject.transform.position);
                    selectedUnit.GetComponent<UnitMovement>().GetPath(tempStart, tempEnd);

                    selectedUnit = null;
                    tempStart = null;
                    tempEnd = null;
                }                
            }
        }
	}
}
