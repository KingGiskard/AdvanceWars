using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreation : MonoBehaviour
{
    public int redUnits;
    public int blueUnits;

    public GameObject redTank;
    public GameObject blueTank;

    GridBase gridBase;

	void Start ()
    {
        gridBase = GridBase.GetInstance();
        for (int i = 0; i < redUnits; i++)
        {
            int tempX = Random.Range(0, gridBase.xMax);
            int tempY = Random.Range(0, gridBase.yMax);
            Instantiate(redTank, new Vector3(tempX, 0.0f, tempY), Quaternion.identity);
        }
        for (int i = 0; i < blueUnits; i++)
        {
            int tempX = Random.Range(0, gridBase.xMax);
            int tempY = Random.Range(0, gridBase.yMax);
            Instantiate(blueTank, new Vector3(tempX, 0.0f, tempY), Quaternion.identity);
        }
    }
}