using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Score {
    public class ScoreManager : MonoBehaviour {
        [SerializeField] private Text scoreText;
        [SerializeField] private int score = 0;

        private void Start() {
            resetScore();
        }

        public void addPoints(int points) {
            updateScore(score + points);
        }

        // same type bonus?
        private void updateScore(int newValue) {
            this.score = newValue;
            this.scoreText.text = $"{this.score} Points";
        }

        public void resetScore() {
            updateScore(0);
        }
    }
}