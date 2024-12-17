using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AfterIntro : MonoBehaviour
{
    // Start is called before the first frame update

    public VideoPlayer player;

    void OnFinish(VideoPlayer vp)
    {
        SceneManager.LoadScene("LevelOne");
    }

    void Start()
    {
        player.loopPointReached += OnFinish;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            OnFinish(player);
        }
    }
}
