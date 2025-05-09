using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Music Settings")]
    [Tooltip("The music for this level")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private bool startPlaying;
    
    [Header("Scoring Settings")]
    [SerializeField] private int currentScore;
    [SerializeField] private int scorePerNote;
    [SerializeField] private int scorePerGoodNote;
    [SerializeField] private int scorePerPerfectNote;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("Multiplier Settings")]
    [Tooltip("Multiplier that is currently set")]
    [SerializeField] private int currentMultiplier;
    
    [Tooltip("Tracking the notes hit for the current multiplier level")]
    [SerializeField] private int multiplierTracker;
    
    [Tooltip("Thresholds to get to next multiplier level")]
    [SerializeField] private int[] multiplierThreshold;
    [SerializeField] private TextMeshProUGUI multiplierText;
    
    [Header("References")]
    [SerializeField] private BeatScroller beatScroller;
    public static GameManager Instance;
    
    private void Start()
    {
        Instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        multiplierText.text = "Multiplier: x1";
    }

    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;
                musicSource.Play();
            }
        }
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
        currentMultiplier = 1;
        multiplierTracker = 0;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }
    
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }
    
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }
}
