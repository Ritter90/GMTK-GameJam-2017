using UnityEngine;
using UnityEngine.UI;

public class IntroScrollerController : BaseScreenController
{
	public Button backButton;

    public RectTransform scrollPanel;

    public float scrollDuration;
    private float scrollStartTime;

    private float scrollStartPos;
    private float scrollEndPos;


	protected override void Awake()
	{
		base.Awake();

		NavigationController.ISC = this;

		backButton.onClick.AddListener(BackButtonClicked);

        SetupScrollVariables();
	}

    void Start()
    {
        scrollStartTime = Time.time;
    }

    void OnEnable()
    {
        scrollStartTime = Time.time;
    }

	private void BackButtonClicked()
	{
		NavigationController.LeaveIntroScroller();
	}

    void Update()
    {
        if(Time.time - scrollStartTime < scrollDuration)
        {
            float yPos = Mathf.Lerp(scrollStartPos, scrollEndPos, (Time.time - scrollStartTime) / scrollDuration);

            scrollPanel.localPosition = new Vector2(scrollPanel.localPosition.x, yPos);
        }
    }

    private void SetupScrollVariables()
    {
        int screenHeight = Screen.height;
        scrollStartPos = -screenHeight * 1.2f;
        scrollEndPos = screenHeight * 1.2f;
    }
}
