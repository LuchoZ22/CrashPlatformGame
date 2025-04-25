using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumBoxController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "CrashBandicoot")
            {
                Debug.Log("Collision with Crash");
            }
        }
    }
}
