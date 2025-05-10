using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("Sprite Settings")]
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite pressedSprite;

    public KeyCode keyToPress;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress)) _spriteRenderer.sprite = pressedSprite;
        
        if (Input.GetKeyUp(keyToPress)) _spriteRenderer.sprite = defaultSprite;
    }
}