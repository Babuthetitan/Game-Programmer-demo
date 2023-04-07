using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu1Manager : MonoBehaviour
{
    [SerializeField] InputAction anyInput;
    [SerializeField] MainMenu2Manager menu2;
    [SerializeField] AudioSource ClickSoundEffect;
    [SerializeField] AudioSource MainMenuSoundEffect;

    private void OnEnable()
    {
        anyInput.Enable();
    }

    private void OnDisable()
    {
        anyInput.Disable();
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        anyInput.performed += _ => AnyKeyPressed();
    }

    private void AnyKeyPressed()
    {
        menu2.enabled = true;
        this.enabled = false;

        //play click sound effect on any key pressed
        ClickSoundEffect.Play();
        MainMenuSoundEffect.Play();
    }
}
