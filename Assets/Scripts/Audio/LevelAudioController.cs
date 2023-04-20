using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class LevelAudioController : MonoBehaviour
{
    private FMOD.Studio.EventInstance _instance;

    private void Start()
    {
        _instance = FMODAudioManager.Instance.CreateEventInstance("event:/FmodEvents/MainEvent");
        FMODAudioManager.Instance.PlayEvent(_instance);
    }

    void Update()
    {
        // This part is for testing purposes only
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFmodParameter("level", "level1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetFmodParameter("level", "level2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFmodParameter("level", "level3");
        }
    }

    public void SetFmodParameter(string parameter, string label)
    {
        _instance.setParameterByNameWithLabel(parameter, label);
    }

    public void OnAllEnemiesDead()
    {
        SetFmodParameter("level", "level2");
        Debug.Log("All enemies are dead.");
    }
}
