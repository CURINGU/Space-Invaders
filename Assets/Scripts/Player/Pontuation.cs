using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pontuation : MonoBehaviour
{
    public int currentPontuation;
    public TMP_Text pontuationTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pontuationTxt.text = currentPontuation.ToString();
    }

    public void AddPoints(int amount)
    {
        currentPontuation += amount;
    }

    public void DecreasePoints(int amount)
    {
        currentPontuation -= amount;
    }
}
