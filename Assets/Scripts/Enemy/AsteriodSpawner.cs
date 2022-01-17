using Assets.Scripts.Layer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodSpawner : MonoBehaviour {

    public Asteriod[] asteriodPrefabs;

    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;
    [SerializeField] Transform playerLocation;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn() {

        for (int i = 0; i < this.spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = playerLocation.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            //Quaternion rotation = Quaternion.AngleAxis(0, Vector3.forward);

            Asteriod asteriod = Instantiate(randomAsteriodPrefab(), spawnPoint, rotation);
            asteriod.size = Random.Range(asteriod.minSize, asteriod.maxSize); // Random size
            asteriod.setTrajectory(rotation * -spawnDirection);
        }

    }

    private Asteriod randomAsteriodPrefab() {
        return asteriodPrefabs[Random.Range(0, asteriodPrefabs.Length)];
    }

    public void SpawnSplits(Asteriod asteriod, float originalSize, float speed, int splits) {
        Transform transform = asteriod.transform;
        for (int i = 0; i < splits; i++) {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle / (float)splits;

            Asteriod half = Instantiate(asteriod, position, transform.rotation);
            half.size = originalSize / (float)splits; // todo figure out a better way
            half.setTrajectory(Random.insideUnitCircle.normalized * speed);
        }
    }

    public void ResetForGameOver() {
        GameObject[] asteriods = GameObject.FindGameObjectsWithTag(Tags.AsteriodTag);
        for (int i = 0; i < asteriods.Length; i++) {
            Destroy(asteriods[i]);
        }
    }


}
