using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Music Settings")]
    [Tooltip("The music for this level")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private bool startPlaying;
    // [SerializeField] private int beatDelay;
    private float _timeDelay;
    
    [Header("Scoring Settings")]
    [SerializeField] private int currentScore;
    [SerializeField] private int scorePerNote;
    [SerializeField] private int scorePerGoodNote;
    [SerializeField] private int scorePerPerfectNote;
    
    [Header("Multiplier Settings")]
    [Tooltip("Multiplier that is currently set")]
    [SerializeField] private int currentMultiplier;
    
    [Tooltip("Tracking the notes hit for the current multiplier level")]
    [SerializeField] private int multiplierTracker;
    
    [Tooltip("Thresholds to get to next multiplier level")]
    [SerializeField] private int[] multiplierThreshold;
    [SerializeField] private TextMeshProUGUI multiplierText;
    
    [Header("Results Settings")]
    [SerializeField] private TextMeshProUGUI scoreText;
    public int mehHitCount, goodHitCount, perfectHitCount, missedHitCount;

    [Header("References")]
    [SerializeField] private BeatScroller beatScroller;
    public static GameManager Instance;
    
    private void Start()
    {
        startPlaying = false;
        Instance = this;
        
        scoreText.text = "Score: 0";
        
        currentMultiplier = 1;
        multiplierText.text = "Multiplier: x1";
        
        // _timeDelay = beatDelay * (60f / beatScroller.BeatTempo);
    }

    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPlaying = true;
                beatScroller.hasStarted = true;
                // StartCoroutine(StartAfterDelay(_timeDelay));
                musicSource.Play(); // remove if uncommenting the above lines
            }
        }
        
        scoreText.text = "Score: " + currentScore;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }

    private IEnumerator StartAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        musicSource.Play();
    }

    public void NoteHit()
    { 
        if (currentMultiplier - 1 < multiplierThreshold.Length)
        {
            multiplierTracker++;
            if (multiplierThreshold[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        
        scoreText.text = "Score: " + currentScore;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }

    public void NoteMissed()
    {
        missedHitCount++;
        currentMultiplier = 1;
        multiplierTracker = 0;
    }

    public void MehHit()
    {
        mehHitCount++;
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }
    
    public void GoodHit()
    {
        goodHitCount++;
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }
    
    public void PerfectHit()
    {
        perfectHitCount++;
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }
}
