using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathTemporary : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("attack"))    
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("attack"))
        {
            Destroy(this.gameObject);
        }
    }
}
