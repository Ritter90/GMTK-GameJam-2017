using System;

public static class NavigationController
{
    private static MainMenuScreenController mainMenu;
    private static CreditsScreenController credits;

    public static MainMenuScreenController MainMenu
    {
        set { mainMenu = value; }
    }

    public static CreditsScreenController Credits
    {
        set { credits = value; }
    }

    public static void PlayButtonClicked()
    {

    }

    internal static void CreditsButtonClicked()
    {
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }

    internal static void LeaveCreditsScreen()
    {
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
