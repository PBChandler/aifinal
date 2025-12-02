using System.Collections;
using UnityEngine;

public class FridgeGFXManager : MonoBehaviour
{
    public AIBrain brain;
    public Sprite idle, chasing;
    private SpriteRenderer sr;
    public AudioClip open;
    public AudioClip close, dead;
    public AudioSource src;
    private string lastState = "";
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        brain.dg_hurt += Flash;
    }
    public void Flash()
    {
        sr.color = Color.red;
        StartCoroutine(resetToColor());
    }

    public IEnumerator resetToColor()
    {
        while (sr.color.g < 0.76f)
        {
            sr.color = Color.Lerp(sr.color, Color.white, 2 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        sr.color = Color.white;
    }
    public void Update()
    {
        switch (brain.CurrentState.llamo)
        {
            case "STE_DoNothing":
                sr.sprite = idle;
                if(lastState != brain.CurrentState.llamo)
                {
                    src.clip = close;
                    src.Play();
                    lastState = brain.CurrentState.llamo;
                }
            break;
            case "STE_Chase":
                sr.flipX = Player.instance.transform.position.x < transform.position.x ? true : false;
                sr.sprite = chasing;
                if (lastState != brain.CurrentState.llamo)
                {
                    src.clip = open;
                    src.Play();
                    lastState = brain.CurrentState.llamo;
                }
                break;

            default:
                Debug.Log(brain.CurrentState.name);
                break;
        }
    }

    public void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(dead, transform.position);
    }
}
