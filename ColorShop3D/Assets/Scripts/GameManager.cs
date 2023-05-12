using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using Tabtale.TTPlugins;

public class GameManager : MonoBehaviour
{
    #region Script References
    [Header("Script References")]
    private MasterStorage _masterStorage;
    private ColorMain _colorMain;
    private LoadingScreenManager _loadingscreenmanager;
    #endregion

    #region UI References
    [Header("UI References")]
    [HideInInspector]
    public GameObject StartUI;
    [HideInInspector]
    public GameObject IngameUI;
    [HideInInspector]
    public GameObject ResultUI;
    public GameObject _Tutorial_BTN;
    #endregion

    #region Heterogenous Values
    public bool _is_Tutorial_Level = false;
    #endregion

    #region Clik Values
    private Dictionary<string, object> _parameter_Dictionary = new Dictionary<string, object>();
    #endregion


    private void Awake()
    {
        // Initialize CLIK Plugin
        TTPCore.Setup();

        _masterStorage = FindObjectOfType<MasterStorage>();
        _colorMain = FindObjectOfType<ColorMain>();
        _loadingscreenmanager = FindObjectOfType<LoadingScreenManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadUI();
        _masterStorage.Spawn_NPC();
        //SetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        _masterStorage.Hand_Cursor();
    }

    private void SetLevel()
    {
        #region Sets characters according to level number
        if (!_masterStorage.is_CreativeMode)
            {
                switch (_masterStorage.levelnum)
                {
                    case 0:
                        _masterStorage._character = MasterStorage.Character.Mandalorian;
                        break;

                    case 1:
                        _masterStorage._character = MasterStorage.Character.Batman;
                        break;

                    case 2:
                        _masterStorage._character = MasterStorage.Character.Spiderman;
                        break;

                    case 3:
                        _masterStorage._character = MasterStorage.Character.Deadpool;
                        break;

                    case 4:
                        _masterStorage._character = MasterStorage.Character.Flash;
                        break;

                    case 5:
                        _masterStorage._character = MasterStorage.Character.Squid;
                        break;

                    case 6:
                        _masterStorage._character = MasterStorage.Character.Wolverine;
                        break;

                    case 7:
                        _masterStorage._character = MasterStorage.Character.Robin;
                        break;

                    case 8:
                        _masterStorage._character = MasterStorage.Character.Buzz;
                        break;

                    case 9:
                        _masterStorage._character = MasterStorage.Character.America;
                        break;

                    case 10:
                        _masterStorage._character = MasterStorage.Character.Vision;
                        break;

                    case 11:
                        _masterStorage._character = MasterStorage.Character.Baymax;
                        break;

                    case 12:
                        _masterStorage._character = MasterStorage.Character.Imperial;
                        break;

                    case 13:
                        _masterStorage._character = MasterStorage.Character.Antman;
                        break;

                    case 14:
                        _masterStorage._character = MasterStorage.Character.Chief;
                        break;

                    case 15:
                        _masterStorage._character = MasterStorage.Character.Doraemon;
                        break;

                    case 16:
                        _masterStorage._character = MasterStorage.Character.Redhood;
                        break;

                    case 17:
                        _masterStorage._character = MasterStorage.Character.Robocop;
                        break;

                    case 18:
                        _masterStorage._character = MasterStorage.Character.Lantern;
                        break;

                    case 19:
                        _masterStorage._character = MasterStorage.Character.Magneto;
                        break;

                    case 20:
                        _masterStorage._character = MasterStorage.Character.Mandalorian;
                        break;

                    case 21:
                        _masterStorage._character = MasterStorage.Character.PeaceMaker;
                        break;

                    case 22:
                        _masterStorage._character = MasterStorage.Character.Cyclops;
                        break;

                    case 23:
                        _masterStorage._character = MasterStorage.Character.Dredd;
                        break;

                    case 24:
                        _masterStorage._character = MasterStorage.Character.Invisible;
                        break;

                    case 25:
                        _masterStorage._character = MasterStorage.Character.Deathstroke;
                        break;
                }
            }
        #endregion

        #region Clik Event : Mission Started
        _parameter_Dictionary.Add("missionName", _masterStorage._character.ToString());
        TTPGameProgression.FirebaseEvents.MissionStarted(_masterStorage.levelnum, _parameter_Dictionary);
        #endregion

        _masterStorage.Sort_According_To_Character();
            _masterStorage.Assign_Head_To_Body();
    }

