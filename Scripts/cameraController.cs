using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{

    private CinemachineFreeLook cam;
    // Start is called before the first frame update


    private void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }
    void Awake()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

    public void invertX(bool evt)
    {
        Debug.Log("X inverted");
        cam.m_XAxis.m_InvertInput = evt;
        
    }

    public void invertY(bool evt)
    {
        Debug.Log("Y inverted");
        Debug.Log(cam.m_YAxis.m_InvertInput);
        cam.m_YAxis.m_InvertInput = evt;
    }

    public void mouseSense(float value)
    {
        cam.m_YAxis.m_MaxSpeed *= value * 3 / 100;
        cam.m_XAxis.m_MaxSpeed *= value * 3 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
