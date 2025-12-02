using System.Collections;
using UnityEngine;
using static AIBrain;
using UnityEngine.UI;

public class PlayerGFXHandler : MonoBehaviour
{
    public Player owner;
    public SpriteRenderer sr;
    public AudioClip hurt;
    public Image[] hearts;
    void Start()
    {
        owner.dg_onhurt += Flash;
    }

    public void Flash()
    {
        //there are much better ways to do this but they were all fighting me so it's this for now.
        if(owner.HP > 2)
            hearts[2].color = Color.gray;
        else if (owner.HP > 1)
            hearts[1].color = Color.gray;
        else hearts[0].color = Color.gray;
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
}
