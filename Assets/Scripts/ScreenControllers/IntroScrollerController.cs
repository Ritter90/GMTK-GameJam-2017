using UnityEngine.UI;

public class IntroScrollerController : BaseScreenController {


	public Button backButton;

	protected override void Awake()
	{

		base.Awake();

		NavigationController.ISC = this;

		backButton.onClick.AddListener(BackButtonClicked);

	}

	private void BackButtonClicked()
	{
		NavigationController.LeaveIntroScroller();
	}


}
