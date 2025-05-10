using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;
    [SerializeField] private Vector3 effectRelativePosition;
    
    [Header("Thresholds")]
    [SerializeField] private float normalHitThreshold;
    [SerializeField] private float goodHitThreshold;
    [SerializeField] private float perfectHitThreshold;

    [Header("References")] 
    [SerializeField] private ButtonController button;
    [SerializeField] private GameObject hitEffect, goodHitEffect, perfectHitEffect, missedHitEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        canBePressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button.keyToPress))
        {
            if (canBePressed)
            {
                if (Mathf.Abs(transform.position.x - button.transform.position.x) > normalHitThreshold)
                {
                    GameManager.Instance.NormalHit();
                    Instantiate(hitEffect, transform.position + effectRelativePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(transform.position.x - button.transform.position.x) > goodHitThreshold)
                {
                    GameManager.Instance.GoodHit();
                    Instantiate(goodHitEffect, transform.position + effectRelativePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(transform.position.x - button.transform.position.x) > perfectHitThreshold)
                {
                    GameManager.Instance.PerfectHit();
                    Instantiate(perfectHitEffect, transform.position + effectRelativePosition, Quaternion.identity);
                }
                
                gameObject.SetActive(false);
                Destroy(gameObject);
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
            Instantiate(missedHitEffect, transform.position + effectRelativePosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SetButton(ButtonController bird)
    {
        button = bird;
    }
}
