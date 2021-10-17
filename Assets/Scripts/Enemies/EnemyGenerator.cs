using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyType {
    Small,
    Big
}
[System.Serializable] 
public struct EnemyHord {
    public int smalls;
    public int bigs;
}
[System.Serializable] 
public struct EnemyWave {
    public List<EnemyHord> hords;
}
public class EnemyGenerator : MonoBehaviour {
    [SerializeField] private GameObject city;

    [SerializeField] private GameObject EnemySmallPrefab;
    [SerializeField] private GameObject EnemyBigPrefab;
    [SerializeField] private GameObject WarpPoint;
    [SerializeField] private float radius;
    private int wave = 0;
    [SerializeField] private NavMeshSurface navSurface;
    public List<EnemyWave> enemyWaves;
    [NonSerialized] private List<Vector3> currentWarpPos;
    [NonSerialized] private List<GameObject> currentWarpObjects;
    private void Start() {
        wave = 0;
        currentWarpPos = new List<Vector3>();
        currentWarpObjects = new List<GameObject>();
    }

    public void ShowPreviousWarpPoints() {
        Camera.main.orthographicSize = 10;
        currentWarpPos.Clear();
        var hords = enemyWaves[wave].hords;
        foreach (var hord in hords) {
            currentWarpPos.Add(GetWarpPoint());
        }

        foreach (var warpPos in currentWarpPos) {
           var warp = Instantiate(WarpPoint);
           currentWarpObjects.Add(warp);
           warp.transform.position = warpPos;
        }
    }

    // Update is called once per frame
    public void StartWave() {
        StartCoroutine(CreateWave());
    }

    private IEnumerator CreateWave() {
        yield return new WaitForSeconds(0.5f);
        EnemyWave enemyWave = enemyWaves[wave];
        var hordNumber = 0;
        foreach (var hord in enemyWave.hords) {
            var warpPos = currentWarpPos[hordNumber];
            for (int i = 0; i < hord.smalls; i++) {
                CreateEnemy(EnemyType.Small, warpPos);
                yield return new WaitForSeconds(0.3f);
            }
            for (int i = 0; i < hord.bigs; i++) {
                CreateEnemy(EnemyType.Big, warpPos);
                yield return new WaitForSeconds(0.3f);
            }
            hordNumber++;
        }
        wave++;
        DeleteWarpObjects();
    }

    private void CreateEnemy(EnemyType enemyType, Vector3 position) {
        switch (enemyType) {
            case EnemyType.Big:
                var big = Instantiate(EnemyBigPrefab);
                big.transform.position = position;
                navSurface.BuildNavMesh();
                var enemyBig = big.GetComponent<Enemy>();
                enemyBig.followPoint = city.transform.position;
                enemyBig.SetUpNavAgent();
                break;
            case EnemyType.Small:
                var small = Instantiate(EnemySmallPrefab);
                small.transform.position = position;
                navSurface.BuildNavMesh();
                var enemySmall = small.GetComponent<Enemy>();
                enemySmall.followPoint = city.transform.position;
                enemySmall.SetUpNavAgent();
                break;
        }
    }

    private Vector3 GetWarpPoint() {
        float angle = Mathf.Deg2Rad * Random.Range(0, 360);
        return new Vector3(radius*Mathf.Cos(angle), 0 , radius*Mathf.Sin(angle));
    }


    private void DeleteWarpObjects() {
        foreach (var warp in currentWarpObjects) {
            Destroy(warp);
        }
        currentWarpObjects.Clear();
        
    }
}


















