using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
//using Tabtale.TTPlugins;

public class MasterStorage : MonoBehaviour
{
    //  This file stores every gameobject that is to be used by any other script in the game

    #region Script References
    private GameManager _gameManager;
    #endregion

    #region State Machine For Character Selection
    [Header("Character Selection")]
    public Character _character;
    public enum Character
    {
        Batman, Wolverine, Antman, Baymax, Buzz, America, Cyclops, Deadpool, Deathstroke, Doraemon, Flash, Lantern, Imperial, Invisible,
        Dredd, Magneto, Mandalorian, Chief, PeaceMaker, Redhood, Robin, Robocop, Spiderman, Squid, Vision
    }
    #endregion

    #region Color Values
    #region Required Colors
    private Color[] _RequiredColors;
    [Header("Required Colors : [0] = Skin [Last] = body")]
    public Color[] _BatmanRequiredColors;
    public Color[] _WolverineRequiredColors;
    public Color[] _AntManRequiredColors;
    public Color[] _BaymaxRequiredColors;
    public Color[] _BuzzRequiredColors;
    public Color[] _AmericaRequiredColors;
    public Color[] _CyclopsRequiredColors;
    public Color[] _DeadpoolRequiredColors;
    public Color[] _DeathstrokeRequiredColors;
    public Color[] _DoraemonRequiredColors;
    public Color[] _FlashRequiredColors;
    public Color[] _LanternRequiredColors;
    public Color[] _ImperialRequiredColors;
    public Color[] _InvisibleRequiredColors;
    public Color[] _DreddRequiredColors;
    public Color[] _MagnetoRequiredColors;
    public Color[] _MandalorianRequiredColors;
    public Color[] _ChiefRequiredColors;
    public Color[] _PeaceMakerRequiredColors;
    public Color[] _RedhoodRequiredColors;
    public Color[] _RobinRequiredColors;
    public Color[] _RobocopRequiredColors;
    public Color[] _SpidermanRequiredColors;
    public Color[] _SquidRequiredColors;
    public Color[] _VisionRequiredColors;
    #endregion

    #region Result Colors
    //[SerializeField]
    [HideInInspector]
    public Color[] _ResultColors;
    [Header("Result Colors")]
    [Header("Color Comparison  = Head Object Hierarchy")]
    public Color[] _BatmanResultColors;
    public Color[] _WolverineResultColors;
    public Color[] _AntManResultColors;
    public Color[] _BaymaxResultColors;
    public Color[] _BuzzResultColors;
    public Color[] _AmericaResultColors;
    public Color[] _CyclopsResultColors;
    public Color[] _DeadpoolResultColors;
    public Color[] _DeathstrokeResultColors;
    public Color[] _DoraemonResultColors;
    public Color[] _FlashResultColors;
    public Color[] _LanternResultColors;
    public Color[] _ImperialResultColors;
    public Color[] _InvisibleResultColors;
    public Color[] _DreddResultColors;
    public Color[] _MagnetoResultColors;
    public Color[] _MandalorianResultColors;
    public Color[] _ChiefResultColors;
    public Color[] _PeaceMakerResultColors;
    public Color[] _RedhoodResultColors;
    public Color[] _RobinResultColors;
    public Color[] _RobocopResultColors;
    public Color[] _SpidermanResultColors;
    public Color[] _SquidResultColors;
    public Color[] _VisionResultColors;
    #endregion

    #region Available Colors
    [Header("Available Colors")]
    public Color Skin;
    public Color Black;
    public Color White;
    public Color Wolverine_Yellow;
    public Color Baymax_Red;
    public Color AntMan_Silver;
    public Color Buzz_Purple;
    public Color Cyclops_Gold;
    public Color Cyclops_Blue;
    public Color Deathstroke_Orange;
    public Color Doraemon_Blue;
    public Color Lantern_Green;
    public Color Magneto_Grey;
    public Color Chief_Green;
    public Color Redhood_Red;
    #endregion

    public Color _Highlight_Color;
    #endregion

