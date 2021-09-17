using DesignPatterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryCommand : Command
{
    private readonly NavMeshAgent _agent;
    public override bool IsFinished => _agent.remainingDistance <= 2f;

    GameObject _commandCenter;
    Unit _unit;

    public DeliveryCommand(Unit unit, GameObject commandCenter, NavMeshAgent agent)
    {
        _pos = commandCenter.transform.position;
        _unit = unit;
        _commandCenter = commandCenter;
        _agent = agent;
    }

    public override void Execute()
    {
        _unit.commands.Clear();
        _unit.commands.Enqueue(new MoveCommand(_commandCenter.transform.position, _agent));
        _unit.Collect(_unit.lastCrystal.transform.position, _unit.lastCrystal);
    }

    public override void OnFinished()
    {

    }
}
