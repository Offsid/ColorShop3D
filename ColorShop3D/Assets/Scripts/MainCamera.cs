using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private MasterStorage _masterStorage;


    private void Awake()
    {
        _masterStorage = FindObjectOfType<MasterStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Set_FOV();
        _masterStorage.LookAt_NPC();
        _masterStorage.LookAt_Head();
    }

    private void Set_FOV()
    {
        if (_masterStorage._is_set_FOV)
        {
            _masterStorage.Camera_Set_FOV();

            /*if(_masterStorage._main_Camera.fieldOfView == _masterStorage._target_FOV)
            {
                Debug.Log("Fov = " + _masterStorage._main_Camera.fieldOfView);
                _masterStorage._is_set_FOV = false;
                this.enabled = false;
            }*/
        }
    }
}
