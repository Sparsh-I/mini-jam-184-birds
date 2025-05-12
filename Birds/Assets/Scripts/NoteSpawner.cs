using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Transform spawnPoint;
    
    [Header("Scroller Mechanics")] [SerializeField] 
    private BeatScroller beatScroller;
     
    [Header("Bird Note Data")] [SerializeField]
    private List<BirdNoteData> birdNoteData = new List<BirdNoteData>();
    
    private readonly List<List<float>> _birdNoteTimings = new List<List<float>>();
    private int[] _nextNoteIndices;
    
    [SerializeField] private int currentBeat;
    
    private void Start()
    {
        var secondsPerBeat = 60f / beatScroller.BeatTempo;
        _nextNoteIndices = new int[birdNoteData.Count];
        
        foreach (var bird in birdNoteData)
        {
            List<float> timings = new List<float>();
            foreach (var beat in bird.beatTimings)
                timings.Add(beat * secondsPerBeat);

            _birdNoteTimings.Add(timings);
        }
    }
    
    private void Update()
    {
        if (!beatScroller.hasStarted) return;
        
        var currentTime = musicSource.time;

        for (int i = 0; i < birdNoteData.Count; i++)
        {
            var noteTimings = _birdNoteTimings[i];
            int nextIndex = _nextNoteIndices[i];

            if (nextIndex >= noteTimings.Count) continue;
            
            if (currentTime >= noteTimings[nextIndex])
            {
                SpawnFeather(i);
                _nextNoteIndices[i]++;
            }
        }
        
        currentBeat = (int) (musicSource.time * beatScroller.BeatTempo / 60f);
    }

    private void SpawnFeather(int birdIndex)
    {
        BirdNoteData bird = birdNoteData[birdIndex];
        GameObject feather = bird.featherPrefab;
        ButtonController birdController = bird.buttonController;
        Vector3 spawnOffset = new Vector3(0f, birdController.transform.position.y, 0f);
        
        GameObject note = Instantiate(feather, spawnPoint.position + spawnOffset, Quaternion.identity, beatScroller.transform);
        NoteObject noteScript = note.GetComponent<NoteObject>();
        noteScript.SetButton(birdController);
    }
}
