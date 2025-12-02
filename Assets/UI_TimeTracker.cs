using System;
using TMPro;
using UnityEngine;

public class UI_TimeTracker : MonoBehaviour
{
    private TextMeshProUGUI myText;
    public void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        Invoke("Subscribe", 0.1f);
    }

    public void Subscribe()
    {
        Probability.instance.dg_timeUpdated += UpdateTimeText;
    }

    public void UpdateTimeText()
    {
        string time = DateTime.UtcNow.ToLocalTime().ToString("h:mm:ss");
        time = "<color=white>" + time.Substring(0, time.Length - 1) + "</color><color=red>" + time[time.Length-1];
        myText.text = time;
    }
}
