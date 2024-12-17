using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class pauseController : MonoBehaviour
{
    public GameObject PauseUI;
    private bool pauseActive;
    public GameObject cams;
    public VisualElement ui;
    public Button resume;
    public Button backMenu;
    // Start is called before the first frame update
    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        pauseActive = PauseUI.activeSelf;
    }

    public void OnPause()
    {
        pauseActive = PauseUI.activeSelf;
        Debug.Log(pauseActive);
        PauseUI.SetActive(!pauseActive);
        cams.SetActive(pauseActive);
        Time.timeScale = 1 - Time.timeScale;
        //pauseActive = !pauseActive;

    }
    private void OnEnable()
    {
        resume = ui.Q<Button>("Resume");
        backMenu = ui.Q<Button>("BackMenu");
        resume.clicked += OnPause;
        backMenu.clicked += BackMenu_clicked;
    }

    private void OnDisable()
    {
        cams.SetActive(true);
    }

    private void BackMenu_clicked()
    {
        SceneManager.LoadScene("LevelOne");
        Time.timeScale = 1 - Time.timeScale;
    }
}
