using UnityEngine;

public class Trig_Timere : AITrigger
{
    public float time;
    private float currentTime;
    public void Update()
    {
        if (currentTime < time)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            dg_onTriggerActivated.Invoke(this);
            Invoke("resset", 1f);
        }

        
    }

    public void resset()
    {
        currentTime = 0;
        dg_onTriggerDeactivated.Invoke(this);
    }
}
