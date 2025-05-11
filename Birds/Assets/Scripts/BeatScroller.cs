using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] private float beatTempo;
    private float _beatsPerSecond;
    public bool hasStarted;
    public float BeatTempo => beatTempo;
    
    void Start()
    {
        _beatsPerSecond = beatTempo / 60;
    }

    void Update()
    {
        if (hasStarted) transform.position -= new Vector3(_beatsPerSecond * Time.deltaTime, 0f, 0f);
    }
}
