using DesignPatterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCommand : Command
{
    Unit _unit;
    public CollectCommand(Unit unit)
    {
        _pos = unit.transform.position;
        _unit = unit;
    }

    public override bool IsFinished => !_unit.busy;

    public override void Execute()
    {
        _unit.StartCollect();
    }

    public override void OnFinished()
    {

    }
}
