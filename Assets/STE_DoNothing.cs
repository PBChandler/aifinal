using UnityEngine;

public class STE_DoNothing : AIState
{
    
    public override void OnEnter()
    {
        llamo = "STE_DoNothing";
        base.OnEnter();
    }
    public override void Process()
    {
        base.Process();
    }
}
