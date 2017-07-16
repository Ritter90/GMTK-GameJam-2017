using UnityEngine;

public static class InputManager
{
    public static bool active; //Used to disable controls

    public static float GetHorizontalInput(Player.Character character)
    {
        if(active)
        {
            switch (character)
            {
                case Player.Character.SirYale:
                    return Input.GetAxis("Horizontal1");
                    break;
                case Player.Character.SirChubb:
                    return Input.GetAxis("Horizontal2");
                default:
                    return 0;
                    break;
            }
        }
        else
        {
            return 0;
        }
    }

    public static bool GetJumpInput(Player.Character character)
    {
        if (active)
        {
            switch (character)
            {
                case Player.Character.SirYale:
                    return Input.GetButtonDown("Jump1");
                    break;
                case Player.Character.SirChubb:
                    return Input.GetButtonDown("Jump2");
                    break;
                default:
                    return false;
                    break;
            }
        }
        else
        {
            return false;
        }
    }

    public static bool GetActionInput(Player.Character character)
    {
        if (active)
        {
            switch (character)
            {
                case Player.Character.SirYale:
                    return Input.GetButtonDown("Action1");
                    break;
                case Player.Character.SirChubb:
                    return Input.GetButtonDown("Action2");
                    break;
                default:
                    return false;
                    break;
            }
        }
        else
        {
            return false;
        }
    }
}
