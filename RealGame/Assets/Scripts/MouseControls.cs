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
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Unit")
                {
                    if (selectedUnit == null)
                    {
                        tempStart = gridBase.GetNodeFromVector3(hit.collider.gameObject.transform.position);
                        selectedUnit = hit.collider.gameObject;
                    }
                    else if (selectedUnit != null)
                    {
                        selectedUnit.GetComponent<UnitBehavior>().TryAttack(hit.collider.gameObject);

                        selectedUnit = null;
                        tempStart = null;
                        tempEnd = null;
                    }
                }
                else if (hit.collider.gameObject.tag == "Ground")
                {
                    if (tempStart != null)
                    {
                        tempEnd = gridBase.GetNodeFromVector3(hit.collider.gameObject.transform.position);
                        selectedUnit.GetComponent<UnitBehavior>().TryMove(tempStart, tempEnd);

                        selectedUnit = null;
                        tempStart = null;
                        tempEnd = null;
                    }
                }
            }
            else
            {
                selectedUnit = null;
                tempStart = null;
                tempEnd = null;
            }           
        }
	}
}
