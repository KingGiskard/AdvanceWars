using UnityEngine;
using System.Collections;

public class CameraWasd : MonoBehaviour {
    public float speed = 20;
    public Transform camera;
    


    // Update is called once per frame
    void Update()
    {




        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camera.eulerAngles.y, transform.eulerAngles.z);
        if (Input.GetKey(KeyCode.W)){
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
				transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
				transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.A))
            {
				transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        
    }
}
