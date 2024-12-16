using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Overhear : MonoBehaviour
{
    public Label Label;
    public string dialogue;
    private VisualElement VisualElement;
    // Start is called before the first frame update
    void Start()
    {
        Label.text = dialogue;
    }
    private void Awake()
    {
        VisualElement = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        Label = VisualElement.Q<Label>("NPCChat");
        Label.text = dialogue;

    }
    private void OnDisable()
    {
        Label.text = dialogue;
    }

    // Update is called once per frame
    void Update()
    {
        Label.text = dialogue;
    }
}
