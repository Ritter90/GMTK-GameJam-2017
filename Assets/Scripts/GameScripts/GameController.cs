using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player SirYale;

    [SerializeField]
    private Player SirChubb;

    [SerializeField]
    private AxeKey currentKey;

    public Arena[] arenaData;
    public Exit[] exits;
    public int currentArena;
    private Transform keySpawner;
    public GameObject keyPrefab;
    public float keyRespawnTime = 1f;

    public GameObject gameArea;
    

    public float arenaTransitionTime = 2f;
    public bool transistioning = false;

    void Awake()
    {
        currentArena = 2;

        foreach (Exit exit in exits)
        {
            exit.gameController = this;
        }
        InputManager.active = true;
        SetPlayerSpawnLocations(currentArena);
        SetKeySpawnLocation(currentArena);
        SpawnKey();
    }

    void Update()
    {
        if(currentKey == null)
        {
            StartCoroutine(SpawnKeyAfterSeconds(keyRespawnTime));
        }
    }

    public void ArenaWonBy(Player.Character character)
    {
        switch (character)  
        {
            case Player.Character.SirYale:
                ++currentArena;
                break;
            case Player.Character.SirChubb:
                --currentArena;
                break;
            default:
                break;
        }

        MoveToArena(currentArena);
    }

    private void MoveToArena(int arenaNumber)
    {
        transistioning = true;
        InputManager.active = false;
        SetPlayerSpawnLocations(arenaNumber);
        SetKeySpawnLocation(arenaNumber);
        ResetPlayers();
        arenaData[arenaNumber].ResetDoors();

        StartCoroutine(TransitionToArena(arenaNumber));

    }

    private void ResetPlayers()
    {
        SirChubb.Kill();
        SirYale.Kill();
    }

    private void SetPlayerSpawnLocations(int arenaNumber)
    {
        SirChubb.spawnLocation = arenaData[arenaNumber].chubbSpawnLocation;
        SirYale.spawnLocation = arenaData[arenaNumber].yaleSpawnLocation;
    }

    private void SetKeySpawnLocation(int arenaNumber)
    {
        keySpawner = arenaData[arenaNumber].keySpawner;
    }

    IEnumerator TransitionToArena(int arenaNumber)
    {
        float startTime = Time.time;
        Vector3 startPosition = gameArea.transform.position;
        Vector3 aimPosition = startPosition - arenaData[arenaNumber].Center;

        while(Time.time-startTime < arenaTransitionTime)
        {
            gameArea.transform.position = Vector3.Lerp(startPosition, aimPosition, (Time.time - startTime) / arenaTransitionTime);
            yield return null;
        }
        gameArea.transform.position = aimPosition;
        SpawnKey();
        InputManager.active = true;
        transistioning = false;
    }

    IEnumerator SpawnKeyAfterSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnKey();
    }

    private void SpawnKey()
    {
        if(currentKey != null)
        {
            Destroy(currentKey.gameObject);
        }
        GameObject newKey = Instantiate(keyPrefab, keySpawner.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        newKey.transform.parent = gameArea.transform;
        currentKey = newKey.GetComponent<AxeKey>();
    }
}
