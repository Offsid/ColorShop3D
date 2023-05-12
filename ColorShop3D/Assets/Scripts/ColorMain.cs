using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorMain : MonoBehaviour
{
    #region Script References
    private MasterStorage master_Storage;
    private GameManager _gameManager;
    #endregion

    #region Objects insertion in ARRAY and DICTIONARY
    //  String = HeadObject unique name && Bool = HeadObject can be colored or not
    private Dictionary<string, bool> HeadObjects_Dictionary = new Dictionary<string, bool>();

    //  String = MaskObject unique name && Bool = MaskObject is enabled or not
    private Dictionary<string, bool> MaskObjects_Dictionary = new Dictionary<string, bool>();
    #endregion

    #region Raycast Input
    private Ray ray;
    private RaycastHit hit;
    #endregion

    #region Heterogenous Values
    private Color tempCol;
    private Color _temp_original_color;
    private int _index_count = 0;
    #endregion


    private void Awake()
    {
        master_Storage = FindObjectOfType<MasterStorage>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Add_Objects_toDictionary();
        master_Storage.Paint_Material.SetFloat("_FillAmount", master_Storage.max_FillAmount);
        master_Storage.Mask_PaintMaterial.SetFloat("_FillAmount", master_Storage.max_FillAmount);
        master_Storage.current_FillAmount = master_Storage.max_FillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFillAmount();

        //Get_Raycast_Input();

        /*if (master_Storage._is_Highlight)
        {
            Highlight_Part();
        }*/
    }

    //  Adds gameobjects from their source arrays to their destination dictionaries
    private void Add_Objects_toDictionary()
    {
        //  Adds HeadObjects to the HeadObject Dictionary
        for (int i = 0; i < master_Storage.HeadObjects_Array.Length; i++)
        {
            HeadObjects_Dictionary.Add(master_Storage.HeadObjects_Array[i].tag, true);
        }

        //  Adds MaskObjects to the MaskObject Dictionary
        for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
        {
            MaskObjects_Dictionary.Add(master_Storage.MaskObjects_Array[i].tag, false);
        }
    }

    //  Uses Raycast as input for identifying objects
    private void Get_Raycast_Input()
    {
        if (master_Storage._is_Game_Started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Objective"))
                    {
                        if (!master_Storage._UI_Objective_Holder.activeSelf)
                        {
                            master_Storage._UI_Objective_Holder.SetActive(true);

                            if (master_Storage.levelnum == 0 || _gameManager._is_Tutorial_Level)
                            {
                                if (master_Storage._Tutorial_Counter == 1)
                                {
                                    _gameManager.Execute_Tutorial_Level();
                                }
                            }
                        }
                        else
                        {
                            master_Storage._UI_Objective_Holder.SetActive(false);

                            if (master_Storage.levelnum == 0 || _gameManager._is_Tutorial_Level)
                            {
                                if (master_Storage._Tutorial_Counter == 2)
                                {
                                    _gameManager.Execute_Tutorial_Level();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    //  Compares the dictionary values with the array list and sorts the gamobjects for coloring process
    public void Color_Objects(Color color)//string color_name)
    {
        //Disable_Highlight();

        master_Storage.is_Coloring = true;
        master_Storage.is_UpdateFillAmount = true;
        RandomWave();
        master_Storage._PaintLiquid.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        master_Storage.Hand_PourPaint.SetActive(true);
        var temp_liquidPaint = master_Storage._LiquidPaintVFX.GetComponent<ParticleSystem>().main;
        temp_liquidPaint.startColor = color;

        //  Paints Head Objects
        string keyname;

        for (int i = 0; i < master_Storage.HeadObjects_Array.Length; i++)
        {
            keyname = master_Storage.HeadObjects_Array[i].tag;

            if (HeadObjects_Dictionary.ContainsKey(keyname))
            {
                if (HeadObjects_Dictionary[keyname] == true)
                {
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetColor("_TopColor", color);
                }
            }
        }
    }

    //  Changes Color of the Gameobject
    private Color Object_ChangeColor(string color_name)
    {
        ColorUtility.TryParseHtmlString(color_name, out tempCol);

        return tempCol;
    }

    //  Enables/Disables the mask models and excludes/includes the headobjects in/from coloring process
    public void MaskObjects_Process(string MaskObject_Tag)
    {
        if(!master_Storage.is_Coloring)
        {
            string keyname;
            string _temp_keyname;
            keyname = MaskObject_Tag;

            #region Automated Covering
            for(int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
            {
                //  If the provided keyname matches then the face elemnt will remain/will be uncovered and will be included in color process
                if(keyname == master_Storage.MaskObjects_Array[i].tag)
                {
                    _temp_keyname = master_Storage.MaskObjects_Array[i].tag;

                    //  Updating values of both Head & Mask Dictionary
                    MaskObjects_Dictionary[_temp_keyname] = false;
                    HeadObjects_Dictionary[_temp_keyname] = true;

                    //  Mask of the particular face element will be disabled
                    master_Storage.MaskObjects_Array[i].SetActive(false);
                }
                //  If the keyname does not matches then face elements will be covered and excluded from coloring process
                else
                {
                    _temp_keyname = master_Storage.MaskObjects_Array[i].tag;

                    //  Updating values of both Head & Mask Dictionary
                    MaskObjects_Dictionary[_temp_keyname] = true;
                    HeadObjects_Dictionary[_temp_keyname] = false;

                    //  Mask of the particular face element will be enabled
                    master_Storage.MaskObjects_Array[i].SetActive(true);
                }
            }
            #endregion

            Highlight_Part_to_color(keyname);

            /*if (MaskObjects_Dictionary.ContainsKey(keyname))
            {
                //  When mask model is disabled
                if (MaskObjects_Dictionary[keyname] == false)
                {
                    //  MaskObject Dictionary Updated
                    MaskObjects_Dictionary[keyname] = true;

                    //  Enabling the MaskObject model
                    for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
                    {
                        if (keyname == master_Storage.MaskObjects_Array[i].tag)
                        {
                            master_Storage.MaskObjects_Array[i].SetActive(true);
                        }
                    }

                    //  Updating HeadObjects Dictionary to exclude masked object from coloring process
                    if (HeadObjects_Dictionary.ContainsKey(keyname))
                    {
                        HeadObjects_Dictionary[keyname] = false;
                    }
                }
                //  When mask model  is enabled
                else if (MaskObjects_Dictionary[keyname] == true)
                {
                    //  MaskObject Dictionary Updated
                    MaskObjects_Dictionary[keyname] = false;

                    //  Disabling the MaskObject model
                    for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
                    {
                        if (keyname == master_Storage.MaskObjects_Array[i].tag)
                        {
                            master_Storage.MaskObjects_Array[i].SetActive(false);
                        }
                    }

                    //  Updating HeadObjects Dictionary to include masked object in coloring process
                    if (HeadObjects_Dictionary.ContainsKey(keyname))
                    {
                        HeadObjects_Dictionary[keyname] = true;
                    }
                }
            }*/
        }
    }

    //  Updates the fill amount of the material of the models
    private void UpdateFillAmount()
    {
        if (master_Storage.is_UpdateFillAmount)
        {
            master_Storage.current_FillAmount = Mathf.MoveTowards(master_Storage.current_FillAmount, master_Storage.min_FillAmount, master_Storage.Fill_Speed * Time.deltaTime);

            //  Update HeadObject Fill Amount
            for (int i = 0; i < master_Storage.HeadObjects_Array.Length; i++)
            {
                string keyname = master_Storage.HeadObjects_Array[i].tag;

                if (HeadObjects_Dictionary.ContainsKey(keyname))
                {
                    if (HeadObjects_Dictionary[keyname] == true)
                    {
                        master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", master_Storage.current_FillAmount);
                    }
                }
            }

            //  Update MasksObject Fill Amount
            for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
            {
                string keyname = master_Storage.MaskObjects_Array[i].tag;

                if (MaskObjects_Dictionary.ContainsKey(keyname))
                {
                    if (MaskObjects_Dictionary[keyname] == true)
                    {
                        master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", master_Storage.current_FillAmount);
                    }
                }
            }
        //}

            //  Reset the fill amount
            if (Mathf.Approximately(master_Storage.current_FillAmount, master_Storage.min_FillAmount))
            {
                master_Storage.is_UpdateFillAmount = false;
                master_Storage.current_FillAmount = master_Storage.max_FillAmount;
                //master_Storage.is_Coloring = false;
                master_Storage.Hand_PourPaint.SetActive(false);

                //  Reset HeadObject Fill Amount
                for (int i = 0; i < master_Storage.HeadObjects_Array.Length; i++)
                {
                    string keyname = master_Storage.HeadObjects_Array[i].tag;

                    if (HeadObjects_Dictionary.ContainsKey(keyname))
                    {
                        if (HeadObjects_Dictionary[keyname] == true)
                        {
                            Color temp_Color = master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.GetColor("_TopColor");
                            master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetColor("_BottomColor", temp_Color);
                            master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", master_Storage.current_FillAmount);
                            //Debug.Log("Shader Fill Reset");
                        }
                    }
                }

                //  Reset MaskObject Fill Amount
                for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
                {
                    string keyname = master_Storage.MaskObjects_Array[i].tag;

                    if (MaskObjects_Dictionary.ContainsKey(keyname))
                    {
                        if (MaskObjects_Dictionary[keyname] == true)
                        {
                            master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", master_Storage.current_FillAmount);

                            Color temp_TopColor = master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.GetColor("_TopColor");
                            Color temp_BottomColor = master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.GetColor("_BottomColor");
                            master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetColor("_BottomColor", temp_TopColor);
                            master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetColor("_TopColor", temp_BottomColor);
                            //master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", master_Storage.current_FillAmount);
                        }
                    }
                }
                StartCoroutine(master_Storage.Delayed_Function_Call(Automate_Mask_Process, 0.5f));
            }
        }
    }

    //  Generates random waves
    private void RandomWave()
    {
        int rand = Random.Range(1,4);
        float freq = 0, amp = 0, speed = 0, freq2 = 0, amp2 = 0, speed2 = 0;

        switch(rand)
        {
            case 1:
                freq = 2.7f; amp = 0.1f; speed = 10f; freq2 = 1.62f; amp2 = -0.18f; speed2 = -13.5f;
                break;

            case 2:
                freq = -1.14f; amp = 0.11f; speed = 10f; freq2 = -1.14f; amp2 = -0.18f; speed2 = -8.7f;
                break;

            case 3:
                freq = 3.9f; amp = 0.02f; speed = 10f; freq2 = 3.9f; amp2 = -0.05f; speed2 = -8.7f;
                break;
        }

        //  Random Wave for Head Object
        for (int i = 0; i < master_Storage.HeadObjects_Array.Length; i++)
        {
            string keyname = master_Storage.HeadObjects_Array[i].tag;

            if (HeadObjects_Dictionary.ContainsKey(keyname))
            {
                if (HeadObjects_Dictionary[keyname] == true)
                {
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Freq", freq);
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Amp", amp);
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Speed", speed);
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Freq2", freq2);
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Amp2", amp2);
                    master_Storage.HeadObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Speed2", speed2);
                }
            }
        }

        //  Random Wave for Mask Object
        for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
        {
            string keyname = master_Storage.MaskObjects_Array[i].tag;

            if (MaskObjects_Dictionary.ContainsKey(keyname))
            {
                if (MaskObjects_Dictionary[keyname] == true)
                {
                    master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Freq", freq);
                    master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Amp", amp);
                    master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Speed", speed);
                    master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Freq2", freq2);
                    master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Amp2", amp2);
                    master_Storage.MaskObjects_Array[i].GetComponent<MeshRenderer>().material.SetFloat("_Speed2", speed2);
                }
            }
        }
    }

    //  Disables All Masks
    public void Disable_AllMasks()
    {
        //  Disabling the MaskObject model
        for (int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
        {
            master_Storage.MaskObjects_Array[i].SetActive(false);
        }

        string _temp_key;
        //  Resets the Mask & Head Dictionary
        for(int i = 0; i < master_Storage.MaskObjects_Array.Length; i++)
        {
            _temp_key = master_Storage.MaskObjects_Array[i].tag;

            HeadObjects_Dictionary[_temp_key] = true;
            MaskObjects_Dictionary[_temp_key] = false;
        }
    }

    #region Automated Mask Process
    //  Automates the masking process
    public void Automate_Mask_Process()
    {
        //  When First Cycle
        if (!master_Storage._is_First_ColorCycleComplete)
        {
            master_Storage.is_Coloring = false;

            if (master_Storage._automation_Counter < master_Storage.MaskObjects_Array.Length)
            {
                ApplyMask(master_Storage.MaskObjects_Array[master_Storage._automation_Counter].tag);
            }

            master_Storage._automation_Counter++;

            if (master_Storage._automation_Counter > master_Storage.MaskObjects_Array.Length)
            {
                master_Storage._automation_Counter = 0;

                _gameManager.ResultProceed();
            }
        }
        //  Second Cycle and onwards
        else
        {
            master_Storage.is_Coloring = false;

            master_Storage.Reset_Step_Progress();

            if (master_Storage._automation_Counter < master_Storage._WrongColor_HeadID.Count)
            {
                ApplyMask(master_Storage.MaskObjects_Array[master_Storage._WrongColor_HeadID[master_Storage._automation_Counter]].tag);
            }

            master_Storage._automation_Counter++;

            if (master_Storage._automation_Counter > master_Storage._WrongColor_HeadID.Count)
            {
                master_Storage._automation_Counter = 0;

                _gameManager.ResultProceed();
            }
        }
    }

    //  Same functions which was used in GAME BUTTON script
    public void ApplyMask(string _object_Tag)
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
            MaskObjects_Process(Object_ID);
            yield return new WaitForSeconds(0.8f);
            master_Storage.Hand_ApplyMask.SetActive(false);
        }

    private IEnumerator PlaySound(AudioClip sfx, float delay)
        {
            yield return new WaitForSeconds(delay);
            master_Storage._audio.PlayOneShot(sfx);
        }
    #endregion

    #region Highlight Process
    //  Highlights the part that has to be colored
    private void Highlight_Part_to_color(string _object_tag)
    {
        for (int i = 0; i < master_Storage.HeadObjects_Array.Length; i++)
        {
            if (master_Storage.HeadObjects_Array[i].tag == _object_tag)
            {
                _index_count = i;
                _temp_original_color = master_Storage.HeadObjects_Array[_index_count].GetComponent<MeshRenderer>().material.GetColor("_BottomColor");
                master_Storage._is_Highlight = true;
            }
        }
    }

    /*private void Highlight_Part()
    {
        tempCol = Color.Lerp(_temp_original_color, master_Storage._Highlight_Color, Mathf.PingPong(Time.time * 0.25f, 0.25f));
        master_Storage.HeadObjects_Array[_index_count].GetComponent<MeshRenderer>().material.SetColor("_BottomColor", tempCol);
    }

    private void Disable_Highlight()
    {
        master_Storage._is_Highlight = false;
        master_Storage.HeadObjects_Array[_index_count].GetComponent<MeshRenderer>().material.SetColor("_BottomColor", _temp_original_color);
    }*/
    #endregion
}
