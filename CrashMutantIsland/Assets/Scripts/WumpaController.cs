using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WumpaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CrashBandicoot")) 
        {
            Destroy(gameObject); // Destroys *this* Wumpa, not all
           // WumpaCounter.wumpaCounter.RaiseScore(1);
        }
    }
}

