using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]
public class RoundData : ScriptableObject
{
    public int enemyCount;
    public float spawnInterval;
    public GameObject[] enemyPrefabs;
}
