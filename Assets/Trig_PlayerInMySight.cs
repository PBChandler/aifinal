using UnityEngine;

public class Trig_PlayerInMySight : AITrigger
{
    public Vector2 direction;
    public float distance;
    public LayerMask mask;
    public void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, direction, distance, mask);
        Debug.DrawLine(transform.position, transform.position - new Vector3(direction.x,direction.y));
        if (hit.collider == null)
            return;
        if(hit.collider.tag == "Player")
        {
            dg_onTriggerActivated.Invoke(this);
            Debug.Log("hit");
            isBeingTriggered = true;
        }
        else
        {
            if (isBeingTriggered)
            {
                dg_onTriggerDeactivated(this);
            }
            isBeingTriggered = false;
        }
    }
}
