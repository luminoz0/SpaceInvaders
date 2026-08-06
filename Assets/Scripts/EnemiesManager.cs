using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class EnemiesManager : MonoBehaviour

{
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private UnityEvent<Transform> onEnemyDestroy;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private UnityEvent onAllEnemiesDestroyed;
    private int enemiesDestroyed = 0;
    private LevelData currentLevelData;
    public void SetLevel()
    {
        enemiesDestroyed = 0;
        currentLevelData = levelManager.GetCurrentLevelData();
        foreach (EnemiesData enemyData in currentLevelData.enemiesData)
        {
            StartCoroutine(SpawnEnemy(enemyData));
        }
    }
    private IEnumerator SpawnEnemy(EnemiesData enemyData)
    {
        yield return new WaitForSeconds(enemyData.spawnTime);
        Enemy enemy = PoolManager.Instance.GetObject(enemyData.enemyPrefab.gameObject, 
          Vector3.zero,true).GetComponent<Enemy>();
        enemy.OnDeath.AddListener(HandleEnemyDeath);
        enemy.Target = target;
        enemy.PositionEnemy();
    }
    private void HandleEnemyDeath(Transform enemyTransform)
    {
        onEnemyDestroy.Invoke(enemyTransform);
        enemiesDestroyed++;
        if (enemiesDestroyed >= currentLevelData.enemiesData.Length)
        {
            onAllEnemiesDestroyed.Invoke();
        }
    }
}
 