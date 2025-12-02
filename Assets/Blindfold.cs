using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Blindfold : MonoBehaviour
{
    public static Blindfold instance;
    public Image blind;

    public void Start()
    {
        instance = this;
    }
    public void BlindScreen()
    {
        blind.color = Color.clear;
        StartCoroutine(throwBlind(0.5f));
    }

    public IEnumerator throwBlind(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            time += 0.01f;
            blind.color = Color.Lerp(blind.color, Color.black, time);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(catchBlind(duration));
    }

    public IEnumerator catchBlind(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += 0.01f;
            blind.color = Color.Lerp(blind.color, Color.clear, time);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
