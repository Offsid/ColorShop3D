using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWheel : MonoBehaviour
{
    [SerializeField]
    Transform wheel;

    Vector3 startPos, endPos;
    float start_angle, end_angle, result_angle, z_angle;
    bool is_Initialized = false, is_Rotate = false;


    public void RotateWheel()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            Vector3 dir = startPos - wheel.transform.position;
            start_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Debug.Log("Start Angle Recorded");
        }
        if(Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            Vector3 dir = endPos - wheel.transform.position;
            end_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            result_angle = end_angle - start_angle;

            wheel.transform.eulerAngles = Vector3.forward * (result_angle);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Button Lifted");
        }*/

        if (!is_Initialized)
        {
            startPos = Input.mousePosition;
            Vector3 dir = startPos - wheel.transform.position;
            start_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            z_angle = wheel.transform.eulerAngles.z;
            //Debug.Log("Start Angle Recorded");

            is_Initialized = true;
            is_Rotate = true;
        }

        if (is_Rotate)
        {
            endPos = Input.mousePosition;
            Vector3 dir = endPos - wheel.transform.position;
            end_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            result_angle = end_angle - start_angle;

            wheel.transform.eulerAngles = Vector3.forward * (result_angle + z_angle);
        }
    }

    public void ResetValues()
    {
        //Debug.Log("Result angle = " + result_angle);
        is_Initialized = false;
        is_Rotate = false;
    }
}
