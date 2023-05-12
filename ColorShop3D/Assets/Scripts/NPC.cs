using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] Hair, _Clothes;
    [HideInInspector]
    public GameObject Eyebrow, Head, Body, _ThoughtCloud, _Thought_HeadArt;
    [HideInInspector]
    public Color[] colors;
    private MasterStorage _masterStorage;
    [HideInInspector]
    public bool _is_Move_StartPos = false, _is_Move_Away = false, _is_Rotate = false;

    private void Awake()
    {
        _masterStorage = FindObjectOfType<MasterStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _is_Move_Away = false;
        //_is_Rotate = false;

        Hair_Disable();
        Set_Hair_Type();
        Set_Cloth_Color();

        StartCoroutine(_masterStorage.Delayed_Function_Call(Set_Rotation, 2f));
    }

    // Update is called once per frame
    void Update()
    {
        Check_Movement();
        Set_SmoothRotation();
    }

    private void Set_Hair_Type()
    {
        Hair[Random.Range(0, Hair.Length)].SetActive(true);
    }

    private void Set_Cloth_Color()
    {
        foreach(GameObject obj in _Clothes)
        {
            obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", colors[Random.Range(0, colors.Length)]);
        }
    }

    private void Check_Movement()
    {
        if (_is_Move_StartPos)
        {
            Move_StartPos();
        }
        else if (_is_Move_Away)
        {
            Move_Away();
        }
    }

    private void Move_StartPos()
    {
        //transform.position = Vector3.Lerp(transform.position, _masterStorage._NPC_Main_StandPos, Time.deltaTime * 1f);
        float x = transform.position.x;
        x -= Time.deltaTime * 1f;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void Move_Away()
    {
        float x = transform.position.x;
        x -= 1f * Time.deltaTime;

        float z = transform.position.z;
        z += Time.deltaTime * 0.05f;

        transform.position = new Vector3(x, transform.position.y, z);
    }

    public void Set_Rotation()
    {
        if (_is_Move_StartPos)
        {
            //transform.rotation = _masterStorage._NPC_Main_StandRot;
            _is_Move_StartPos = false;
            _is_Rotate = true;
            _masterStorage._NPC_Body.GetComponent<Animator>().SetTrigger("Idle");

            StartCoroutine(_masterStorage.Delayed_Function_Call(Stop_SmoothRotation, 1f));
        }
        else if (_is_Move_Away)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * -90f);
            _masterStorage._NPC_Body.GetComponent<Animator>().SetTrigger("Walk");

            StartCoroutine(_masterStorage.Delayed_Function_Call(This_Destroy, 4f));
        }
    }

    public void Set_SmoothRotation()
    {
        if (_is_Rotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _masterStorage._NPC_Main_StandRot, Time.deltaTime * 3f);
        }
    }

    public void Stop_SmoothRotation()
    {
        _is_Rotate = false;
        _masterStorage.NPC_ThoughtHead();
    }

    private void Hair_Disable()
    {
        foreach (GameObject obj in Hair)
        {
            obj.SetActive(false);
        }
    }

    public void This_Destroy()
    {
        Destroy(this.gameObject);
    }
}
