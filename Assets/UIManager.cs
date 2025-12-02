using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<MaskableGraphic> anyUI;
    bool flip;
    public void FlipFlopUI()
    {
        flip = !flip;
        foreach (MaskableGraphic g in anyUI)
        {
            g.color = flip ? Color.clear : Color.white;
        }
    }

    public void Start()
    {
        FlipFlopUI();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            FlipFlopUI();
        }
    }
}
