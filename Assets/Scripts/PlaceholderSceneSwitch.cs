using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlaceholderSceneSwitch : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;


    [FMODUnity.ParamRef]
    public string song1EventPath;
    [FMODUnity.ParamRef]
    public string song2EventPath;
    [FMODUnity.ParamRef]
    public string song3EventPath;

    private FMOD.Studio.EventInstance currentSong;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchObjectAndSong(object1, song1EventPath);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchObjectAndSong(object2, song2EventPath);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchObjectAndSong(object3, song3EventPath);
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    object1.SetActive(true);
        //    object2.SetActive(false);
        //    object3.SetActive(false);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    object1.SetActive(false);
        //    object2.SetActive(true);
        //    object3.SetActive(false);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    object1.SetActive(false);
        //    object2.SetActive(false);
        //    object3.SetActive(true);
        //}
    }
    private void SwitchObjectAndSong(GameObject targetObject, string targetSongEventPath)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);

        targetObject.SetActive(true);

        //currentSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //currentSong.release();

        //currentSong = FMODUnity.RuntimeManager.CreateInstance(targetSongEventPath);
        //currentSong.start();
    }
}
