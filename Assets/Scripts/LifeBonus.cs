using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player.health < 5)
            {
                player.PlayerUI.playerLivesImages[(int)player.health].enabled = true;
                player.health++;
            }

            Destroy(gameObject);
            Manager.audio.Play("UIButtonOk");
        }
    }
}
