using System.Collections;
using UnityEngine;

public class ShriekerVFXManager : MonoBehaviour
{
    public AIBrain brain;
    public Sprite closed, open, shrieking;
    private SpriteRenderer sr;
    public AudioClip scream;
    public AudioClip dead, hurt;
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
        AudioSource.PlayClipAtPoint(hurt, transform.position, 0.5f);
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
                sr.sprite = closed;
                break;
            case "STE_Looking":
                sr.sprite = open;
                break;
            case "STE_Chase":
                sr.sprite = shrieking;
                if(lastState != "STE_Chase")
                {
                    AudioSource.PlayClipAtPoint(scream, transform.position, 0.5f);
                    lastState = "STE_Chase";
                }
                
                break;
            default:
                Debug.Log(brain.CurrentState.llamo);
                break;
        }
    }

    public void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(dead, transform.position, 0.5f);
    }
}
