using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreation : MonoBehaviour
{
    public static UnitCreation instance;

    public Material redMat;
    public Material blueMat;

    GridBase gridBase;
    UnitList unitList;
    TeamsManage teamMng;

	void Start ()
    {
        unitList = UnitList.GetInstance();
        teamMng = TeamsManage.GetInstance();
    }

    public void CreateUnit(string unitName, Vector2 pos, string team)
    {
        for (int i = 0; i < unitList.units.Count; i++)
        {
            if (unitList.units[i].unitName == unitName)
            {
                Node tempNode = gridBase.GetNodeFromVector2(pos);
                Vector3 tempPos = new Vector3(tempNode.x, tempNode.z);
                GameObject tempUnit = Instantiate(unitList.units[i].uModel, tempPos, Quaternion.identity) as GameObject;
                tempUnit.name = unitList.units[i].unitName;
                if (team == "Red")
                {
                    tempUnit.GetComponent<Renderer>().material = redMat;
                    teamMng.redTeam.Add(tempUnit);
                }
                else
                {
                    tempUnit.GetComponent<Renderer>().material = blueMat;
                    teamMng.blueTeam.Add(tempUnit);
                }
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
                Vector3 tempPos = new Vector3(tempNode.x, tempNode.z);
                GameObject tempUnit = Instantiate(unitList.units[i].uModel, tempPos, Quaternion.identity) as GameObject;
                tempUnit.name = unitList.units[i].unitName;
                if (team == "Red")
                {
                    tempUnit.GetComponent<Renderer>().material = redMat;
                    teamMng.redTeam.Add(tempUnit);
                }
                else
                {
                    tempUnit.GetComponent<Renderer>().material = blueMat;
                    teamMng.blueTeam.Add(tempUnit);
                }
            }
        }
    }
    public static UnitCreation GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
}