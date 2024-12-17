using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PageMenu : MonoBehaviour
{
    public Button option1;
    public Button option2;
    public Button option3;
    public GameObject pageMenu;
    public Label MessageBox;

    public GameObject player;
    public GameObject cam;
    public GameObject HUD;
    public GameObject page;

    public string Question;
    public string o1;
    public string o2;
    public string o3;

    public int CorrectOption;

    private VisualElement ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MessageBox = ui.Q<Label>("MessageBox");
        option1 = ui.Q<Button>("Option1");
        option2 = ui.Q<Button>("Option2");
        option3 = ui.Q<Button>("Option3");
        option1.text = o1;
        option2.text = o2;
        option3.text = o3;
        MessageBox.text = Question;
        option1.clicked += Option1Clicked;
        option2.clicked += Option2Clicked;
        option3.clicked += Option3Clicked;

    }

    private void endPage()
    {
        pageMenu.SetActive(false);
        player.SetActive(true);
        cam.SetActive(true);
        HUD.SetActive(true);
        Destroy(page);
    }
    

    private void Option1Clicked()
    {
        if (CorrectOption == 3)
        {
            Debug.Log("YAY");
        }
        endPage();
        
    }

    private void Option2Clicked()
    {
        if (CorrectOption == 2)
        {
            Debug.Log("YAY");
        }
        endPage();
    }

    private void Option3Clicked()
    {
        if (CorrectOption == 1)
        {
            Debug.Log("Yay");
        }
        endPage();
    }
}
