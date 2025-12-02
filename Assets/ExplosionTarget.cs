using UnityEngine;

public class ExplosionTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetBombed()
    {
        try
        {
            GetComponent<AIBrain>().SufferDamage(5, AIBrain.DamageType.BOMB);
        }
        catch
        {

        }
        
    }
}
