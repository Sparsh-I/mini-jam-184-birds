using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI normalHitCountText, goodHitCountText, perfectHitCountText, missedHitCountText;
    [SerializeField] private GameObject resultsPane;
    
    // Start is called before the first frame update
    void Start()
    {
        resultsPane.gameObject.SetActive(false);
    }

    private void ShowResults()
    {
        resultsPane.gameObject.SetActive(true);
        
        normalHitCountText.text = "Normal\n" + gameManager.normalHitCount;
        goodHitCountText.text = "Good\n" + gameManager.goodHitCount;
        perfectHitCountText.text = "Perfect\n" + gameManager.perfectHitCount;
        missedHitCountText.text = "Missed\n" + gameManager.missedHitCount;
    }
}
