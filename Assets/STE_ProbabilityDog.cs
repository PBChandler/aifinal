using UnityEngine;

public class STE_ProbabilityDog : AIState
{
    public float lastTime = 0;
    public override void OnEnter()
    {
        llamo = "STE_ProbabilityDog";
        owner.agent.isStopped = false;
        base.OnEnter();

    }
    public override void Process()
    {
        owner.SetDestination(Player.instance.gameObject);
        if (Time.time > lastTime)
        {
            if (Probability.instance.footsteps % 2 == 0)
            {
                if(Player.instance.HP < 3)
                {
                    Player.instance.HP++;
                    Player.instance.dg_onhurt.Invoke(1);
                }
            }
            else
            {
                //don't do anything :)
            }
            lastTime += 5f;
        }
        
    }

    public override void OnExit()
    {
        
    }
}
