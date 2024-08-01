using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public RoundData[] rounds;
    private int currentRoundIndex = 0;
    private int enemiesRemaining;

    void Start()
    {
        StartRound(currentRoundIndex);
    }

    void StartRound(int roundIndex)
    {
        if (roundIndex >= rounds.Length)
        {
            Debug.Log("Todos os rounds completos!");
            return;
        }

        enemiesRemaining = rounds[roundIndex].enemyCount;
        StartCoroutine(SpawnEnemies(rounds[roundIndex]));
    }

    IEnumerator SpawnEnemies(RoundData roundData)
    {
        Debug.Log("Iniciando Round " + currentRoundIndex);
        for (int i = 0; i < roundData.enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 6f, 0f); // Ajuste as coordenadas conforme necessário
            GameObject enemyPrefab = roundData.enemyPrefabs[Random.Range(0, roundData.enemyPrefabs.Length)];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(roundData.spawnInterval);
        }
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            currentRoundIndex++;
            StartRound(currentRoundIndex);
        }
    }
}
