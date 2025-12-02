using Unity.Cinemachine;
using UnityEngine;

public class VCam : MonoBehaviour
{
    public static VCam instance;
    public CinemachineConfiner2D confiner;
    public void Start()
    {
        instance = this;
    }

    public void Swap(Collider2D c)
    {
        Debug.Log("yuck");
        confiner.BoundingShape2D = c;
    }
    
}
