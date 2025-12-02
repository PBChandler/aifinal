using UnityEngine;

public class BoundsSwapper : MonoBehaviour
{
    public Collider2D flip, flop;
    public bool flipcheck;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("yum");
        if(collision.CompareTag("Player"))
        {
            flipcheck = !flipcheck;
            VCam.instance.Swap(flipcheck ? flip : flop);
        }
    }

    public void setup()
    {
        
    }
}
