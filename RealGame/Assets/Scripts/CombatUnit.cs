using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class CombatUnit : Unit
{
    private int move_range;
    public int Move_Range
    { get; set; }
    private int atk_range_max;
    public int Atk_Range_Max
    { get; set; }
    private int atk_range_min;
    public int Atk_Range_Min
    { get; set; }
    private int atk_dmg;
    public int Atk_Dmg
    { get; }

    public CombatUnit(int _id, string _name, int _move_range, int _atk_range_max, int _atk_range_min, int _atk_dmg, int _health, int _defense, string _unit_type) : base(_id, _name, _health, _defense, _unit_type)
    {

        move_range = _move_range;
        atk_range_max = _atk_range_max;
        atk_range_min = _atk_range_min;
        atk_dmg = _atk_dmg;
    }

    /// <summary>
    /// method assumes that the attack is in the attack range of the enemy.
    /// Return an array of booleans. First boolean is whether the attack killed the enemy. Second boolean is whether the attack died.
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public bool[] attack(CombatUnit enemy)
    {
        if (enemy.takeDamage(atk_dmg))
            return new bool[] { true, false };
        else
            return new bool[] { false, this.takeDamage(enemy.atk_dmg) };

    }

    public void move(Node[] path)
    {
        //move this object along the path
    }





}