    #region Material References
    [Header("Materials")]
    [HideInInspector]
    public Material Paint_Material;
    [HideInInspector]
    public Material Mask_PaintMaterial;
    #endregion

    #region Head Objects References
    [Header("Main Heads")]
    [HideInInspector]
    public GameObject[] _MainHeads;
    [HideInInspector]
    public GameObject[] HeadObjects_Array;
    [HideInInspector]
    public GameObject[] MaskObjects_Array;
    #endregion

    #region Character Sprite References
    /*#region Mask Icons
    [Header("Mask Icons")]
    public Sprite[] _BatmanMaskIcon;
    public Sprite[] _WolverineMaskIcon;
    public Sprite[] _AntManMaskIcon;
    public Sprite[] _BaymaxMaskIcon;
    public Sprite[] _BuzzMaskIcon;
    public Sprite[] _AmericaMaskIcon;
    public Sprite[] _CyclopsMaskIcon;
    public Sprite[] _DeadpoolMaskIcon;
    public Sprite[] _DeathstrokeMaskIcon;
    public Sprite[] _DoraemonMaskIcon;
    public Sprite[] _FlashMaskIcon;
    public Sprite[] _LanternMaskIcon;
    public Sprite[] _ImperialMaskIcon;
    public Sprite[] _InvisibleMaskIcon;
    public Sprite[] _DreddMaskIcon;
    public Sprite[] _MagnetoMaskIcon;
    public Sprite[] _MandalorianMaskIcon;
    public Sprite[] _ChiefMaskIcon;
    public Sprite[] _PeaceMakerMaskIcon;
    public Sprite[] _RedhoodMaskIcon;
    public Sprite[] _RobinMaskIcon;
    public Sprite[] _RobocopMaskIcon;
    public Sprite[] _SpidermanMaskIcon;
    public Sprite[] _SquidMaskIcon;
    public Sprite[] _VisionMaskIcon;
    #endregion
    */

    [Header("Output Image References")]
    [HideInInspector]
    public Sprite[] _OutputImage;

    [Header("NPC Thought Head Arts")]
    [HideInInspector]
    public Sprite[] _NPC_HeadArt;
    #endregion

    #region GameObject References
    [Header("Button References")]
    [HideInInspector]
    public GameObject[] _MaskButtons;
    [HideInInspector]
    public GameObject[] _ColorButtons;
    [Header("GameObject References")]
    [HideInInspector]
    public GameObject _OutputImageHolder;
    [HideInInspector]
    public GameObject _Body;
    private GameObject _MainHead;
    private GameObject _HeadObjects;
    private GameObject _MaskObjects;
    [HideInInspector]
    public GameObject _PaintLiquid;
    [HideInInspector]
    public GameObject Hand_PourPaint;
    [HideInInspector]
    public GameObject Hand_ApplyMask;
    [HideInInspector]
    public Transform _HeadSlot;
    [HideInInspector]
    public Transform _AudioButton;
    [HideInInspector]
    public GameObject _Hand_HoldHead, _Accept_Button, Reject_Button, _Hand_Cursor, _ControlHint_Cursor;
    [HideInInspector]
    public GameObject[] _Objects_ToHide;

    #region Hints
    [HideInInspector]
    public float _hint_delay = 0.5f;
    [HideInInspector]
    public bool _is_Hints_enabled = false;

    [HideInInspector]
    public GameObject _Hint_MainPanel;
    /*
    [Header("Hints")]
    public GameObject[] _Mandalorian_Hints;
    public GameObject[] _Imperial_Hints;
    public GameObject[] _PeaceMaker_Hints;
    public GameObject[] _Batman_Hints;
    public GameObject[] _Chief_Hints;
    public GameObject[] _Buzz_Hints;
    public GameObject[] _Antman_Hints;
    public GameObject[] _Lantern_Hints;
    public GameObject[] _Flash_Hints;
    public GameObject[] _Magneto_Hints;
    public GameObject[] _Redhood_Hints;
    public GameObject[] _America_Hints;
    public GameObject[] _Baymax_Hints;
    public GameObject[] _Robin_Hints;
    public GameObject[] _Spiderman_Hints;
    public GameObject[] _Robocop_Hints;
    public GameObject[] _Deadpool_Hints;
    public GameObject[] _Doraemon_Hints;
    public GameObject[] _Invisible_Hints;
    public GameObject[] _Cyclops_Hints;
    public GameObject[] _Wolverine_Hints;
    public GameObject[] _Vision_Hints;
    public GameObject[] _Dredd_Hints;
    public GameObject[] _Squid_Hints;
    public GameObject[] _Deathstroke_Hints;*/
    #endregion
    #endregion

