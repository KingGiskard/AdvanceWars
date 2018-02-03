using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFrame : MonoBehaviour
{

    TeamsManage teams;

    private string curTeam;

	void Start ()
    {
        teams = TeamsManage.GetInstance();

        curTeam = "Blue";
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchTeams();
        }
	}

    private void SwitchTeams()
    {
        if (curTeam == "Blue")
        {
            for (int i = 0; i < teams.blueTeam.Count; i++)
            {
                teams.blueTeam[i].GetComponent<UnitBehavior>().movesLeft = 0;
                teams.blueTeam[i].GetComponent<UnitBehavior>().atksLeft = 0;
            }
            for (int i = 0; i < teams.redTeam.Count; i++)
            {
                teams.redTeam[i].GetComponent<UnitBehavior>().movesLeft = 1;
                teams.redTeam[i].GetComponent<UnitBehavior>().atksLeft = 1;
            }
            curTeam = "Red";
        }
        else if (curTeam == "Red")
        {           
            for (int i = 0; i < teams.redTeam.Count; i++)
            {
                teams.redTeam[i].GetComponent<UnitBehavior>().movesLeft = 0;
                teams.redTeam[i].GetComponent<UnitBehavior>().atksLeft = 0;
            }
            for (int i = 0; i < teams.blueTeam.Count; i++)
            {
                teams.blueTeam[i].GetComponent<UnitBehavior>().movesLeft = 1;
                teams.blueTeam[i].GetComponent<UnitBehavior>().atksLeft = 1;
            }
            curTeam = "Blue";
        }
    }
}
