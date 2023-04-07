using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu2Manager : MonoBehaviour
{
    [SerializeField] Animator[] anims;
    [SerializeField] GameObject mainMenuButtons;

    private void OnEnable()
    {
        foreach (Animator i in anims)
        {
            i.SetTrigger("isEcho");
        }

        Invoke(nameof(ButtonsEnabled), 2);
    }

    void ButtonsEnabled()
    {
        mainMenuButtons.SetActive(true);
    }
}