    #region VFX References
    [Header("VFX References")]
    [HideInInspector]
    public ParticleSystem _LiquidPaintVFX;
    [HideInInspector]
    public ParticleSystem _WinVFX;
    #endregion

    #region Heterogenous Values
    [Header("Heterogenous Values")]
    [HideInInspector]
    public float current_FillAmount = 1f;
    [HideInInspector]
    public float Fill_Speed = 1f;
    [HideInInspector]
    public float max_FillAmount = 1f;
    [HideInInspector]
    public float min_FillAmount = 0f;
    [HideInInspector]
    public bool is_UpdateFillAmount = false;
    [HideInInspector]
    public bool is_Coloring = false;
    [HideInInspector]
    public int levelnum = 0;
    public int _Creative_LevelNum = 0;
    [HideInInspector]
    public int _temp_levelnum = 0;
    private float _resultCounter = 0;
    private float _max_resultCount = 0;
    private float _resultAccuracy = 0f;
    private float _base_resultAccuracy = 0f;
    [HideInInspector]
    public bool is_calculateResult = false;
    [HideInInspector]
    public bool is_LevelPassed = true;
    [HideInInspector]
    public bool _is_Tutorial = false;
    [HideInInspector]
    public int _Tutorial_Counter = 0;
    [HideInInspector]
    public bool _is_ExecuteTutorialStep = false;
    public bool is_CreativeMode = false;
    [HideInInspector]
    public int _automation_Counter = 0;
    [HideInInspector]
    public delegate void Delayed_Call();
    [HideInInspector]
    public bool _is_Highlight = false;
    private int _step_Count = 0;
    [HideInInspector]
    public Color _step_color;
    [HideInInspector]
    public GameObject[] _progress_Box;
    [HideInInspector]
    public List<int> _WrongColor_HeadID;  //  Head Object's int id stored here which don't have correct colors
    [HideInInspector]
    public bool _is_First_ColorCycleComplete = false;
    private int _Pass_Count = 0;
    #endregion

    #region UI Values
    [Header("UI GameObjects")]
    [HideInInspector]
    public Image _accuracyBar;
    [HideInInspector]
    public TextMeshProUGUI _accuracyNum;
    [HideInInspector]
    public TextMeshProUGUI _Prev_levelnum;
    [HideInInspector]
    public TextMeshProUGUI _Next_levelnum;
    [HideInInspector]
    public TextMeshProUGUI[] _current_levelnum;
    [HideInInspector]
    public GameObject _nextButton;
    [HideInInspector]
    public GameObject _retryButton;
    [HideInInspector]
    public GameObject _LoadUI;
    [HideInInspector]
    public GameObject _UI_Objective_Holder;
    [HideInInspector]
    public Image _UI_Objective_Image;
    [HideInInspector]
    public GameObject[] _progress_Bar;
    [HideInInspector]
    public Sprite Checkmark;
    [HideInInspector]
    public Sprite Crossmark;

    #region Tutorial Icons
    [HideInInspector]
    public GameObject[] _Tutorial_Icon;
    #endregion

    #endregion

    #region SFX Values
    [HideInInspector]
    public AudioSource _audio;

    [HideInInspector]
    public AudioClip _Liquid_SFX, _Click_SFX, _Win_SFX;
    [HideInInspector]
    public AudioClip _Hand_SFX;

    [HideInInspector]
    public Sprite[] _sound_icon;
    #endregion

    #region Game Session Values
    [HideInInspector]
    public bool _is_Game_Started = false;
    #endregion

