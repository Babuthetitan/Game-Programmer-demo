using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    bool cameraLeft;
    float timer = 1;

    [SerializeField] CinemachineVirtualCamera vcam1; //Left Cam
    [SerializeField] CinemachineVirtualCamera vcam2; //Right Cam

    private void Start()
    {
        SwitchCameras();
        SwitchCameras();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        else
        {
            timer = 21;
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        if (cameraLeft)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }

        if (!cameraLeft)
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }

        cameraLeft = !cameraLeft;
        timer = 21;
    }
}
