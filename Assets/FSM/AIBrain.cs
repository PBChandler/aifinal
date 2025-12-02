using NUnit.Framework;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;
public class AIBrain : MonoBehaviour
{
    public AIState CurrentState;
    public List<AITransition> transitions = new List<AITransition>();
    public float health;
    public delegate void hurt();
    public hurt dg_hurt;
    public bool triggeringContactDamage;
    public NavMeshAgent agent;
    public void Start()
    {
        Invoke("Setup", 0.1f); //give other Start Methods time to run.
        dg_hurt += Dummy;
    }
    static float agentDrift = 0.0001f; // minimal
    public void SetDestination(GameObject target)
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < agentDrift)
        {
            var driftPos = target.transform.position + new Vector3(agentDrift, 0f, 0f);
            agent.SetDestination(driftPos);
        }
       agent.SetDestination(target.transform.position);
    }
    public void Dummy() { }
    public enum DamageType
    {
        BOMB,
        LINE,
        HIT,
    }
    public void SufferDamage(float damage, DamageType type)
    {
        if (type == DamageType.BOMB)
        {
            if (CurrentState.ImmuneToExplosions)
                return;
        }
        if(type == DamageType.LINE)
        {
            if (CurrentState.ImmuneToLines)
                return;
        }    
        if(type == DamageType.HIT)
        {
            if (CurrentState.ImmuneToHits)
                return;
        }
        dg_hurt.Invoke();
        if (health - damage <= 0)
        {
            
            Die();
        }
        else
        {
            health -= damage;
        }
    }

    public void Die()
    {
        GameObject.Destroy(gameObject);
    }
    public void Setup()
    {
        CurrentState.owner = this;
        CurrentState.OnEnter();
        foreach (AITransition transition in transitions)
        {
            if (transition.OnDetriggerInstead)
            {
                transition.whenTriggered.dg_onTriggerDeactivated += CheckTransitionDetrigger;
            }
            else
            {
                transition.whenTriggered.dg_onTriggerActivated += CheckTransitionTrigger;
            }
        }
    }
    public void Update()
    {
        CurrentState.owner = this;
        CurrentState.Process();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject == Player.instance.gameObject && triggeringContactDamage)
        {
            Player.instance.InflictDamage(1);
        }
    }

    public void ChangeState(AIState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }

    public void CheckTransitionTrigger(AITrigger trig)
    {
        AIState to = null;
        int priorityToBeat = -999;
        foreach(AITransition t in transitions)
        {
            if(t.From == CurrentState && t.whenTriggered == trig && !t.OnDetriggerInstead)
            {
                if(t.priority > priorityToBeat)
                {
                    to = t.To;
                    priorityToBeat = t.priority;
                }
            }
        }
        if (to != null)
        {
            ChangeState(to);
        }
    }
    public void CheckTransitionDetrigger(AITrigger trig)
    {
        AIState to = null;
        int priorityToBeat = -999;
        foreach (AITransition t in transitions)
        {
            if (t.From == CurrentState && t.whenTriggered == trig && t.OnDetriggerInstead)
            {
                if (t.priority > priorityToBeat)
                {
                    to = t.To;
                    priorityToBeat = t.priority;
                }
            }
        }
        if (to != null)
        {
            ChangeState(to);
        }
    }
}
[System.Serializable]
public struct AITransition
{
    public AIState From, To;
    public AITrigger whenTriggered;
    public bool OnDetriggerInstead;
    public int priority;
}

