using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlaceholderSceneSwitch : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchObject(object1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchObject(object2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchObject(object3);
        }

    }
    private void SwitchObject(GameObject targetObject)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);

        targetObject.SetActive(true);
    }
}
