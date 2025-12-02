using UnityEngine;

public class Trig_PlayerFacingMe : AITrigger
{
    public float range;
    private bool leftMode = false;
    public void Update()
    {
        if(Vector3.Distance(Player.instance.transform.position, transform.position) < range)
        {
            //if player is facing to the left and I'm left of them
            if(!Player.instance.spr.flipX && transform.position.x <= Player.instance.transform.position.x)
            {
                dg_onTriggerActivated.Invoke(this);
                leftMode = true;
                isBeingTriggered = true;
            }
            //if player is right
            if(Player.instance.spr.flipX && transform.position.x >= Player.instance.transform.position.x)
            {
                dg_onTriggerActivated.Invoke(this);
                isBeingTriggered = true;
                leftMode = false;
            }
        }

        if (isBeingTriggered)
        {
            if (leftMode)
            {
                if (!(!Player.instance.spr.flipX && transform.position.x <= Player.instance.transform.position.x))
                {
                    isBeingTriggered = false;
                    dg_onTriggerDeactivated.Invoke(this);
                }
            }
            else
            {
                if (!(Player.instance.spr.flipX && transform.position.x >= Player.instance.transform.position.x))
                {
                    isBeingTriggered = false;
                    dg_onTriggerDeactivated.Invoke(this);
                }
            }
        }
    }
}
