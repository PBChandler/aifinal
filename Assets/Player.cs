using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player instance;
    Rigidbody2D rb;
    public Vector2 input;
    public float speed;
    public SpriteRenderer spr;
    public bool dashCooldown;
    public Vector2 last;
    public Animator anim;
    public Sprite openMouth, closedMouth;
    public GameObject projectilePrefab;
    public LineRenderer bars;

    public bool immunityFrames = false;
    public List<AudioClip> steps;

    public AudioClip dashSound;
    public delegate void FootStep();
    public FootStep dg_step;

    public List<GameObject> spawnedBombs;
    public EdgeCollider2D barEdgeCollider;
    private bool attackCooldown;
    public int HP = 3;

    public delegate void onhurt(float direction);
    public onhurt dg_onhurt;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bars.transform.SetParent(null);
        instance = this;
        dg_onhurt += dummy;
    }

    public void dummy(float f)
    {

    }

    public void InflictDamage(int damage)
    {
        if (immunityFrames)
            return;
       
        if (!immunityFrames)
        {
            immunityFrames = true;
            dg_onhurt.Invoke(-1);
            if (HP - damage <= 0)
            {
                Die();
            }
            else
            {
                HP -= damage;
            }
            Invoke("stopimmunity", 1f);
        }
            
        
    }

    public void stopimmunity()
    {
        immunityFrames = false;
    }

    public void Die() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       
        if (!dashCooldown)
        {
            if (input.x != 0 && !attackCooldown)
            {
                last.x = input.x;
                last = new Vector2(input.x, 0);
                spr.flipX = input.x < 0 ? false : true;
            }
            if (input.y != 0 && !attackCooldown)
            {
                last.y = input.y;
                spr.flipX = input.x < 0 ? false : true;
                last = new Vector2(0, input.y);
            }
            
        }


        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        anim.SetFloat("Speed", rb.linearVelocity.magnitude);

        if(Input.GetMouseButtonDown(0) && !attackCooldown)
        {
            SpawnBomb();
            
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            bars.positionCount = 0;
            barEdgeCollider.points = new Vector2[0];
       
        }
        bars.positionCount = spawnedBombs.Count;
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < spawnedBombs.Count; i++)
        {
            points.Add(spawnedBombs[i].gameObject.transform.position);
          
        }

        List<Vector3> threePoints = new List<Vector3>();
        foreach(Vector3 poi in points)
        {
            threePoints.Add(poi);
        }
        bars.SetPositions(threePoints.ToArray());
        barEdgeCollider.points = points.ToArray();
    }

    public void ResetMouth()
    {
        spr.sprite = closedMouth;
        attackCooldown = false;
    }

    public void SpawnBomb()
    {
        if (spawnedBombs.Count >= 4)
        {
            spawnedBombs.First().GetComponent<PlayerProjectile>().Explode();
            spawnedBombs.Remove(spawnedBombs.First());
        }
        spr.sprite = openMouth;
        attackCooldown = true;
        Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Invoke("ResetMouth", 0.1f);
        GameObject g = Instantiate(projectilePrefab, transform.position + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 3, transform.rotation);
        g.GetComponent<PlayerProjectile>().launchDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        g.GetComponent<PlayerProjectile>().owner = this;
        spr.flipX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x ? false : true;
        spawnedBombs.Add(g);
    }


   
    public void PlayFootStepSound()
    {
        float rand = steps.Count * Probability.GetSeededValue();
        //probability made a very annoying sound experience ngl
        AudioSource.PlayClipAtPoint(steps[Random.Range(0, steps.Count)], transform.position, Random.Range(0.02f, 0.04f) * (Probability.GetTimeSeededValue()+1));
        dg_step.Invoke();
    }
    public void Dash()
    {
        if (dashCooldown)
            return;

        anim.Play("dash");
        AudioSource.PlayClipAtPoint(dashSound, transform.position, Random.Range(0.7f, 1.1f));
        rb.linearVelocity += last * 25;
        dashCooldown = true;
        Invoke("EndCoolDown", 0.1f);
    }

    public void EndCoolDown()
    {
        dashCooldown = false;
    }

    public void FixedUpdate()
    {
        if(!dashCooldown)
        {
            rb.linearVelocity = input * speed;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(dashCooldown)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
