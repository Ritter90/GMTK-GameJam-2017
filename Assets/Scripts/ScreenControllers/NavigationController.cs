using UnityEngine;

public static class NavigationController
{
    private static MainMenuScreenController mainMenu;
    private static CreditsScreenController credits;
    private static IntroScrollerController isc;


    public static MainMenuScreenController MainMenu
    {
        set { mainMenu = value; }
    }

    public static CreditsScreenController Credits
    {
        set { credits = value; }
    }

    public static IntroScrollerController ISC
    {
        set { isc = value;}
    }

    public static void PlayButtonClicked()
    {

    }

    public static void CreditsButtonClicked()
    {
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }

    public static void LeaveCreditsScreen()
    {
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public static void LeaveIntroScroller()
    {
        isc.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public static void QuitApplication()
    {
        Application.Quit();
    }

}
