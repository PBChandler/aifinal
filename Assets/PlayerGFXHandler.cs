using System.Collections;
using UnityEngine;
using static AIBrain;
using UnityEngine.UI;
using System.Linq;

public class PlayerGFXHandler : MonoBehaviour
{
    public Player owner;
    public SpriteRenderer sr;
    public AudioClip hurt;
    public Image[] hearts;
    void Start()
    {
        hearts.Reverse();
        owner.dg_onhurt += Flash;
    }

    public void Flash(float high)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (owner.HP > i+1 || owner.HP == 3) hearts[i].color = Color.white;
            else hearts[i].color = Color.gray;
        }
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