    public void StartGame()
    {
        _masterStorage._is_Game_Started = true;
        _masterStorage.is_Coloring = true;

        _masterStorage.Initiate_Camera_Setup();

        StartUI.SetActive(false);
        IngameUI.SetActive(true);

        StartCoroutine(_masterStorage.Delayed_Function_Call(_colorMain.Automate_Mask_Process, 1.5f));
    }

    public void ResultProceed()
    {
        if (!_masterStorage.Hand_ApplyMask.activeSelf)
        {
            if (!_masterStorage.is_Coloring)
            {
                IngameUI.SetActive(false);
                StartCoroutine(ProcessResult());
            }
        }
    }

    private IEnumerator ProcessResult()
    {
        _masterStorage.Hand_ApplyMask.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        _colorMain.Disable_AllMasks();
        yield return new WaitForSeconds(0.8f);
        _masterStorage.Hand_ApplyMask.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        _masterStorage.CalculateResult();
        yield return new WaitForSeconds(0.8f);
        _masterStorage.Initiate_Camera_Rotation();
        //_masterStorage._Body.GetComponent<Animator>().SetTrigger("Think");
        //yield return new WaitForSeconds(0.8f);
        ResultUI.SetActive(true);
        //_masterStorage.CalculateResult();
        yield return new WaitForSeconds(1.5f);
        //_masterStorage.is_calculateResult = true;
        //yield return new WaitForSeconds(2f);
        _masterStorage.ActivateResultButtons();
    }

