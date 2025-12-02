using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Probability : MonoBehaviour
{
    public int footsteps;
    public int lastNumberOfSeconds;

    public static Probability instance;
    public delegate void timeUpdated();
    public timeUpdated dg_timeUpdated;
    public Player player;
    GameObject currentRoom;
    public List<GameObject> roomPrefabs;
    public bool inProbabilityTest = false;
    private static float timeSinceLastShot = 0;
    public static float pitch
    {
        get
        {
            return DeterminePitch();
        }
    }
    private static float _pitch;

    public static float DeterminePitch()
    {
        _pitch = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized.y;
        return _pitch;
    }
    public void Start()
    {
        instance = this;
        player.dg_step += IncrementFootstep;
        dg_timeUpdated += Dummy;
        if (inProbabilityTest)
        {
            SpawnNextRoom();
        }
    }

    public void SpawnNextRoom()
    {
        try
        {
            GameObject.Destroy(currentRoom);
        }
        catch
        {
            //no room exists yet;
        }
        player.transform.position = Vector3.zero;
        currentRoom = GameObject.Instantiate(roomPrefabs[lastNumberOfSeconds]);
        
    }
    private void Dummy()
    {

    }
    public void FixedUpdate()
    {
        UpdateTime();
    }

    public void UpdateTime()
    {
        string seconds = DateTime.UtcNow.ToLocalTime().Second.ToString("D2");
        lastNumberOfSeconds = int.Parse(seconds[1]+"");
        dg_timeUpdated.Invoke();
    }
    private void IncrementFootstep()
    {
        if (footsteps < 99)
            footsteps++;
        else
            footsteps = 0;
    }
    public static float GetSeededValue()
    {
        Debug.Log((float)instance.footsteps / 100f);
        return (float)instance.footsteps / 100f;
    }

    public static float GetTimeSeededValue()
    {
        return instance.lastNumberOfSeconds;
    }
}
