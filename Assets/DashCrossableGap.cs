using UnityEngine;

public class DashCrossableGap : MonoBehaviour
{
    public Collider2D component;
    
    public void Update()
    {
        component.isTrigger = Player.instance.dashCooldown;
    }
}
