using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DistantLands;

public class FishPoolManager : MonoBehaviour
{
    public List<IndependentFish> fishPool;
    public List<Transform> spawnPoints;
    public Transform playerCamera;
    public int initialFishCount = 3;

    private void Start()
    {

         // Disable all fish first
        foreach (var fish in fishPool)
        {
            fish.gameObject.SetActive(false);
        }

        // Shuffle spawn points to avoid always using the same ones
        spawnPoints = spawnPoints.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < initialFishCount && i < fishPool.Count && i < spawnPoints.Count; i++)
        {
            fishPool[i].ResetFish(spawnPoints[i].position);
        }

        // Deactivate the rest of the fish
        for (int i = initialFishCount; i < fishPool.Count; i++)
        {
            fishPool[i].gameObject.SetActive(false);
        }

        LogActiveFishCount();
    }

    private void OnEnable()
    {
        FishManager.OnFishCaught += HandleFishCaught;
    }

    private void OnDisable()
    {
        FishManager.OnFishCaught -= HandleFishCaught;
    }

    private void HandleFishCaught(int totalCaught)
    {
        var inactiveFish = fishPool.Where(f => !f.gameObject.activeInHierarchy).OrderBy(f => Random.value).FirstOrDefault();
        if (inactiveFish != null)
        {
            Transform spawnPoint = GetHiddenSpawnPoint();
            if (spawnPoint != null)
            {
                inactiveFish.ResetFish(spawnPoint.position);
            }
        }
    }

    private Transform GetHiddenSpawnPoint()
    {
        foreach (Transform spawn in spawnPoints)
        {
            Vector3 toSpawn = spawn.position - playerCamera.position;
            float distance = toSpawn.magnitude;

            toSpawn.Normalize();
            float dot = Vector3.Dot(playerCamera.forward, toSpawn);

       
            if (dot < Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                return spawn;
            }
        }

        return null;
    }

    public void LogActiveFishCount()
    {
        int activeCount = fishPool.Count(f => f.gameObject.activeInHierarchy);
        Debug.Log("Active Fish Count: " + activeCount);
    }
}