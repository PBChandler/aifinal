using UnityEngine;

public class Trig_OnHurt : AITrigger
{
    public AIBrain brain;
    void Awake()
    {
        brain.dg_hurt += trigger;
    }

    public void trigger()
    {
        dg_onTriggerActivated.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
