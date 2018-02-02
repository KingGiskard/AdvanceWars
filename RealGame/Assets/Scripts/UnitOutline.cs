using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitOutline
{
    public int unitID;
    public string unitName;

    public int uMoveRange;
    public int uAtkRangeMax;
    public int uAtkRangeMin;
    public int uAtkDmg;
    public int uHealth;

    public Object uModel;

    public UnitOutline(int id, string name, int mRange, int aRangeMax, int aRangeMin, int aDmg, int uHP, Object model)
    {
        unitID = id;
        unitName = name;
        uMoveRange = mRange;
        uAtkRangeMax = aRangeMax;
        uAtkRangeMin = aRangeMin;
        uAtkDmg = aDmg;
        uHealth = uHP;
        uModel = model;
    }
}