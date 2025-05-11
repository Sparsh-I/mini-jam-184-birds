using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Transform spawnPoint;
    
    [Header("Scroller Mechanics")]
    [SerializeField] private BeatScroller beatScroller;
    [SerializeField] private Vector3 brownFeatherOffset;
    [SerializeField] private Vector3 purpleFeatherOffset;
     
    [Header("Note Mechanics")]
    [SerializeField] private GameObject brownFeatherNote;
    [SerializeField] private GameObject purpleFeatherNote;
    [SerializeField] private ButtonController brownBirdController;
    [SerializeField] private ButtonController purpleBirdController;

    [Header("Timing")]
    [SerializeField] private List<float> brownFeatherBeats;
    [SerializeField] private List<float> purpleFeatherBeats;
    
    private readonly List<float> _brownFeatherTimings = new List<float>();
    private readonly List<float> _purpleFeatherTimings = new List<float>();
    
    [SerializeField] private int currentBeat;
    
    private int _nextBrownFeatherIndex;
    private int _nextPurpleFeatherIndex;

    private void Start()
    {
        var secondsPerBeat = 60f / beatScroller.BeatTempo;

        foreach (var beat in brownFeatherBeats) _brownFeatherTimings.Add(beat * secondsPerBeat);
        foreach (var beat in purpleFeatherBeats) _purpleFeatherTimings.Add(beat * secondsPerBeat);
    }
    
    private void Update()
    {
        if (!beatScroller.hasStarted) return;
        
        currentBeat = (int) (musicSource.time * beatScroller.BeatTempo / 60f);
        UpdateBrownFeather();
        UpdatePurpleFeather();
    }

    private void UpdateBrownFeather()
    {
        if (_nextBrownFeatherIndex >= _brownFeatherTimings.Count) return;
        
        var currentTime = musicSource.time;

        if (currentTime >= _brownFeatherTimings[_nextBrownFeatherIndex])
        {
            SpawnBrownFeather();
            _nextBrownFeatherIndex++;
        }
    }
    
    private void UpdatePurpleFeather()
    {
        if (_nextPurpleFeatherIndex >= _purpleFeatherTimings.Count) return;
        
        var currentTime = musicSource.time;

        if (currentTime >= _purpleFeatherTimings[_nextPurpleFeatherIndex])
        {
            SpawnPurpleFeather();
            _nextPurpleFeatherIndex++;
        }
    }

    private void SpawnBrownFeather()
    {
        GameObject note = Instantiate(brownFeatherNote, spawnPoint.position + brownFeatherOffset, Quaternion.identity, beatScroller.transform);
        NoteObject noteScript = note.GetComponent<NoteObject>();
        noteScript.SetButton(brownBirdController);
    }
    
    private void SpawnPurpleFeather()
    {
        GameObject note = Instantiate(purpleFeatherNote, spawnPoint.position + purpleFeatherOffset, Quaternion.identity, beatScroller.transform);
        NoteObject noteScript = note.GetComponent<NoteObject>();
        noteScript.SetButton(purpleBirdController);
    }


}
