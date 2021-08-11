using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDetector : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;

    [SerializeField]
    private LayerMask checkMask; 
    
    Vector2 colliderPos;

    private void Update()
    {
        colliderPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(colliderPos, 0.25f, checkMask);

        CanMove = colliders.Length == 0;
    }
}