    #region Main Camera Values
    [Header("Main Camera Values")]
    [HideInInspector]
    public Camera _main_Camera;
    [HideInInspector]
    public float _target_FOV = 60f;
    [HideInInspector]
    public float _speed_FOV = 1f;
    private float _camera_rot_speed = 1f;
    private float _target_X_rot = 30f;
    private bool _is_look_NPC = false, _is_look_head = false;
    [HideInInspector]
    public bool _is_set_FOV = false;
    #endregion

    #region NPC Values
    [HideInInspector]
    public NPC _NPC;
    [HideInInspector]
    public GameObject _NPC_Head, _NPC_Eyebrow, _NPC_Body;
    [HideInInspector]
    public GameObject _NPC_Primary_Body;
    [HideInInspector]
    public Quaternion _NPC_Main_StandRot;
    [HideInInspector]
    public Vector3 _NPC_Main_StandPos;
    #endregion


    private void Awake()
    {
        // Initialize CLIK Plugin
        //TTPCore.Setup();

        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (is_CreativeMode)
        {
            levelnum = _Creative_LevelNum;
        }
        else
        {
            levelnum = 1;
        }

        //  Automatically assigns all the values at specific variables available inside this script only for saving time to prevent spaghetti structure

        //Sort_According_To_Character();

        //Sort_Head_Mask_Objects();

        //Assign_Head_To_Body();
    }

    //  Makes the Head Object's and Mask Object's group child of the Head slot of the body
    public void Assign_Head_To_Body()
    {
        _MainHead.transform.parent = _HeadSlot;
    }

    //  Automatically finds the Head & Mask Objects in game and assigns them to their respective arrays. Also assigns them their resective tags
    private void Sort_Head_Mask_Objects()
    {
        _HeadObjects = _MainHead.transform.Find("Head Objects").gameObject;
        _MaskObjects = _MainHead.transform.Find("Mask Objects").gameObject;

        HeadObjects_Array = new GameObject[_HeadObjects.transform.childCount];
        MaskObjects_Array = new GameObject[_MaskObjects.transform.childCount];

        _max_resultCount = HeadObjects_Array.Length;

        for(int i = 0; i < _HeadObjects.transform.childCount; i++)
        {
            _HeadObjects.transform.GetChild(i).gameObject.tag = "Object" + (i + 1).ToString();
            HeadObjects_Array[i] = _HeadObjects.transform.GetChild(i).gameObject;
        }

        for(int i = 0; i < _MaskObjects.transform.childCount; i++)
        {
            _MaskObjects.transform.GetChild(i).gameObject.tag = "Object" + (i + 1).ToString();
            MaskObjects_Array[i] = _MaskObjects.transform.GetChild(i).gameObject;
        }
    }

