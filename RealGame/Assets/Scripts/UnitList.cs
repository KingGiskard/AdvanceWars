using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public List<UnitOutline> units = new List<UnitOutline>();

    public GameObject tankModel;
    public GameObject artilleryModel;

    public static UnitList instance;

	void Start ()
    {
        units.Add(new UnitOutline(1, "Tank",      8, 2, 0, 5, 10, tankModel));
        units.Add(new UnitOutline(2, "Artillery", 6, 5, 2, 5, 10, artilleryModel));
	}

    public static UnitList GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this; 
    }
}