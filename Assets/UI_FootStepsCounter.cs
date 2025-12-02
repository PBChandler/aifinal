using TMPro;
using UnityEngine;

public class UI_FootStepsCounter : MonoBehaviour
{
    public Player p;
    public TextMeshProUGUI text;
    public void Start()
    {
        p.dg_step += Display;
    }
    public void Display()
    {
        text.text = Probability.instance.footsteps.ToString("D2");
    }
}
