using UnityEngine;

public class SFXLaserGrid : MonoBehaviour
{
    private AudioSource src;
    public Player player;
    void Start()
    {
        src = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        src.volume = ((float)player.spawnedBombs.Count / 4)*0.5f;
    }
}
