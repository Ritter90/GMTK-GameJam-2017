using UnityEngine;

/// <summary>
/// Class for holding player data
/// </summary>
public class Player : MonoBehaviour, IKillable, IHittable
{
    public enum Character
    {
        SirYale, SirChubb
    }

    public Character character;
    public Transform spawnLocation;

    public AudioSource audioSource;

    public AudioClip deathSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Kill()
    {
        ResetToSpawn();
        audioSource.PlayOneShot(deathSound);
    }

    public void ResetToSpawn()
    {
        gameObject.GetComponent<PlayerActionManager>().DropPickup();
        transform.position = spawnLocation.position;
    }

    public void Hit(Vector2 hitForce)
    {
        gameObject.GetComponent<PlayerActionManager>().DropPickup();
        gameObject.GetComponent<Rigidbody2D>().AddForce(hitForce);
    }
}
