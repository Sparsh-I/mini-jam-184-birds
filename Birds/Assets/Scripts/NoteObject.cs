using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;
    [SerializeField] private Vector3 positionAddOn;
    
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
        positionAddOn = new Vector3(0f, Random.Range(-2f, 2f), 0f);
        
        if (Input.GetKeyDown(button.keyToPress))
        {
            if (canBePressed)
            {
                if (Mathf.Abs(transform.position.x - button.transform.position.x) > normalHitThreshold)
                {
                    GameManager.Instance.NormalHit();
                    Instantiate(hitEffect, transform.position + positionAddOn, Quaternion.identity);
                }
                else if (Mathf.Abs(transform.position.x - button.transform.position.x) > goodHitThreshold)
                {
                    GameManager.Instance.GoodHit();
                    Instantiate(goodHitEffect, transform.position + positionAddOn, Quaternion.identity);
                }
                else if (Mathf.Abs(transform.position.x - button.transform.position.x) > perfectHitThreshold)
                {
                    GameManager.Instance.PerfectHit();
                    Instantiate(perfectHitEffect, transform.position + positionAddOn, Quaternion.identity);
                }

                gameObject.SetActive(false);
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
            Instantiate(missedHitEffect, transform.position + positionAddOn, Quaternion.identity);
        }
    }

    public void SetButton(ButtonController bird)
    {
        button = bird;
    }
}
