using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudControl : MonoBehaviour
{
    private const int MIN_CLOUDS = 5;
    private const int MAX_CLOUDS = 10;
    private const int SHUFFLE_COUNT = 30;
    private const float MOVE_SPEED = 5f;

    [SerializeField] private List<GameObject> cloudPrefabs;
    [SerializeField] private Transform startPoint, endPoint, spawnHub;

    private List<int> prefabIndexes = new List<int>();
    private List<Transform> clouds = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < cloudPrefabs.Count; i++)
            prefabIndexes.Add(i);
        
        foreach (Transform spawnPoint in spawnHub)
        {
            ShufflePrefabIndexes();
            int index = prefabIndexes[0];

            GameObject cloud = Instantiate(cloudPrefabs[index], spawnPoint);
            clouds.Add(cloud.transform);
            cloud.SetActive(false);
        }
        EnableRandomClouds();
    }

    void Update()
    {
        MoveClouds();
    }

    private void ShufflePrefabIndexes()
    {
        int repeat = SHUFFLE_COUNT;
        for (int i = 0; i < repeat; i++)
        {
            int index1 = Random.Range(0, prefabIndexes.Count);
            int index2 = Random.Range(0, prefabIndexes.Count);

            int temp = prefabIndexes[index1];
            prefabIndexes[index1] = prefabIndexes[index2];
            prefabIndexes[index2] = temp;
        }
    }

    private void EnableRandomClouds()
    {
        HashSet<int> cloudIndexes = new HashSet<int>();
        int totalCloudCount = Random.Range(MIN_CLOUDS, MAX_CLOUDS + 1);

        while (cloudIndexes.Count < totalCloudCount)
        {
            int index = Random.Range(0, clouds.Count);
            cloudIndexes.Add(index);
        }
        foreach (int index in cloudIndexes)
            clouds[index].gameObject.SetActive(true);
    }

    private void MoveClouds()
    {
        foreach (Transform cloud in clouds)
        {
            cloud.Translate(-cloud.right * MOVE_SPEED * Time.deltaTime);
            
            if (cloud.transform.position.x <= endPoint.position.x)
                cloud.transform.position = new Vector3(startPoint.position.x, cloud.position.y, cloud.position.z);
        }
    }
}
