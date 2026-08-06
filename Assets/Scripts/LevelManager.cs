using UnityEngine;

[CreateAssetMenu(fileName = "LevelManager", menuName = "Scriptable Objects/LevelManager")]
public class LevelManager : ScriptableObject
{
   private int levelIndex = 0;
   public LevelData[] levels;
   public int LevelIndex => levelIndex;
   public bool IsPastLastLevel => levelIndex >= levels.Length - 1;
   public void NextLevel()
    {
        levelIndex++;
    }
   public void SetLevelIndex(int index)
   {
       levelIndex = index;
   }
   public LevelData GetCurrentLevelData()
    {
        if(levelIndex >= 0 && levelIndex < levels.Length)
        {
            return levels[levelIndex];
        }
        else
        {
            Debug.LogWarning("Level index is out of range.Returning null.");
            return levels[0];
        }
    }
}
[System.Serializable]
public class EnemiesData
{
   public Enemy enemyPrefab;
   public float spawnTime;
}
[System.Serializable]
public class LevelData
{
   public EnemiesData[] enemiesData;
}