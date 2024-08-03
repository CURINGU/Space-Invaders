using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomPowerUp : MonoBehaviour
{
    private PlayerPowerUps playerPowerUps;
    private RoundManager roundManager;
    private UiSoundFeedback uiSoundFeedback;

    public float selectionSpeed = 0.1f;
    private int currentIndex = 0;
    public bool isSelecting = false;

    [Header("UI")]
    public Image[] powerUpImages;
    public Image selectionMarker;
    public GameObject btnUI;
    public TMP_Text selectedTxt;

    private void Start()
    {
        playerPowerUps = FindObjectOfType<PlayerPowerUps>();
        roundManager = FindObjectOfType<RoundManager>();
        uiSoundFeedback = FindObjectOfType<UiSoundFeedback>();

        selectionMarker.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isSelecting && Input.GetKeyDown(KeyCode.Space))
        {
            ApplySelectedPowerUp();
        }
    }

    public void StartSelection()
    {
        if (!isSelecting)
        {
            StartCoroutine(SelectPowerUp());
        }
    }

    //Seleciona um PowerUp aleatório com um marcador que corre pelos objetos
    private IEnumerator SelectPowerUp()
    {
        yield return new WaitForSeconds(1);

        isSelecting = true;
        selectionMarker.gameObject.SetActive(true);
        currentIndex = 0;
        btnUI.SetActive(true);
        selectedTxt.gameObject.SetActive(false);

        while (isSelecting)
        {
            selectionMarker.transform.position = powerUpImages[currentIndex].transform.position;
            currentIndex = (currentIndex + 1) % powerUpImages.Length;
            uiSoundFeedback.PlaySound(UiSoundType.selection);
            yield return new WaitForSeconds(selectionSpeed);
        }
    }

    public void StopSelection()
    {
        selectionMarker.gameObject.SetActive(false);
        selectedTxt.gameObject.SetActive(false);
        roundManager.GoNextRound();
    }

    public void ApplySelectedPowerUp()
    {
        //Debug.Log("PowerUp " + currentIndex);
        switch (currentIndex)
        {
            case 0:
                playerPowerUps.DecreaseReloadTime();
                selectedTxt.text = "Faster Reload!";
                break;
            case 1:
                playerPowerUps.AddSpeed();
                selectedTxt.text = "Increased Movement Speed!";
                break;
            case 2:
                playerPowerUps.AddLives();
                selectedTxt.text = "5 Extra Lives!";
                break;
            case 3:
                playerPowerUps.AddShieldDuration();
                selectedTxt.text = "Shield Duration Extended!";
                break;
        }
        uiSoundFeedback.PlaySound(UiSoundType.selected);
        isSelecting = false;
        StartCoroutine(selectedPowerUp());
    }

    private IEnumerator selectedPowerUp()
    {
        btnUI.SetActive(false);
        selectedTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        StopSelection();
    }
}
