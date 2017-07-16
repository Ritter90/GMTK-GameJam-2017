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
}
