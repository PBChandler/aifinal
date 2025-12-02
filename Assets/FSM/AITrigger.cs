using UnityEngine;

public class AITrigger : MonoBehaviour
{
    public delegate void OnTriggerActivated(AITrigger t);
    public delegate void OnTriggerDeactivated(AITrigger t);

    public OnTriggerActivated dg_onTriggerActivated;
    public OnTriggerDeactivated dg_onTriggerDeactivated;
    public bool isBeingTriggered;

    public void Start()
    {
        dg_onTriggerActivated += Dummy;
        dg_onTriggerDeactivated += Dummy;
    }

    public void Dummy(AITrigger t)
    {

    }

}
