using UnityEngine;

public class DoorController : MonoBehaviour, IOpenable
{
    public void Open()
    {
        gameObject.SetActive(false);
    }
}
