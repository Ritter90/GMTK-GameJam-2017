using UnityEngine;

/// <summary>
/// Class For Holding Arena Data
/// </summary>
public class Arena : MonoBehaviour
{
    public Vector3 Center
    {
        get { return gameObject.transform.position; }
    }

    public Transform yaleSpawnLocation;
    public Transform chubbSpawnLocation;
    public Transform keySpawner;

    public GameObject[] doors;

    public void ResetDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }
}
