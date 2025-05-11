using System.Collections;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Header("Song Settings")]
    [SerializeField] private float beatTempo;
    private float _beatsPerSecond;
    public float BeatTempo => beatTempo;
    public bool hasStarted;
    
    [Header("Timing Settings")] 
    [Tooltip("Length of the level in seconds")]
    [SerializeField] private float timer;
    
    [Tooltip("Length of the fade out at the end of the level in seconds")]
    [SerializeField] private float fadeTimer;
    
    private bool _levelEnded;
    
    [SerializeField] private float currentTimeInLevel;
    
    [Header("References")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private ResultsManager resultsManager;
    
    void Start()
    {
        _levelEnded = false;
        _beatsPerSecond = beatTempo / 60;
    }

    void Update()
    {
        if (!hasStarted || _levelEnded) return;
        
        transform.position -= new Vector3(_beatsPerSecond * Time.deltaTime, 0f, 0f);
        currentTimeInLevel += Time.deltaTime;

        if (musicSource.time >= timer - fadeTimer)
        {
            StartCoroutine(FadeOut(musicSource, fadeTimer));
        }
    }
    
    private IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeTime);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        EndLevel();
        audioSource.volume = startVolume;
    }
    
    private void EndLevel()
    {
        _levelEnded = true;
        resultsManager.ShowResults();
        foreach (Transform note in transform) Destroy(note.gameObject);
    }
}
