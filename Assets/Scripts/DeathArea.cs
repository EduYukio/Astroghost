using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public Vector3 respawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            Player.respawnPosition = respawnPosition;
            player.health -= 1;
            // Destroy(player.PlayerUI.playerLivesImages[(int)player.health]);
            (player.PlayerUI.playerLivesImages[(int)player.health]).enabled = false;
            player.spriteRenderer.sprite = player.beingHitSprite;
            Manager.audio.Play("PlayerTakeHit");
            Invoke(nameof(TeleportPlayer), 0.5f);
        }
    }

    private void TeleportPlayer()
    {

        Player player = FindObjectOfType<Player>();
        player.spriteRenderer.sprite = player.normalSprite;
        player.transform.position = respawnPosition;
    }
}