    //  Sorts Colors and Mask Buttons According to the character
    public void Sort_According_To_Character()
    {
        switch (_character)
        {
            #region Batman
            case Character.Batman:
                Initialize_Attributes(0, _BatmanResultColors, _BatmanRequiredColors);
                break;
            #endregion

            #region Wolverine
            case Character.Wolverine:
                Initialize_Attributes(1, _WolverineResultColors, _WolverineRequiredColors);
                break;
            #endregion

            #region Antman
            case Character.Antman:
                Initialize_Attributes(2, _AntManResultColors, _AntManRequiredColors);
                break;
            #endregion

            #region Baymax
            case Character.Baymax:
                Initialize_Attributes(3, _BaymaxResultColors, _BaymaxRequiredColors);
                break;
            #endregion

            #region Buzz
            case Character.Buzz:
                Initialize_Attributes(4, _BuzzResultColors, _BuzzRequiredColors);
                break;
            #endregion

            #region America
            case Character.America:
                Initialize_Attributes(5, _AmericaResultColors, _AmericaRequiredColors);
                break;
            #endregion

            #region Cyclops
            case Character.Cyclops:
                Initialize_Attributes(6, _CyclopsResultColors, _CyclopsRequiredColors);
                break;
            #endregion

            #region Deadpool
            case Character.Deadpool:
                Initialize_Attributes(7, _DeadpoolResultColors, _DeadpoolRequiredColors);
                break;
            #endregion

            #region Deathstroke
            case Character.Deathstroke:
                Initialize_Attributes(8, _DeathstrokeResultColors, _DeathstrokeRequiredColors);
                break;
            #endregion

            #region Doraemon
            case Character.Doraemon:
                Initialize_Attributes(9, _DoraemonResultColors, _DoraemonRequiredColors);
                break;
            #endregion

            #region Flash
            case Character.Flash:
                Initialize_Attributes(10, _FlashResultColors, _FlashRequiredColors);
                break;
            #endregion

            #region Lantern
            case Character.Lantern:
                Initialize_Attributes(11, _LanternResultColors, _LanternRequiredColors);
                break;
            #endregion

            #region Imperial
            case Character.Imperial:
                Initialize_Attributes(12, _ImperialResultColors, _ImperialRequiredColors);
                break;
            #endregion

            #region Invisible
            case Character.Invisible:
                Initialize_Attributes(13, _InvisibleResultColors, _InvisibleRequiredColors);
                break;
            #endregion

            #region Dredd
            case Character.Dredd:
                Initialize_Attributes(14, _DreddResultColors, _DreddRequiredColors);
                break;
            #endregion

            #region Magneto
            case Character.Magneto:
                Initialize_Attributes(15, _MagnetoResultColors, _MagnetoRequiredColors);
                break;
            #endregion

            #region Mandalorian
            case Character.Mandalorian:
                Initialize_Attributes(16, _MandalorianResultColors, _MandalorianRequiredColors);
                break;
            #endregion

            #region Chief
            case Character.Chief:
                Initialize_Attributes(17, _ChiefResultColors, _ChiefRequiredColors);
                break;
            #endregion

            #region PeaceMaker
            case Character.PeaceMaker:
                Initialize_Attributes(18, _PeaceMakerResultColors, _PeaceMakerRequiredColors);
                break;
            #endregion

            #region Redhood
            case Character.Redhood:
                Initialize_Attributes(19, _RedhoodResultColors, _RedhoodRequiredColors);
                break;
            #endregion

            #region Robin
            case Character.Robin:
                Initialize_Attributes(20, _RobinResultColors, _RobinRequiredColors);
                break;
            #endregion

            #region Robocop
            case Character.Robocop:
                Initialize_Attributes(21, _RobocopResultColors, _RobocopRequiredColors);
                break;
            #endregion

            #region Spiderman
            case Character.Spiderman:
                Initialize_Attributes(22, _SpidermanResultColors, _SpidermanRequiredColors);
                break;
            #endregion

            #region Squid
            case Character.Squid:
                Initialize_Attributes(23, _SquidResultColors, _SquidRequiredColors);
                break;
            #endregion

            #region Vision
            case Character.Vision:
                Initialize_Attributes(24, _VisionResultColors, _VisionRequiredColors);
                break;
                #endregion
        }

        _UI_Objective_Image.sprite = _OutputImageHolder.GetComponent<SpriteRenderer>().sprite;
    }

    //  Calculates Result Accuracy
    public void CalculateResult()
    {
        _WrongColor_HeadID.Clear();

        if (!_is_First_ColorCycleComplete)
        {
            _is_First_ColorCycleComplete = true;
        }

        for(int i = 0; i < HeadObjects_Array.Length; i++)
        {
            Color temp_color_comp = HeadObjects_Array[i].GetComponent<MeshRenderer>().material.GetColor("_BottomColor");

            if(temp_color_comp == _ResultColors[i])
            {
                _resultCounter += 1;
            }
            else
            {
                _WrongColor_HeadID.Add(i);
            }
        }
        _resultAccuracy = (_resultCounter / _max_resultCount);

        if (_resultCounter == _max_resultCount)
        {
            NPC_Happy();
            Hand_HoldHead();
        }
        else
        {
            NPC_Angry();
        }
    }

    public void UpdateAccuracy_Bar()
    {
        _base_resultAccuracy = Mathf.MoveTowards(_base_resultAccuracy, _resultAccuracy, Time.deltaTime * 0.8f);
        _accuracyBar.fillAmount = _base_resultAccuracy;
        _accuracyNum.text = (_base_resultAccuracy * 100).ToString("F0") + "%";
    }

