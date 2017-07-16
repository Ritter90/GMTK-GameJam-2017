using UnityEngine;

public class BaseKey: MonoBehaviour, IDangerous
{
    private bool dangerous;

    public AudioSource audioSource;
    public float volumeScale = 0.2f;
    public AudioClip openDoor;
    public AudioClip hitfloor;
    public AudioClip hitWall;
    public AudioClip hitPlayer;

    void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
    }

    public void SetDangerous()
    {
        dangerous = true;
    }

    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            if(coll.gameObject.tag == "Floor")
            {
                audioSource.PlayOneShot(hitfloor, volumeScale);
            }
            else if(coll.gameObject.tag == "Wall")
            {
                audioSource.PlayOneShot(hitfloor, volumeScale);
            }
            dangerous = false;
        }

        IOpenable openable = coll.gameObject.GetComponent<IOpenable>();
        IKillable killable = coll.gameObject.GetComponent<IKillable>();

        if (openable != null)
        {
            audioSource.PlayOneShot(openDoor, 0.5f);
            openable.Open();
            Destroy(gameObject);
        }

        if(killable != null && dangerous)
        {
            audioSource.PlayOneShot(hitPlayer, volumeScale);
            killable.Kill();
            Destroy(gameObject);
        }
    }
}
