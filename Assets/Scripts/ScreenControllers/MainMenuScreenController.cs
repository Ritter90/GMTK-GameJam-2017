﻿using UnityEngine.UI;

public class MainMenuScreenController : BaseScreenController
{
    public Button playButton;
    public Button creditsButton;

    protected override void Awake()
    {
        base.Awake();

        NavigationController.MainMenu = this;

        playButton.onClick.AddListener(PlayButtonClicked);
        creditsButton.onClick.AddListener(CreditsButtonClicked);
    }

    private void PlayButtonClicked()
    {
        NavigationController.PlayButtonClicked();
    }

    private void CreditsButtonClicked()
    {
        NavigationController.CreditsButtonClicked();
    }
}