    //  Decides which result button to activate depending upon the accuracy result
    public void ActivateResultButtons()
    {
        if(_resultCounter == _max_resultCount)
        {
            //_Body.GetComponent<Animator>().SetTrigger("Dance");
            _nextButton.SetActive(true);
            _WinVFX.Play();
            _audio.PlayOneShot(_Win_SFX);
        }
        else
        {
            //_Body.GetComponent<Animator>().SetTrigger("Sad");
            //_retryButton.SetActive(true);
            _resultCounter = 0;

            _gameManager = FindObjectOfType<GameManager>();

            _gameManager.Clik_LevelFailed();
            _gameManager.ResultUI.SetActive(false);
            is_calculateResult = false;
            _gameManager.StartGame();
            Reset_Camera_Rotation();
        }
    }

    //  Universal initialization of character's attributes
    public void Initialize_Attributes(int _Head_id, Color[] _Character_Result_Colors, Color[] _Character_Required_Colors)
    {
        _MainHead = Instantiate(_MainHeads[_Head_id], Vector3.zero, Quaternion.identity);
        _ResultColors = new Color[_Character_Result_Colors.Length];
        _RequiredColors = new Color[_Character_Result_Colors.Length];

        for (int i = 0; i < _Character_Required_Colors.Length; i++)
        {
            _Character_Required_Colors[i].a = 1;
        }

        for (int i = 0; i < _Character_Result_Colors.Length; i++)
        {
            _Character_Result_Colors[i].a = 1;
            _ResultColors[i] = _Character_Result_Colors[i];
        }

        _Body.transform.Find("Chibibody").GetComponent<SkinnedMeshRenderer>().material.SetColor("_BottomColor", _Character_Required_Colors[_Character_Required_Colors.Length - 1]);
        _OutputImageHolder.GetComponent<SpriteRenderer>().sprite = _OutputImage[_Head_id];

        Sort_Head_Mask_Objects();

        //  Assigns colors to the color buttons
        for (int i = 0; i < _Character_Required_Colors.Length - 1; i++)
        {
            _ColorButtons[i].SetActive(true);
            _ColorButtons[i].GetComponent<GameButton>().is_ColorButton = true;
            _ColorButtons[i].GetComponent<GameButton>()._color = _Character_Required_Colors[i];
        }

        EnableProgressBar();
    }
    
    //  Call any function at a particular delay
    public IEnumerator Delayed_Function_Call(Delayed_Call _method ,float delay)
    {
        yield return new WaitForSeconds(delay);
        _method();
    }

    private void EnableProgressBar()
    {
        if(_RequiredColors.Length == 2)
        {
            _progress_Bar[0].SetActive(true);

            _progress_Box = new GameObject[_RequiredColors.Length];
            int count = 0;
            for (int i = 0; i < _RequiredColors.Length; i++)
            {
                count++;
                _progress_Box[i] = _progress_Bar[0].transform.Find("Box " + count.ToString()).transform.Find("Result").gameObject;
            }
        }
        else if (_RequiredColors.Length == 3)
        {
            _progress_Bar[1].SetActive(true);

            _progress_Box = new GameObject[_RequiredColors.Length];
            int count = 0;
            for (int i = 0; i < _RequiredColors.Length; i++)
            {
                count++;
                _progress_Box[i] = _progress_Bar[1].transform.Find("Box " + count.ToString()).transform.Find("Result").gameObject;
            }
        }
        else if (_RequiredColors.Length == 4)
        {
            _progress_Bar[2].SetActive(true);

            _progress_Box = new GameObject[_RequiredColors.Length];
            int count = 0;
            for(int i = 0; i < _RequiredColors.Length; i++)
            {
                count++;
                _progress_Box[i] = _progress_Bar[2].transform.Find("Box " + count.ToString()).transform.Find("Result").gameObject;
            }
        }
        else if (_RequiredColors.Length == 5)
        {
            _progress_Bar[3].SetActive(true);

            _progress_Box = new GameObject[_RequiredColors.Length];
            int count = 0;
            for (int i = 0; i < _RequiredColors.Length; i++)
            {
                count++;
                _progress_Box[i] = _progress_Bar[3].transform.Find("Box " + count.ToString()).transform.Find("Result").gameObject;
            }
        }
    }

