using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Vector2 launchDirection = Vector2.one;
    public float speed = 5;
    public float lifeTime = 0;
    private float curTime = 0;
    Rigidbody2D rb;
    public GameObject explosion;
    [SerializeField] public SpriteRenderer myRend;
    public Player owner;
    float stoppingY;
    public float dividend;
    public AudioClip explosionSound;
    public AudioSource src;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        src.pitch = DeterminePitch();
        src.Play();
    }

    public float DeterminePitch()
    {
        float wow = 0;
        wow = launchDirection.y + 1f;
        return wow;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (curTime < lifeTime)
        {
            rb.AddForce(speed * launchDirection * Time.deltaTime, ForceMode2D.Impulse);
            curTime += Time.deltaTime * 2;

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            myRend.transform.GetComponent<TrailRenderer>().enabled = false;
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            Explode();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        curTime = lifeTime;
    }

    public void OnDestroy()
    {
        owner.spawnedBombs.Remove(this.gameObject);
    }
    public void Explode()
    {
        explosion.SetActive(true);
        myRend.color = Color.clear;
        src.clip = explosionSound;
        src.Play();
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D col in hit)
        {
            if(col.GetComponent<ExplosionTarget>() != null)
            {
                col.GetComponent<ExplosionTarget>().GetBombed();
            }
        }
        Invoke("Bombs", 0.5f);
    }

    public void Bombs()
    {
        Destroy(this.gameObject);
    }
}
