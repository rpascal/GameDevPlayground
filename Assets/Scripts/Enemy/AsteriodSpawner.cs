using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodSpawner : MonoBehaviour {

    public Asteriod asteriodPrefab;

    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;


    // Start is called before the first frame update
    void Start() {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn() {

        for (int i = 0; i < this.spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteriod asteriod = Instantiate(this.asteriodPrefab, spawnPoint, rotation);
            asteriod.size = Random.Range(asteriod.minSize, asteriod.maxSize);
            asteriod.setTrajectory(rotation * -spawnDirection);
        }

    }

    public void SpawnSplits(Transform transform, float originalSize, float speed, int splits) {
        for (int i = 0; i < splits; i++) {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle / (float)splits;

            Asteriod half = Instantiate(this.asteriodPrefab, position, transform.rotation);
            half.size = originalSize / (float)splits;
            half.setTrajectory(Random.insideUnitCircle.normalized * speed);
        }
    }


}