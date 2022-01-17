using Assets.Scripts.Layer;
using Assets.Scripts.Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Player player;

    public ParticleSystem explosion;

    public int lives = 3;

    public float respawnTime = 3.0f;
    public float respawnInvulnerbilityTime = 3.0f;

    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private AsteriodSpawner asteriodSpawner;
    [SerializeField] private Text livesText;
    [SerializeField] private Text immortalityText;

    private void Start() {
        this.immortalityText.gameObject.SetActive(false);
    }

    public void AsteriodDestroyed(Asteriod asteriod) {
        this.explosion.transform.localPosition = asteriod.transform.localPosition;
        this.explosion.Play();
    }

    public void PlayerDied() {

        this.explosion.transform.localPosition = this.player.transform.localPosition;
        this.explosion.Play();
        this.lives--;
        updateLivesText();

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn() {
        this.player.gameObject.layer = Layers.IgnoreCollisionsLayerId;
        this.player.transform.localPosition = Vector3.zero;
        this.player.gameObject.SetActive(true);
        this.immortalityText.gameObject.SetActive(true);
        this.immortalityText.text = $"Immortal for {this.respawnInvulnerbilityTime} sec";
        Invoke(nameof(TurnOnCollisions), respawnInvulnerbilityTime);
    }

    private void TurnOnCollisions() {
        this.player.gameObject.layer = Layers.PlayerLayerId;
        this.immortalityText.gameObject.SetActive(false);
    }

    private void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    private void updateLivesText() {
        this.livesText.text = $"{lives} Lives";
    }

}
