using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;
    [SerializeField] private Vector3 effectRelativePosition;
    
    [Header("Thresholds")] [SerializeField] 
    private float normalHitThreshold, goodHitThreshold, perfectHitThreshold;

    [Header("References")] 
    [SerializeField] private ButtonController button;
    [SerializeField] private GameObject hitEffect, goodHitEffect, perfectHitEffect, missedHitEffect;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator") && gameObject.activeSelf)
        {
            Instantiate(missedHitEffect, transform.position + effectRelativePosition, missedHitEffect.transform.rotation);
            GameManager.Instance.NoteMissed();
            
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void SetButton(ButtonController bird)
    {
        button = bird;
    }

    public void Hit()
    {
        float hitOffset = Mathf.Abs(transform.position.y - button.transform.position.y);
        
        if (hitOffset <= perfectHitThreshold)
        {
            Instantiate(perfectHitEffect, transform.position + effectRelativePosition, perfectHitEffect.transform.rotation);
            GameManager.Instance.PerfectHit();
        }
        else if (hitOffset <= goodHitThreshold)
        {
            Instantiate(goodHitEffect, transform.position + effectRelativePosition, goodHitEffect.transform.rotation);
            GameManager.Instance.GoodHit();
        }
        else if (hitOffset <= normalHitThreshold)
        {
            Instantiate(hitEffect, transform.position + effectRelativePosition, hitEffect.transform.rotation);
            GameManager.Instance.MehHit();
        }
                
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
