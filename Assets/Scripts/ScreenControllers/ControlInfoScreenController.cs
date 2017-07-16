using UnityEngine.UI;

public class ControlInfoScreenController : BaseScreenController
{
    public Button backButton;

    protected override void Awake()
    {
        base.Awake();

        NavigationController.Controls = this;

        backButton.onClick.AddListener(backButtonClicked);

        gameObject.SetActive(false);
    }

    private void backButtonClicked()
    {
        NavigationController.LeaveControlInfoScreen();
    }
}
