using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class BuildingUnit : Unit
{
    private int radius;
    public int Radius
    { get; }
    private int income;
    public int Income
    { get; }
    public BuildingUnit(int _id, string _name, int _health, int _defense, int _radius, int _income, string _unit_type) : base(_id, _name, _health, _defense, _unit_type)
    {
        radius = _radius;
        income = _income;
    }

    public CombatUnit build_unit(int _id, string _name, int _move_range, int _atk_range_max, int _atk_range_min, int _atk_dmg, int _health, int _defense, string _unit_type)
    {
        return new CombatUnit(_id, _name, _move_range, _atk_range_max, _atk_range_min, _atk_dmg, _health, _defense, _unit_type);
    }

}

