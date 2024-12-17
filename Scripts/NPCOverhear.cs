using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOverhear : MonoBehaviour
{
    public string dialogue;
    public ScriptableObject ScriptableObject;
    // Start is called before the first frame update
    public GameObject overherUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overherUI.SetActive(true);
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overherUI.SetActive(false);
            
        }
    }
}
