using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTest : MonoBehaviour {

    UnitCreation unitCreate;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            unitCreate = UnitCreation.GetInstance();
            unitCreate.CreateUnit("Tank", new Vector2(4, 7), "Red");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            unitCreate = UnitCreation.GetInstance();
            unitCreate.CreateUnit("Artillery", new Vector2(9, 2), "Red");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            unitCreate = UnitCreation.GetInstance();
            unitCreate.CreateUnit("Tank", new Vector2(1, 0), "Blue");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            unitCreate = UnitCreation.GetInstance();
            unitCreate.CreateUnit("Artillery", new Vector2(11, 9), "Blue");
        }
    }
}