using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDetector : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
            this.CanMove = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
            this.CanMove = true;
    }
}
