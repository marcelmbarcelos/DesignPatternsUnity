using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
    public abstract class Command
    {        
        public abstract void Execute();

        public abstract void OnFinished();

        public abstract bool IsFinished { get; }

        public Vector3 _pos;

    }
}
