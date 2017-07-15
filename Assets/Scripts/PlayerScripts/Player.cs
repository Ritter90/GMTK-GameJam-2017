using UnityEngine;

/// <summary>
/// Class for holding player data
/// </summary>
public class Player : MonoBehaviour, IKillable
{
    public int playerNumber;
    public Vector3 spawnLocation;

    public void Kill()
    {
        gameObject.GetComponent<PlayerActionManager>().DropPickup();
        transform.position = spawnLocation;
    }
}
