using UnityEngine;

/// <summary>
/// Controller for doors implementing the IOpenable interface
/// </summary>
public class DoorController : MonoBehaviour, IOpenable
{
    public void Open()
    {
        gameObject.SetActive(false);
    }
}
