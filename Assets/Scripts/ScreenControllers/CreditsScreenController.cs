

using System;
using UnityEngine.UI;

public class CreditsScreenController : BaseScreenController
{
    public Button backButton;

    protected override void Awake()
    {
        base.Awake();

        NavigationController.Credits = this;

        backButton.onClick.AddListener(backButtonClicked);

        gameObject.SetActive(false);
    }

    private void backButtonClicked()
    {
        NavigationController.LeaveCreditsScreen();
    }
}
