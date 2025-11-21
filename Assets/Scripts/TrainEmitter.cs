using System.Collections;
using UnityEngine;

public class TrainEmitter : MonoBehaviour
{
    [Header("Assign the prefab that should APPEAR on each side")]
    public GameObject prefabFromLeft;
    public GameObject prefabFromRight;

    [Header("Spawn point transforms on each side (same Y as the track)")]
    public Transform spawnFromLeft;
    public Transform spawnFromRight;

    [Header("Optional: hook up the lever for this track")]
    public DirectionSwitch lever;

    [Header("Timing")]
    public bool spawnOnStart = true;
    public float minDelay = 4f;
    public float maxDelay = 6f;

    [Header("Debug")]
    public bool debugLogs = false;

    IEnumerator Start()
    {
        if (spawnOnStart) SpawnOnce();

        while (true)
        {
            float wait = Mathf.Max(0f, Random.Range(minDelay, maxDelay));
            yield return new WaitForSeconds(wait);
            SpawnOnce();
        }
    }

    public void SpawnOnce()
    {
        bool wantRightward = lever ? lever.isRight : true;

        Transform spawn = wantRightward ? spawnFromLeft : spawnFromRight;
        GameObject prefab = wantRightward ? prefabFromLeft : prefabFromRight;

        if (prefab == null || spawn == null) return;

        var go = Instantiate(prefab, spawn.position, Quaternion.identity);
        var mover = go.GetComponent<TrainMover>();
        if (mover)
        {
//mover.moveRight = wantRightward;

            if (debugLogs)
            {
                Debug.Log($"Spawned train at {spawn.name}, moving {(wantRightward ? "right" : "left")}");
            }
        }
    }
}
