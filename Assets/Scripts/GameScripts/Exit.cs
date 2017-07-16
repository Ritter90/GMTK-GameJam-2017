using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Player.Character characterExit;
    public GameController gameController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameController.transistioning == false)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (player.character == characterExit)
                {
                    gameController.ArenaWonBy(characterExit);
                }
                else
                {
                    player.Kill();
                }
            }
        }
    }
}
