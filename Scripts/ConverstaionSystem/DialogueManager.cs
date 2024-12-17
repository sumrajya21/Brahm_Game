using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("DialogueUI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool dialoguePlaying { get; private set; }

    [Header("Choices")]
    public GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    private static DialogueManager instance;

    public GameObject cams;
    void Start()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialoguePlaying)
        {
            
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("A");
            ContinueStory();
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one dialogue manager"); // Pls stop >:[
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void StartDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        cams.SetActive(true);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            displayChoices();
        }

        else
        {
            ExitDialogueMode();
        }
    }

    private void displayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices. stahp");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void makeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        Debug.Log(choiceIndex);
    }

}
