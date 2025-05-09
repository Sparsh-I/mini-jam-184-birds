using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;

    [SerializeField] private ButtonController button;
    
    [Header("Thresholds")]
    [SerializeField] private float normalHitThreshold;
    [SerializeField] private float goodHitThreshold;
    [SerializeField] private float perfectHitThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button.keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                if (Mathf.Abs(transform.position.x - button.transform.position.x) > normalHitThreshold) GameManager.Instance.NormalHit();
                else if (Mathf.Abs(transform.position.x - button.transform.position.x) > goodHitThreshold) GameManager.Instance.GoodHit();
                else if (Mathf.Abs(transform.position.x - button.transform.position.x) > perfectHitThreshold) GameManager.Instance.PerfectHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator") && gameObject.activeSelf)
        {
            canBePressed = false;
            GameManager.Instance.NoteMissed();
        }
    }
}
