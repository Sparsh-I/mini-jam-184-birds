using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<float> brownFeatherTimings;
    [SerializeField] private List<float> purpleFeatherTimings;
    [SerializeField] private float noteTravelTime = 1.0f;
    [SerializeField] private float time;
    
    private int _nextBrownFeatherIndex;
    private int _nextPurpleFeatherIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = musicSource.time;
        UpdateBrownFeather();
        UpdatePurpleFeather();
    }

    private void UpdateBrownFeather()
    {
        if (_nextBrownFeatherIndex >= brownFeatherTimings.Count) return;
        
        var currentTime = musicSource.time;

        if (currentTime >= brownFeatherTimings[_nextBrownFeatherIndex] - noteTravelTime)
        {
            SpawnBrownFeather();
            _nextBrownFeatherIndex++;
        }
    }
    
    private void UpdatePurpleFeather()
    {
        if (_nextPurpleFeatherIndex >= purpleFeatherTimings.Count) return;
        
        var currentTime = musicSource.time;

        if (currentTime >= purpleFeatherTimings[_nextPurpleFeatherIndex] - noteTravelTime)
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
