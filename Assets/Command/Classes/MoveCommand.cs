using DesignPatterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private readonly NavMeshAgent _agent;

    public MoveCommand(Vector3 pos, NavMeshAgent agent)
    {
        _pos = pos;
        _agent = agent;
    }

    public override bool IsFinished => _agent.remainingDistance <= 0.1f;

    public override void Execute()
    {
        Debug.Log("Move Command " + _pos);
        _agent.ResetPath();
        _agent.SetDestination(_pos);

    }

    public override void OnFinished()
    {

    }
}
