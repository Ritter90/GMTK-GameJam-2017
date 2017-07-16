using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player SirYale;

    [SerializeField]
    private Player SirChubb;

    public Arena[] arenaData;
    public Exit[] exits;
    private int currentArena;

    public GameObject gameArea;

    public float arenaTransitionTime = 2f;

    void Awake()
    {
        currentArena = 2;

        foreach (Exit exit in exits)
        {
            exit.gameController = this;
        }
        InputManager.active = true;
        SetPlayerSpawnLocations(currentArena);
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
        InputManager.active = false;
        SetPlayerSpawnLocations(arenaNumber);
        ResetPlayers();
        SetExitsActive(false);

        StartCoroutine(TransitionToArena(arenaNumber));

    }

    private void SetExitsActive(bool active)
    {
        foreach (Exit exit in exits)
        {
            exit.gameObject.SetActive(active);
        }
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
        SetExitsActive(true);
        InputManager.active = true;
    }

}
