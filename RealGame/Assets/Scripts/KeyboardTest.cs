using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTest : MonoBehaviour {

    UnitCreation unitCreate;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            unitCreate = this.gameObject.GetComponent<UnitCreation>();
            unitCreate.CreateUnit("Tank", new Vector2(4, 7), "Red");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            unitCreate = this.gameObject.GetComponent<UnitCreation>();
            unitCreate.CreateUnit(2, new Vector2(9, 2), "Blue");
        }
    }
}