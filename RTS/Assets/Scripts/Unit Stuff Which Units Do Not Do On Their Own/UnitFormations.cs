using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitFormations : MonoBehaviour
{
    public UnitControl unitControl;
    public List<Vector2> grid;
    Grid gridMap;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject pathfinder = GameObject.FindGameObjectWithTag("PathFinder");
        unitControl = player.GetComponent<UnitControl>();
        gridMap = pathfinder.GetComponent<Grid>();
    }

    public void GetFormation()
    {
        unitControl.SendMessage("UpdateActive");

        int length = Mathf.CeilToInt(Mathf.Sqrt(unitControl.unitsActive.Count));

        grid = new List<Vector2>();
        
        int i = 0;
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < length; y++)
            {
                
                grid.Add(new Vector2(2*x, 2*y));
				if(i <= unitControl.unitsActive.Count - 1){

					unitControl.unitsActive[i].targetOffset = grid[i];

				}
                i++;
            }
        }
       
    }
}