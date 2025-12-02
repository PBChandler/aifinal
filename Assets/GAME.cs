using UnityEngine;

public class GAME : MonoBehaviour
{
    public static GAME instance;

    public delegate void BroadCastMessage(string message, GameObject sender);
    public BroadCastMessage dg_Broadcast;

    public void Awake()
    {
        instance = this;
        dg_Broadcast += Dummy;
    }

    public void Broadcast(string m, GameObject g)
    {
        dg_Broadcast(m, g);
    }

    public void Dummy(string m, GameObject g)
    {

    }
}
