using UnityEngine;

public class STE_Chase : AIState
{
    private Rigidbody2D rb;
    public float speed = 20f;
    public override void OnEnter()
    {
        llamo = "STE_Chase";
        rb = GetComponent<Rigidbody2D>();
        base.OnEnter();
       
    }
    public override void Process()
    {
        owner.triggeringContactDamage = true;

        rb.linearVelocity = (Player.instance.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }

    public override void OnExit()
    {
        owner.triggeringContactDamage = false;
        rb.linearVelocity = Vector3.zero;
        base.OnExit();
    }
}
