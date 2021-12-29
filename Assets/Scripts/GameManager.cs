using Assets.Scripts.Layer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Player player;

    public ParticleSystem explosion;

    public Canvas canvas;

    public int lives = 3;

    public float respawnTime = 3.0f;
    public float respawnInvulnerbilityTime = 3.0f;

    public int score = 0;

    public void AsteriodDestroyed(Asteriod asteriod) {
        this.explosion.transform.position = asteriod.transform.position;
        this.explosion.Play();

        
        if (asteriod.size < 0.75f) {
            score += 100;
        } else if (asteriod.size < 1.2f) {
            score += 50;
        } else {
            score += 25;
        }
        
    }

    public void PlayerDied() {

        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }


    private void Respawn() {
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerbilityTime);
    }

    private void TurnOnCollisions() {
        this.player.gameObject.layer = Layers.PlayerLayerId;
    }

    private void GameOver() {
        this.lives = 3;
        this.score = 0;
    }

}
