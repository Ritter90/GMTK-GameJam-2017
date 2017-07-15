using UnityEngine;

/// <summary>
/// Class for holding player data
/// </summary>
public class Player : MonoBehaviour, IKillable, IHittable
{
    public int playerNumber;
    public Vector3 spawnLocation;

    public void Kill()
    {
        gameObject.GetComponent<PlayerActionManager>().DropPickup();
        transform.position = spawnLocation;
    }

    public void Hit(Vector2 hitForce)
    {
        gameObject.GetComponent<PlayerActionManager>().DropPickup();
        gameObject.GetComponent<Rigidbody2D>().AddForce(hitForce);
    }
}
