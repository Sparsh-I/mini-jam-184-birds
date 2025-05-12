using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    [Header("Results Page")]
    [SerializeField] private GameObject resultsPane;
    [SerializeField] private TextMeshProUGUI normalHitCountText, goodHitCountText, perfectHitCountText, missedHitCountText;

    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    
    private void Start()
    {
        resultsPane.gameObject.SetActive(false);
    }
    
    public void ShowResults()
    {
        resultsPane.gameObject.SetActive(true);
        
        normalHitCountText.text = "Normal\n" + gameManager.mehHitCount;
        goodHitCountText.text = "Good\n" + gameManager.goodHitCount;
        perfectHitCountText.text = "Perfect\n" + gameManager.perfectHitCount;
        missedHitCountText.text = "Missed\n" + gameManager.missedHitCount;
    }
}
