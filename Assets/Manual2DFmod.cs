using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class Manual2DFmod : MonoBehaviour
{
    private FMOD.Studio.EventInstance _instance;


    private void Start()
    {
        _instance = FMODUnity.RuntimeManager.CreateInstance("event:/FmodEvents/MainEvent");
        _instance.start();
    }
    void Update()
    {
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

    private void SetFmodParameter(string parameter, string label)
    {
        _instance.setParameterByNameWithLabel(parameter, label);
    }
}