    public void NextLevel()
    {
        if (_is_Tutorial_Level)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            //_masterStorage.levelnum += 1;
            //PlayerPrefs.SetInt("Level Number", _masterStorage.levelnum);

            TTPGameProgression.FirebaseEvents.MissionComplete(_parameter_Dictionary);

            _masterStorage._audio.PlayOneShot(_masterStorage._Click_SFX);
            PlayerPrefs.SetInt("Level" + _masterStorage.levelnum, 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //  Executes the tutorial
    public void Execute_Tutorial_Level()
    {
        if (_masterStorage._Tutorial_Counter == 0)  //  Tap to Zoom
        {
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[11], null, 1f));
            _masterStorage._Tutorial_Counter++;
        }
        else if (_masterStorage._Tutorial_Counter == 1) //  Tap to Close
        {
            _masterStorage._Tutorial_Icon[11].SetActive(false);
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[12], null, 0.3f));
            _masterStorage._Tutorial_Counter++;
        }
        else if (_masterStorage._Tutorial_Counter == 2) //  Tap to Show Hints
        {
            _masterStorage._Tutorial_Icon[12].SetActive(false);
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[13], null, 0.3f));
            _masterStorage._Tutorial_Counter++;
        }
        else if (_masterStorage._Tutorial_Counter == 3) //  Tap to Color
        {
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[0], _masterStorage._Tutorial_Icon[1], 0.3f));
            _masterStorage._is_ExecuteTutorialStep = true;
            _masterStorage._Tutorial_Counter++;
        }
        else if(_masterStorage._Tutorial_Counter == 4)  //  Tap to Cover
        {
            _masterStorage._Tutorial_Icon[0].SetActive(false);
            _masterStorage._Tutorial_Icon[1].SetActive(false);
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[2], _masterStorage._Tutorial_Icon[3], 2.5f));
            _masterStorage._Tutorial_Counter++;
        }
        else if (_masterStorage._Tutorial_Counter == 5) //  Tap to Color
        {
            _masterStorage._Tutorial_Icon[2].SetActive(false);
            _masterStorage._Tutorial_Icon[3].SetActive(false);
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[4], _masterStorage._Tutorial_Icon[5], 1.5f));
            _masterStorage._Tutorial_Counter++;
        }
        else if (_masterStorage._Tutorial_Counter == 6) //  Tap to Uncover
        {
            _masterStorage._Tutorial_Icon[4].SetActive(false);
            _masterStorage._Tutorial_Icon[5].SetActive(false);
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[6], _masterStorage._Tutorial_Icon[7], 2.5f));
            _masterStorage._Tutorial_Counter++;
        }
        else if (_masterStorage._Tutorial_Counter == 7) //  Tap to Finish
        {
            _masterStorage._Tutorial_Icon[6].SetActive(false);
            _masterStorage._Tutorial_Icon[7].SetActive(false);
            StartCoroutine(Tutorial(_masterStorage._Tutorial_Icon[8], _masterStorage._Tutorial_Icon[9], 1.5f));
            _masterStorage._Tutorial_Counter++;
        }
    }

    private IEnumerator Tutorial(GameObject obj1, GameObject obj2, float delay)
    {
        yield return new WaitForSeconds(delay);

        if(obj1 != null)
        {
            obj1.SetActive(true);
        }

        if(obj2 != null)
        {
            obj2.SetActive(true);
        }
    }

    private void LoadUI()
    {
        _masterStorage._LoadUI.SetActive(true);

        _loadingscreenmanager.RevealLoadingScreen();

        float _load_time = 3f;// Random.Range(1f, 3f);
        StartCoroutine(Deactivate_LoadUI(_load_time));
    }

    private IEnumerator Deactivate_LoadUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        _masterStorage._LoadUI.SetActive(false);

        if (_is_Tutorial_Level)
        {
            _masterStorage._is_Game_Started = true;

            StartUI.SetActive(false);
            IngameUI.SetActive(true);

            //_masterStorage._Body.GetComponent<Animator>().SetTrigger("Sit");

            Execute_Tutorial_Level();
        }

        /*if (_masterStorage.levelnum > 0)
        {
            _Tutorial_BTN.SetActive(true);
        }*/
    }

    //  Mutes/ Unmutes audio
    public void ManageAudio()
    {
        if (_masterStorage._audio.mute)
        {
            _masterStorage._AudioButton.GetComponent<Image>().sprite = _masterStorage._sound_icon[0];
        }
        else
        {
            _masterStorage._AudioButton.GetComponent<Image>().sprite = _masterStorage._sound_icon[1];
        }

        _masterStorage._audio.mute = !_masterStorage._audio.mute;
    }

    public void Tutorial_Level()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Close_UI_Objective_Holder()
    {
        _masterStorage._UI_Objective_Holder.SetActive(false);

        if(_masterStorage.levelnum == 0)
        {
            if (_masterStorage._Tutorial_Counter == 2)
            {
                Execute_Tutorial_Level();
            }
        }
    }

    //  Called by Accept Button
    public void Accept_Task()
    {
        SetLevel();
        StartGame();
        _masterStorage._NPC._ThoughtCloud.SetActive(false);
        StartCoroutine(_masterStorage.Delayed_Function_Call(_masterStorage.Disable_ControlHint_Anim, 5f));
    }

    public void Reject_Task()
    {
        _masterStorage.Minimize_Buttons();
        _masterStorage._NPC._ThoughtCloud.SetActive(false);

        _masterStorage.levelnum++;
        if (_masterStorage.levelnum > 24)
        {
            _masterStorage.levelnum = 1;
        }

        _masterStorage._NPC._is_Move_Away = true;
        _masterStorage._NPC.Set_Rotation();

        StartCoroutine(_masterStorage.Delayed_Function_Call(_masterStorage.Spawn_NPC, 1f));
    }

    public void Clik_LevelFailed()
    {
        TTPGameProgression.FirebaseEvents.MissionFailed(_parameter_Dictionary);
    }
}
