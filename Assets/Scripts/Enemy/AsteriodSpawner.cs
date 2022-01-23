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

    private List<Asteriod> spawned = new List<Asteriod>();
    private bool _resetting = false;


    private float maxSize = 1.5f;
     private float minSize = 0.5f;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating(nameof(Spawn), 0, this.spawnRate);
    }

    private void Spawn() {
        if (_resetting) return;

        for (int i = 0; i < this.spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = playerLocation.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            //Quaternion rotation = Quaternion.AngleAxis(0, Vector3.forward);

            Asteriod asteriod = Instantiate(randomAsteriodPrefab(), spawnPoint, rotation);
            //asteriod.size = 1.25f;//asteriod.maxSize; // Random size
            asteriod.size = Random.Range(minSize, maxSize); // Random size
            asteriod.setTrajectory(rotation * -spawnDirection);
            spawned.Add(asteriod);

        }

    }

    private Asteriod randomAsteriodPrefab() {
        //return asteriodPrefabs[0];
        return asteriodPrefabs[Random.Range(0, asteriodPrefabs.Length)];
    }

    public void SpawnSplits(Asteriod asteriod, float originalSize, float speed, int splits) {
        //Transform transform = asteriod.transform;
        //for (int i = 0; i < splits; i++) {
        //    Vector2 position = transform.position;
        //    position += Random.insideUnitCircle / (float)splits;

        //    Asteriod half = Instantiate(asteriod, position, transform.rotation);
        //    spawned.Add(half);
        //    half.size = originalSize / (float)splits; // todo figure out a better way
        //    half.setTrajectory(Random.insideUnitCircle.normalized * speed);
        //}
    }

    public void ResetForGameOver(
        float spawnRate,
        float maxSize,
        float minSize
        ) {
        CancelInvoke(nameof(Spawn));
        this.maxSize = maxSize;
        this.minSize = minSize;

        //print($"Spawn rate: {spawnRate} maxSize: {maxSize} minSize: {minSize}");


        _resetting = true;
        //GameObject[] asteriods = GameObject.FindGameObjectsWithTag(Tags.AsteriodTag);
        //for (int i = 0; i < asteriods.Length; i++) {
        //    Destroy(asteriods[i]);
        //}

        List<Asteriod> clonedList = new List<Asteriod>(spawned);

        foreach (var spawn in clonedList) {
            if (spawn != null && spawn.gameObject != null) {
                Destroy(spawn.gameObject);
                spawned.Remove(spawn);
            }
        }
        //spawned.Clear();
        _resetting = false;

        InvokeRepeating(nameof(Spawn), 0, spawnRate);


    }


}
