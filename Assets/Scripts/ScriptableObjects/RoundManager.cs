using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public RoundData[] rounds;
    private int currentRoundIndex = 0;
    private int enemiesRemaining;

    public GameObject UpgradesUI;

    private RandomPowerUp randomPowerUp;
    private UiSoundFeedback uiSoundFeedback;

    [Header("UI")]
    public TMP_Text roundTxt;

    void Start()
    {
        StartRound(currentRoundIndex);
        randomPowerUp = FindObjectOfType<RandomPowerUp>();
        uiSoundFeedback = FindObjectOfType<UiSoundFeedback>();
    }

    void StartRound(int roundIndex)
    {
        if (roundIndex >= rounds.Length) // Se chegou no último, repete infinitamente
        {
            enemiesRemaining = rounds[rounds.Length - 1].enemyCount;
            StartCoroutine(SpawnEnemies(rounds[rounds.Length - 1]));
            return;
        }

        enemiesRemaining = rounds[roundIndex].enemyCount;
        StartCoroutine(SpawnEnemies(rounds[roundIndex]));
    }

    IEnumerator SpawnEnemies(RoundData roundData)
    {
        yield return new WaitForSeconds(1);

        roundTxt.gameObject.SetActive(true);

        // Chama a coroutine para exibir o texto do round
        yield return StartCoroutine(DisplayRoundText("Round " + (currentRoundIndex + 1)));

        yield return new WaitForSeconds(1);

        roundTxt.gameObject.SetActive(false);

        //Debug.Log("Iniciando Round " + currentRoundIndex);
        for (int i = 0; i < roundData.enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
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
            UpgradesUI.SetActive(true);
            randomPowerUp.StartSelection();
        }
    }

    public void GoNextRound()
    {
        UpgradesUI.SetActive(false);
        //randomPowerUp.StopSelection();
        currentRoundIndex++;
        StartRound(currentRoundIndex);
    }

    private IEnumerator DisplayRoundText(string message)
    {
        roundTxt.text = ""; // Limpa o texto antes de começar

        foreach (char letter in message)
        {
            roundTxt.text += letter; // Adiciona uma letra de cada vez
            yield return new WaitForSeconds(0.1f);
            uiSoundFeedback.PlaySound(UiSoundType.textTyping);
        }
    }
}
