using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    #region Distinguishing Button
    [HideInInspector]
    public bool is_MaskButton = false;
    [HideInInspector]
    public bool is_ColorButton = false;
    #endregion

    #region Gameobject Reference
    private GameObject Mask_Object;
    #endregion

    #region Script References
    private MasterStorage master_Storage;
    private ColorMain colormain;
    private GameManager _gameManager;
    #endregion

    #region Heterogeneous Values
    [HideInInspector]
    public string _maskTag = "";
    private Button button;
    [HideInInspector]
    public Color _color;
    #endregion


    private void Awake()
    {
        master_Storage = FindObjectOfType<MasterStorage>();
        _gameManager = FindObjectOfType<GameManager>();
        colormain = FindObjectOfType<ColorMain>();
    }

    private void Start()
    {
        if(is_MaskButton)
        {
            foreach(GameObject obj in master_Storage.MaskObjects_Array)
            {
                if(obj.tag == _maskTag)
                {
                    Mask_Object = obj;
                }
            }
        }
        if(is_ColorButton)
        {
            button = GetComponent<Button>();
            button.GetComponent<Image>().color = _color;
        }
    }

    //  Automated the mask process so not required in buttons. Same functions being used in the colormain script in automation region
    #region Mask Process
    /*public void ApplyMask(string _object_Tag)
    {
        if (!master_Storage.is_Coloring)
        {
            if (!master_Storage.Hand_ApplyMask.activeSelf)
            {
                if (master_Storage.levelnum == 0 || _gameManager._is_Tutorial_Level)
                {
                    if (master_Storage._is_ExecuteTutorialStep)
                    {
                        StartCoroutine(Disable_MaskHand(_object_Tag));
                        StartCoroutine(PlaySound(master_Storage._Hand_SFX, 0f));

                        _gameManager.Execute_Tutorial_Level();
                    }
                }
                else
                {
                    StartCoroutine(Disable_MaskHand(_object_Tag));
                    StartCoroutine(PlaySound(master_Storage._Hand_SFX, 0f));
                }
            }
        }
    }

    //  Disables the Hand which applies mask to head model after specified time
    private IEnumerator Disable_MaskHand(string Object_ID)
    {
        master_Storage.Hand_ApplyMask.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        //colormain.MaskObjects_Process(Mask_Object.tag);
        colormain.MaskObjects_Process(Object_ID);
        yield return new WaitForSeconds(0.8f);
        master_Storage.Hand_ApplyMask.SetActive(false);
    }*/
    #endregion

    public void ApplyColor()
    {
        if (!master_Storage.Hand_ApplyMask.activeSelf)
        {
            if (!master_Storage.is_Coloring)
            {
                if(master_Storage.levelnum == 0 || _gameManager._is_Tutorial_Level)
                {
                    if (master_Storage._is_ExecuteTutorialStep)
                    {
                        colormain.Color_Objects(_color);
                        StartCoroutine(PlaySound(master_Storage._Liquid_SFX, 0.5f));

                        _gameManager.Execute_Tutorial_Level();
                    }
                }
                else
                {
                    colormain.Color_Objects(_color);
                    StartCoroutine(PlaySound(master_Storage._Liquid_SFX, 0.5f));

                    master_Storage._step_color = _color;
                    master_Storage.Check_Step_Progress();
                }
            }
        }
    }

    private IEnumerator PlaySound(AudioClip sfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        master_Storage._audio.PlayOneShot(sfx);
    }
}
