using UnityEngine;

public class BoundsSwapper : MonoBehaviour
{
    public Collider2D flip, flop;
    public bool flipcheck;
    private bool cooldown = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (cooldown)
            return;
        Debug.Log("yum");
        if(collision.CompareTag("Player"))
        {
            flipcheck = !flipcheck;
            VCam.instance.Swap(flipcheck ? flip : flop);
            cooldown = true;
            Invoke("setup", 0.1f);
        }
    }

    public void setup()
    {
        cooldown = false;
    }
}
