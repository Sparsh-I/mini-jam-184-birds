using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    [Header("Results Page")]
    [SerializeField] private GameObject resultsPane;
    [SerializeField] private TextMeshProUGUI normalHitCountText, goodHitCountText, perfectHitCountText, missedHitCountText;

    [Header("Timing Check")] [SerializeField]
    private float timer;

    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private BeatScroller beatScroller;
    
    private Transform _notes;
    
    private void Start()
    {
        resultsPane.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (musicSource.time >= timer)
        {
            EndLevel();
            ShowResults();
        }
    }

    private void EndLevel()
    {
        _notes = beatScroller.transform;
        musicSource.Stop();
        foreach (Transform note in _notes) Destroy(note.gameObject);
    }
    
    private void ShowResults()
    {
        resultsPane.gameObject.SetActive(true);
        
        normalHitCountText.text = "Normal\n" + gameManager.mehHitCount;
        goodHitCountText.text = "Good\n" + gameManager.goodHitCount;
        perfectHitCountText.text = "Perfect\n" + gameManager.perfectHitCount;
        missedHitCountText.text = "Missed\n" + gameManager.missedHitCount;
    }
}
