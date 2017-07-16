using UnityEngine.UI;

public class MainMenuScreenController : BaseScreenController
{
    public Button playButton;
    public Button controlsButton;
    public Button creditsButton;
    public Button quitButton;

    protected override void Awake()
    {
        base.Awake();

        NavigationController.MainMenu = this;

        playButton.onClick.AddListener(PlayButtonClicked);
        controlsButton.onClick.AddListener(ControlsButtonClicked);
        creditsButton.onClick.AddListener(CreditsButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        
        gameObject.SetActive(false);
    }

    private void PlayButtonClicked()
    {
        NavigationController.PlayButtonClicked();
    }

    private void ControlsButtonClicked()
    {
        NavigationController.ControlsButtonClicked();
    }

    private void CreditsButtonClicked()
    {
        NavigationController.CreditsButtonClicked();
    }

    private void QuitButtonClicked()
    {
        NavigationController.QuitApplication();
    }
}
