using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit {

    public int id
    {get;}

    public string name
    { get; }

    public int health
    { get; set; }

    public int defense
    { get; set; }

    public string unit_type
    { get; }

    public Unit(int _id, string _name, int _health, int _defense, string _unit_type)
    {
        id = _id;
        name = _name;
        health = _health;
        defense = _defense;
        unit_type = _unit_type;

    }

    /// <summary>
    /// causes the unit to take damages according to the units health and defense stat.
    /// Returns true if the unit was destroyed. 
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool takeDamage(int damage)
    {
        health = health - (damage - this.defense);
        return health <= 0;
    }

}
