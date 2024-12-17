using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Slider = UnityEngine.UIElements.Slider;
using Toggle = UnityEngine.UIElements.Toggle;

public class MainMenuController : MonoBehaviour
{

    public VisualElement ui;
    public Button newGame;
    public Button loadGame;
    public Button settings;
    public Button exit;
    public VisualElement menu;
    public VisualElement settingsMenu;
    public Slider sfx;
    public Slider music;
    public Slider master;
    public Button customizeControls;
    public Toggle InvertX;
    public Toggle InvertY;
    public Slider MouseSense;

    public cameraController cameraController;

    public GameObject player;
    public GameObject cam;
    public GameObject HUD;

    private bool InvX = false;
    private bool InvY = true;

    public AudioSource MasterMusic;

    private float mSense = 100;
    private float mMaster = 100;

    



    public void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        cameraController = GameObject.FindWithTag("GameCam").GetComponent<cameraController>();
        player.SetActive(false);
        cam.SetActive(false);
        HUD.SetActive(false);

    }

    private void OnEnable()
    {
        newGame = ui.Q<Button>("NewGame");
        newGame.clicked += newGameClicked;
        loadGame = ui.Q<Button>("LoadGame");
        loadGame.clicked += loadGameClicked;
        settings = ui.Q<Button>("Settings");
        settings.clicked += settingsClicked;
        exit = ui.Q<Button>("Exit");
        exit.clicked += exitClicked;
        menu = ui.Q<VisualElement>("Menu");
        settingsMenu = ui.Q<VisualElement>("SettingsMenu");
        sfx = ui.Q<Slider>("SFX");
        music = ui.Q<Slider>("Music");
        master = ui.Q<Slider>("Master");
        master.RegisterValueChangedCallback(OnMusicMain);
        customizeControls = ui.Q<Button>("CustomizeControls");
        customizeControls.clicked += back2menu;
        InvertX = ui.Q<Toggle>("InvertX");
        InvertY = ui.Q<Toggle>("InvertY");
        InvertX.value = InvX;
        InvertY.value = InvY;
        InvertX.RegisterValueChangedCallback(OnToggleX);
        InvertY.RegisterValueChangedCallback(OnToggleY);
        MouseSense = ui.Q<Slider>("MouseSensitivity");
        MouseSense.RegisterValueChangedCallback(OnMouseSense);
        MasterMusic = GetComponent<AudioSource>();
        


    }
    

    private void newGameClicked()
    {
        gameObject.SetActive(false);
        player.SetActive(true);
        cam.SetActive(true);
        HUD.SetActive(true);
        cameraController.mouseSense(mSense);
        cameraController.invertX(InvX);
        cameraController.invertY(InvY);
        
    }

    private void loadGameClicked()
    {
        Debug.Log("Aditya has not gotten around to coding that functionality");
    }

    private void settingsClicked()
    {
        Debug.Log("Have fun with this one ;)");
        menu.transform.position += new Vector3(1920, 0, 0);
        settingsMenu.transform.position -= new Vector3(1920, 0, 0);
    }

    private void exitClicked()
    {
        Application.Quit();
        
    }

    private void back2menu()
    {
        settingsMenu.transform.position += new Vector3(1920, 0, 0);
        menu.transform.position -= new Vector3(1920, 0, 0);
    }

    private void OnToggleX(ChangeEvent<bool> evt)
    {
        //cameraController.invertX(evt.newValue);
        InvX = evt.newValue;
    }

    private void OnToggleY(ChangeEvent<bool> evt)
    {
        //cameraController.invertY(evt.newValue);
        InvY = evt.newValue;
    }

    private void OnMouseSense(ChangeEvent<float> evt)
    {
        mSense = evt.newValue;
        //Debug.Log(evt.newValue);
    }

    private void OnMusicMain(ChangeEvent<float> evt)
    {
        mMaster = evt.newValue/100;
        Debug.Log(mMaster);
        //MasterMusic.Stop();
        MasterMusic.volume = mMaster;

    }

    

    private void Update()
    {
        //AudioSource.volume = mMaster * 2;
    }

}