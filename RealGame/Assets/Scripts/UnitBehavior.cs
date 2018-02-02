﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    Pathfinder path;
    UnitList units;
    UnitOutline unit;
    GridBase grid;

    public float moveSpd;

    public string team;

    private int localHP;

    private List<Node> p;

    private Vector3 curPos = Vector3.zero;
    private float moveAmount = 0;

	void Start ()
    {
        path = new Pathfinder();
        units = UnitList.GetInstance();
        grid = GridBase.GetInstance();

        for (int i = 0; i < units.units.Count; i++)
        {
            if (units.units[i].unitName == gameObject.name)
            {
                unit = units.units[i];
            }
        }

        localHP = unit.uHealth;
	}

    private void Update()
    {
        if (p != null && p.Count > 0)

        {
            if (curPos == Vector3.zero)
            {
                curPos = gameObject.transform.position;
            }
            if (moveAmount == 0)
            {
                gameObject.transform.LookAt(new Vector3(p[0].x, transform.position.y, p[0].z));
                moveAmount = Vector3.Distance(curPos, new Vector3(p[0].x, transform.position.y, p[0].z)) * Time.deltaTime * moveSpd;
            }
            if (Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(p[0].x, p[0].z)) > 0.1f)
            {
                gameObject.transform.Translate(Vector3.forward * moveAmount);
            }
            else
            {
                curPos = Vector3.zero;
                moveAmount = 0;
                p.RemoveAt(0);
            }
        }
    }

    public void TryMove(Node start, Node end)
    {
        path.startPos = start;
        path.endPos = end;

        if (path.FindPath().Count <= unit.uMoveRange)
        {
            GetPath(start, end);
        }
        else
        {
            print("Too Far! Max Range: " + unit.uMoveRange + " You Tried: " + path.FindPath().Count);
        }
    }

    public void TryAttack(GameObject otherUnit)
    {
        path.startPos = grid.GetNodeFromVector3(gameObject.transform.position);
        path.endPos = grid.GetNodeFromVector3(otherUnit.transform.position);

        if (path.FindPath().Count >= unit.uAtkRangeMin && path.FindPath().Count <= unit.uAtkRangeMax)
        {
            gameObject.transform.LookAt(otherUnit.transform.position);
            otherUnit.GetComponent<UnitBehavior>().TakeDamage(unit.uAtkDmg);
            print(unit.unitName + " Did " + unit.uAtkDmg + " Damage To " + otherUnit.name);
        }
        else
        {
            print("Not In Range! Range Is " + unit.uAtkRangeMin + " To " + unit.uAtkRangeMax);
        }
    }

    public void TakeDamage(int dmg)
    {
        localHP -= dmg;
        if (localHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

	public void GetPath(Node start, Node end)
    {
        path.startPos = start;
        path.endPos = end;

        p = path.FindPath();
    }
}
