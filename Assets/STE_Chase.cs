using UnityEngine;

public class STE_Chase : AIState
{
    private Rigidbody2D rb;
    public float speed = 20f;
    public override void OnEnter()
    {
        llamo = "STE_Chase";
        rb = GetComponent<Rigidbody2D>();
        owner.agent.isStopped = false;
        base.OnEnter();
       
    }
    public override void Process()
    {
        owner.triggeringContactDamage = true;

        owner.SetDestination(Player.instance.gameObject);
    }

    public override void OnExit()
    {
        owner.triggeringContactDamage = false;
        rb.linearVelocity = Vector3.zero;
        owner.agent.isStopped = true;
        base.OnExit();
    }
}
