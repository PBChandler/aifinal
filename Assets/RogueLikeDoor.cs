using UnityEngine;

public class RogueLikeDoor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip clip;
    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Blindfold.instance.BlindScreen();
            Invoke("spawnroom", 0.5f);
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
            
    }

    public void spawnroom()
    {
        Probability.instance.SpawnNextRoom();
    }
}
