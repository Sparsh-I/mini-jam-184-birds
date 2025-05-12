using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("Sprite Settings")]
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite pressedSprite;

    public KeyCode keyToPress;
    private readonly List<NoteObject> _notesInRange = new();

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            _spriteRenderer.sprite = pressedSprite;
            HitFirstNoteInRange();
        }
        
        if (Input.GetKeyUp(keyToPress)) _spriteRenderer.sprite = defaultSprite;
    }

    private void HitFirstNoteInRange()
    {
        if (_notesInRange.Count == 0) return;

        NoteObject note = _notesInRange[0];
        _notesInRange.RemoveAt(0);
        note.Hit();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out NoteObject note)) _notesInRange.Add(note);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out NoteObject note)) _notesInRange.Remove(note);
    }
}