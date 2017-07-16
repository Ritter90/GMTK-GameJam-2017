using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private IEnumerator spawnCoroutine;
    public GameObject keyPrefab;
    public float keyRespawnTime = 1f;

    public GameObject gameArea;
    
    public float arenaTransitionTime = 2f;
    public bool transistioning = false;

    private bool isFinished = false;
    public Text winText;

    void Awake()
    {
        currentArena = 2;
        winText.text = "";
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
        if(transistioning == false)
        {
            if (currentKey == null)
            {
                spawnCoroutine = SpawnKeyAfterSeconds(keyRespawnTime);
                StartCoroutine(spawnCoroutine);
            }
        } 
        
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(1);
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
        if(currentArena == 0 || currentArena == arenaData.Length - 1)
        {
            isFinished = true;
        }
        MoveToArena(currentArena);
    }

    private void MoveToArena(int arenaNumber)
    {
        if (currentKey != null)
        {
            Destroy(currentKey.gameObject);
        }
        transistioning = true;
        InputManager.active = false;
        SetPlayerSpawnLocations(arenaNumber);
        SetKeySpawnLocation(arenaNumber);
        ResetPlayers();
        arenaData[arenaNumber].ResetDoors();
        StopAllCoroutines();
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
        if(isFinished)
        {
            FinishGame();
        }
        else
        {
            InputManager.active = true;
        }
        
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

    private void FinishGame()
    {
        if(currentArena == 0)
        {
            winText.text = "All Hail King Chubb";
        }
        else
        {
            winText.text = "All Hail King Yale";
        }
    }
}
