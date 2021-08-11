using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaiviour : MonoBehaviour
{
    public Transform OtherSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        collision.GetComponent<PlayerMovement>().SnapToPos(OtherSpawnPoint.position);
    }
}
