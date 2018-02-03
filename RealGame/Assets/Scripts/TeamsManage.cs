using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsManage : MonoBehaviour
{

    public static TeamsManage instance;

    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;

    void Start()
    {
        redTeam = new List<GameObject>();
        blueTeam = new List<GameObject>();
    }

    public static TeamsManage GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
}
