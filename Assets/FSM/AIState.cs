using System;
using UnityEngine;

public class AIState : MonoBehaviour
{
    
   public string llamo = "STE_null";
    public AIBrain owner;
    public bool ImmuneToHits;
    public bool ImmuneToLines;
    public bool ImmuneToExplosions;
    public void Initialize(AIBrain aOwner)
    {
        owner = aOwner;
    }
    public virtual void OnEnter()
    {

    }
    public virtual void Process()
    {

    }

    public virtual void OnExit()
    {
        
    }
}
