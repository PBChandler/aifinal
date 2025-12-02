using UnityEngine;

public class Trig_Ears : AITrigger
{
    public string TriggeringMessage;
    public float distance = 999;
    public bool cooldown;

    public void Awake()
    {
        Invoke("setup", 0.1f); //give other awakes and starts time to happen
    }

    private void setup()
    {
        GAME.instance.dg_Broadcast += ProcessBroadcast;
    }

    public void ProcessBroadcast(string message, GameObject sender)
    {
        if (cooldown)
            return;
        cooldown = true;
        if(message == TriggeringMessage)
        {
            if (Vector3.Distance(gameObject.transform.position, sender.transform.position) < distance)
            {
                isBeingTriggered = true;
                dg_onTriggerActivated.Invoke(this);
            }
        }
        Invoke("resetcooldown", 0.1f);
    }

    public void resetcooldown()
    {
        cooldown = false;
    }
    public void OnDisable()
    {
        GAME.instance.dg_Broadcast -= ProcessBroadcast;
    }

    public void OnDestroy()
    {
        GAME.instance.dg_Broadcast -= ProcessBroadcast;
    }
}