    public void Check_Step_Progress()
    {
        //  First Cycle
        if (!_is_First_ColorCycleComplete)
        {
            if (_step_color == _ResultColors[_step_Count])
            {
                _progress_Box[_step_Count].GetComponent<Image>().sprite = Checkmark;
                _progress_Box[_step_Count].GetComponent<Image>().color = Color.green;
                _progress_Box[_step_Count].GetComponent<Image>().GetComponent<Transform>().DOScale(0.7f, 2f).SetEase(Ease.InOutBounce);
            }
            else if (_step_color != _ResultColors[_step_Count])
            {
                _progress_Box[_step_Count].GetComponent<Image>().sprite = Crossmark;
                _progress_Box[_step_Count].GetComponent<Image>().color = Color.red;
                _progress_Box[_step_Count].GetComponent<Image>().GetComponent<Transform>().DOScale(0.7f, 2f).SetEase(Ease.InOutBounce);
            }

            _step_Count++;

            if (_step_Count >= _ResultColors.Length)
            {
                _step_Count = 0;
            }
        }
        //  Second Cycle and Onwards
        else
        {
            if (_step_color == _ResultColors[_WrongColor_HeadID[_step_Count]])
            {
                _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().sprite = Checkmark;
                _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().color = Color.green;
                _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().GetComponent<Transform>().DOScale(0.7f, 2f).SetEase(Ease.InOutBounce);
            }
            else if (_step_color != _ResultColors[_WrongColor_HeadID[_step_Count]])
            {
                _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().sprite = Crossmark;
                _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().color = Color.red;
                _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().GetComponent<Transform>().DOScale(0.7f, 2f).SetEase(Ease.InOutBounce);
            }

            _step_Count++;

            if (_step_Count >= _WrongColor_HeadID.Count)
            {
                _step_Count = 0;
            }
        }
    }

    public void Reset_Step_Progress()
    {
        _progress_Box[_WrongColor_HeadID[_step_Count]].GetComponent<Image>().GetComponent<Transform>().DOScale(0f, 0.8f).SetEase(Ease.InOutBounce);
    }

    public void Initiate_Camera_Setup()
    {
        _main_Camera.GetComponent<MainCamera>().enabled = true;
        _is_set_FOV = true;
    }

    public void Camera_Set_FOV()
    {
        if(_main_Camera.fieldOfView != _target_FOV)
        {
            //_main_Camera.fieldOfView = Mathf.Lerp(_main_Camera.fieldOfView, _target_FOV, Time.deltaTime * _speed_FOV);
            _main_Camera.fieldOfView -= _speed_FOV;

            if (_main_Camera.fieldOfView == _target_FOV)
            {
                _is_set_FOV = false;
                //Debug.Log("FOV = " + _main_Camera.fieldOfView);
                _main_Camera.GetComponent<MainCamera>().enabled = false;
            }
        }
    }

    public void Initiate_Camera_Rotation()
    {
        _main_Camera.GetComponent<MainCamera>().enabled = true;
        _is_look_NPC = true;
    }

    public void LookAt_NPC()
    {
        if (_is_look_NPC)
        {
            float temp_rot = _main_Camera.transform.eulerAngles.y;
            temp_rot += _camera_rot_speed;
            //_main_Camera.transform.rotation = Quaternion.Euler(12.547f, temp_rot, 0f);
            _main_Camera.transform.eulerAngles = new Vector3(12.547f, temp_rot, 0f);

            if (_main_Camera.transform.eulerAngles.y >= _target_X_rot)
            {
                _is_look_NPC = false;
            }
        }
    }

    public void Reset_Camera_Rotation()
    {
        _is_look_head = true;
    }

