using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene("Asteroids");
    }

    public void GotoMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
