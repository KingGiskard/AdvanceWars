using UnityEngine;
using System.Collections;

public class BoundingBox : MonoBehaviour {
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;
    public Texture selectTexture;
    public Rect bounds;
    UnitFormations unitFormations;
    public Camera camera1;
     UnitControl unitControl;
     bool selecting = false;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        unitControl = player.GetComponent<UnitControl>();
       unitFormations = player.GetComponent<UnitFormations>();
    }

    void OnGUI()
    {
        if (orgBoxPos != Vector2.zero && endBoxPos != Vector2.zero)
        {
            bounds = new Rect(orgBoxPos.x, Screen.height - orgBoxPos.y, endBoxPos.x - orgBoxPos.x, -1 * ((Screen.height - orgBoxPos.y) - (Screen.height - endBoxPos.y)));
            GUI.DrawTexture(bounds, selectTexture);
          
   
        }
    }

   

    void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
                orgBoxPos = Input.mousePosition;
            selecting = true;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            endBoxPos = Input.mousePosition;
            
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (selecting == true && bounds.size.x > 20)
            {
                foreach (Unit unit in unitControl.units)
                {
                    unit.selected = false;

                }

                selecting = false;
                for (int i = 0; i < unitControl.units.Count; i++)
                {

               

                    unitControl.units[i].SendMessage("BoxTest");
                    
                    


                }
            }
            unitFormations.GetFormation();
			orgBoxPos = Vector2.zero;
            endBoxPos = Vector2.zero;
        }
    }

}
