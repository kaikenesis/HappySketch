using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    private BirdMovement[] birds;

    void Start()
    {
        birds = GetComponentsInChildren<BirdMovement>(true);

        for (int i = 0; i < birds.Length; i++)
            birds[i].gameObject.SetActive(false);
    }

    public void TrySpawnBirds(int blockIndex)
    {
        if (blockIndex <= GameController.Instance.EnableBirdBlockIndex)
            return;

        float randomChance = Random.Range(0.0f, 100.0f);
        if (randomChance <= GameController.Instance.BirdSpawnProbability)
            SpawnMovingBirds();
    }

    private void SpawnMovingBirds()
    {
        for (int i = 0; i < birds.Length; i++)
            birds[i].gameObject.SetActive(true);
    }
}
