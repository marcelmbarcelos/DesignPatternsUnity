using DesignPatterns.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    NavMeshAgent agent;
    public ParticleSystem collectParticle;
    public bool busy = false;
    public GameObject commandCenter;
    public GameObject lastCrystal;

    public GameObject handcrystal;

    public Queue<Command> commands = new Queue<Command>();
    public Command currentCommand = null;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        ProcessCommands();    
    }

    public void ProcessCommands()
    {
        if (currentCommand != null && !currentCommand.IsFinished)
            return;

        if (commands.Count < 1)
            return;

        if (currentCommand != null && currentCommand.IsFinished)
            currentCommand.OnFinished();

        currentCommand = commands.Dequeue();
        currentCommand.Execute();
    }

    public void Move(Vector3 point)
    {
        agent.ResetPath();
        commands.Clear();
        commands.Enqueue(new MoveCommand(point, agent));
    }

    public void AddMove(Vector3 point)
    {        
        commands.Enqueue(new MoveCommand(point, agent));
    }

    public void Collect(Vector3 point, GameObject crystal)
    {
        lastCrystal = crystal;

        commands.Enqueue(new MoveCommand(point, agent));
        commands.Enqueue(new CollectCommand(this));
        commands.Enqueue(new DeliveryCommand(this,commandCenter, agent));
    }

    public void StartCollect()
    {
        StartCoroutine(Collecting());
    }

    IEnumerator Collecting()
    {
        busy = true;
        collectParticle.Play();
        yield return new WaitForSeconds(1);
        collectParticle.Play();
        yield return new WaitForSeconds(1);
        collectParticle.Play();
        yield return new WaitForSeconds(1);
        busy = false;
    }
}






