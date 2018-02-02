using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreation : MonoBehaviour
{
    GridBase gridBase;
    UnitList unitList;

	void Start ()
    {
        gridBase = GridBase.GetInstance();
        unitList = UnitList.GetInstance();
    }

    public void CreateUnit(string unitName, Vector2 pos, string team)
    {
        for (int i = 0; i < unitList.units.Count; i++)
        {
            if (unitList.units[i].unitName == unitName)
            {
                Node tempNode = gridBase.GetNodeFromVector2(pos);
                Vector3 tempPos = new Vector3(tempNode.x, tempNode.y, tempNode.z);
                GameObject tempUnit = Instantiate(unitList.units[i].uModel, tempPos, Quaternion.identity) as GameObject;
                tempUnit.name = unitList.units[i].unitName;
                tempUnit.GetComponent<UnitBehavior>().team = team;
            }
        }
    }

    public void CreateUnit(int unitID, Vector2 pos, string team)
    {
        for (int i = 0; i < unitList.units.Count; i++)
        {
            if (unitList.units[i].unitID == unitID)
            {
                Node tempNode = gridBase.GetNodeFromVector2(pos);
                Vector3 tempPos = new Vector3(tempNode.x, tempNode.y, tempNode.z);
                GameObject tempUnit = Instantiate(unitList.units[i].uModel, tempPos, Quaternion.identity) as GameObject;
                tempUnit.name = unitList.units[i].unitName;
                tempUnit.GetComponent<UnitBehavior>().team = team;
            }
        }
    }
}