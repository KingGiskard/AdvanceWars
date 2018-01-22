using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour {

    public Camera mainCam;

	void Start ()
    {
		
	}
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider.gameObject.tag == "Unit")
            {
                print(hit.collider.gameObject.name);
            }
        }
	}
}
