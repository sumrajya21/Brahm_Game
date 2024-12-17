using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dialogueUI;
    public GameObject player;
    public GameObject cam;
    public GameObject HUD;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            page();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void page()
    {
        dialogueUI.SetActive(true);
        player.SetActive(false);
        cam.SetActive(false);
        HUD.SetActive(false);
    }
}
