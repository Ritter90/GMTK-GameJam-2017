using UnityEngine;

public static class InputManager
{
    public static float GetHorizontalInput(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return Input.GetAxis("Horizontal1");
                break;
            case 2:
                return Input.GetAxis("Horizontal2");
            default:
                return 0;
                break;
        }
    }

    public static bool GetJumpInput(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return Input.GetButtonDown("Jump1");
                break;
            case 2:
                return Input.GetButtonDown("Jump2");
                break;
            default:
                return false;
                break;
        }
    }

    public static bool GetActionInput(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return Input.GetButtonDown("Action1");
                break;
            case 2:
                return Input.GetButtonDown("Action2");
                break;
            default:
                return false;
                break;
        }
    }
}