    public void LookAt_Head()
    {
        if (_is_look_head)
        {
            float temp_rot = _main_Camera.transform.eulerAngles.y;
            temp_rot -= _camera_rot_speed;
            _main_Camera.transform.eulerAngles = new Vector3(12.547f, temp_rot, 0f);

            if(_main_Camera.transform.eulerAngles.y <= 1f)
            {
                _is_look_head = false; 
                _main_Camera.GetComponent<MainCamera>().enabled = false;
            }
        }
    }

    #region NPC Functions
    public void Spawn_NPC()
    {
        _NPC_Primary_Body = Instantiate(_NPC_Body, new Vector3(3.223f, -0.8460001f, 0.695f), Quaternion.Euler(Vector3.up * -90f));
        _NPC = _NPC_Primary_Body.GetComponent<NPC>();
        _NPC_Body = _NPC.Body;
        _NPC_Eyebrow = _NPC.Eyebrow;
        _NPC_Head = _NPC.Head;
        _NPC_Body.transform.localScale = Vector3.one * 0.2752101f;

        //_is_MoveTo_StartPos = true;
        _NPC._is_Move_StartPos = true;
    }

    public void NPC_ThoughtHead()
    {
        //  1 = completed || 0 = !completed
        int _level_status = PlayerPrefs.GetInt("Level" + levelnum, 0);
        //Debug.Log("Level Status of Level " + levelnum + " = " + _level_status);

        if(_level_status == 0)
        {
            Maximize_Buttons();
            _NPC._ThoughtCloud.SetActive(true);
            _NPC._Thought_HeadArt.GetComponent<SpriteRenderer>().sprite = _NPC_HeadArt[levelnum - 1];
        }
        else if(_level_status == 1)
        {
            levelnum++;
            _Pass_Count++;

            if(levelnum > 23)
            {
                levelnum = 1;
            }

            if(_Pass_Count > 24)
            {
                _Pass_Count = 0;

                for(int i = 1; i <= _NPC_HeadArt.Length; i++)
                {
                    //  Sets status of all the levels to 0
                    PlayerPrefs.SetInt("Level" + levelnum, 0);
                    //Debug.Log("Status of Level " + levelnum + " = " + PlayerPrefs.GetInt("Level" + levelnum));
                }
            }

            NPC_ThoughtHead();
        }
    }

    public void NPC_Happy()
    {
        _NPC_Eyebrow.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
        _NPC_Head.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
        _NPC_Head.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, 100f);
        _NPC_Head.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, 0f);
        _NPC_Body.GetComponent<Animator>().SetTrigger("Happy");
    }

    public void NPC_Angry()
    {
        _NPC_Eyebrow.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100f);
        _NPC_Head.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
        _NPC_Head.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, 0f);
        _NPC_Head.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, 100f);
        _NPC_Body.GetComponent<Animator>().SetTrigger("Angry");
    }
    #endregion

    #region Hand Functions
    public void Hand_HoldHead()
    {
        _target_X_rot = 20f;

        _Hand_HoldHead.SetActive(true);
        _Hand_HoldHead.GetComponent<Animator>().SetTrigger("Hold");
        _MainHead.transform.parent = _Hand_HoldHead.transform;

        foreach(GameObject obj in _Objects_ToHide)
        {
            obj.SetActive(false);
        }
    }
    #endregion

    public void Minimize_Buttons()
    {
        _Accept_Button.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce);
        Reject_Button.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce);
    }

    public void Maximize_Buttons()
    {
        _Accept_Button.transform.DOScale(new Vector3(4f, 1.7f, 1f), 0.5f).SetEase(Ease.OutBounce);
        Reject_Button.transform.DOScale(new Vector3(4f, 1.7f, 1f), 0.5f).SetEase(Ease.OutBounce);
    }

    public void Hand_Cursor()
    {
        if (is_CreativeMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _audio.PlayOneShot(_Click_SFX);
            }

            _Hand_Cursor.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _Hand_Cursor.transform.position.z);
        }
    }

    public void Disable_ControlHint_Anim()
    {
        _ControlHint_Cursor.SetActive(false);
    }
}
