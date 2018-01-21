using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {


        public LayerMask collsionMaskGround;
        public GameObject markerObject;
        public GameObject plane;
        MapGenerator mapGenerator;

    void Start()
    {
        mapGenerator = this.GetComponent<MapGenerator>();
    }

    void onDetect(int sign)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;


        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, collsionMaskGround))
        {


            
            markerObject.transform.position = rayHit.point;

            float percentX = Mathf.InverseLerp(plane.GetComponent<Collider>().bounds.extents.x, -plane.GetComponent<Collider>().bounds.extents.x, rayHit.point.x);
            float percentZ = Mathf.InverseLerp(plane.GetComponent<Collider>().bounds.extents.z, -plane.GetComponent<Collider>().bounds.extents.z, rayHit.point.z);
           
            //mapGenerator.EditMap(percentX, percentZ, sign);
        }
    }
    void Update()
        {
        if (Input.GetMouseButton(0))
        {
            onDetect(1);
        }
        if (Input.GetMouseButton(1))
        {
            onDetect(-1);
        }
        if (Input.GetMouseButton(2))
        {
            onDetect(0);
        }
    }
}
