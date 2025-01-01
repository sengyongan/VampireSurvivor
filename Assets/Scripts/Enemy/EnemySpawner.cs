using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/* 敌人生成器 */
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyObj;/* 生成物体 */
    public float timeToSpawn;/* 生成间隔时间 */
    private float spawnCounter;/* 生成计时器 */
    public Transform minSpawnerPoint, maxSpawnerPoint;/* 生成点 :左下，右上*/
    private Transform target;/* 玩家 */
    /* 如果每帧检查整个敌人列表太消耗性能，如果每间隔几帧检查一次会跳帧，所以我们每帧检查列表的一部分 */
    private float despawnDistance;/* 超出这个范围，就排除敌人 */
    private List<GameObject> spawnedEnemies = new List<GameObject>();/* 生成的敌人存入到列表 */
    public int checkPerFrame;/* per除法，每一帧检查的范围 */
    private int enemyToCheck;/* 正在检查的敌人的索引，相当于当前窗口起始位置 */
    public List<WaveInfo> waves;/* 产生效果不断循环波，每下一波要等待这一波结束 */
    private int currentWave;
    private float wavesCounter;
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;/* 生成器 */
        // target = FindObjectOfType<PlayerController>().transform;比较昂贵，搜索整个场景
        target = PlayerHealthController.instance.transform;/* 玩家 */
        despawnDistance = Vector3.Distance(target.position, maxSpawnerPoint.position) + 4;/* 排除的距离 */
        currentWave = -1;/* 从第一波开始 */
        GetToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.instance.gameObject.activeSelf)
        {/* 当玩家未死亡 */
            wavesCounter -= Time.deltaTime;
            if (wavesCounter <= 0)
            {
                GetToNextWave();
            }
            /* 生成这一波添加敌人 */
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = timeToSpawn; /* 重置计时器 */
                GameObject newObj = Instantiate(enemyObj, target.position + RandomSpawnPosition(), transform.rotation);
                spawnedEnemies.Add(newObj);
            }
        }
        /* 超过despawnDiatance排除敌人 */
        /* 想象一个敌人列表，要如何抽取检查呢？维护checkPerFrame长度的滑动窗口， */
        int checkTarget = enemyToCheck + checkPerFrame;/* 每一帧检查的窗口 */
        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)/* 窗口在列表内 */
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    /* 以despawnDiatance为半径的圆形范围 */
                    if (Vector3.Distance(spawnedEnemies[enemyToCheck].transform.position, target.position) > despawnDistance)/* 被检查的enemyToCheck超出范围 */
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        --checkTarget;/* 减小窗口范围 */
                    }
                    else
                    {/* 被检查的enemyToCheck没有超出范围 */
                        ++enemyToCheck;/* 减小窗口范围 */
                    }
                }
                else/* 被检查的enemyToCheck不存在，比如被玩家击杀 */
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    --checkTarget;/* 减小窗口范围 */
                }
            }
            else
            {/* 窗口起点超过列表最右边 */
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }
    /* 在相机边界生成敌人 */
    public Vector3 RandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool isTrue = Random.Range(0f, 1f) >= .5f;
        if (isTrue)
        {
            spawnPosition.y = Random.Range(minSpawnerPoint.position.y, maxSpawnerPoint.position.y);
            bool isTrue1 = Random.Range(0f, 1f) >= .5f;
            if (isTrue1)
            {
                spawnPosition.x = minSpawnerPoint.position.x;
            }
            else
            {
                spawnPosition.x = maxSpawnerPoint.position.x;
            }
        }
        else
        {
            spawnPosition.x = Random.Range(minSpawnerPoint.position.x, maxSpawnerPoint.position.x);
            bool isTrue2 = Random.Range(0f, 1f) >= .5f;
            if (isTrue2)
            {
                spawnPosition.y = minSpawnerPoint.position.y;
            }
            else
            {
                spawnPosition.y = maxSpawnerPoint.position.y;
            }
        }
        return spawnPosition;
    }
    private void GetToNextWave()
    {
        ++currentWave;
        if (currentWave >= waves.Count)
        {
            currentWave = -1;/* 循环 */
            GetToNextWave();
        }
        else
        {
            WaveInfo curWave = waves[currentWave];
            enemyObj = curWave.spawnEnemy;
            wavesCounter = curWave.waveLength;
            timeToSpawn = curWave.timeBetweenSpawns;
            spawnCounter = curWave.timeBetweenSpawns;
        }
    }
}
[System.Serializable]
/* 每波敌人的信息 */
public class WaveInfo
{
    public GameObject spawnEnemy;/* 波的生成对象 */
    public float waveLength = 10f;/* 波的持续时间 */
    public float timeBetweenSpawns = 1f;/* 生成每个敌人的间隔时间 */
}