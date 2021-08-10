using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaiviour : MonoBehaviour
{
    public GameObject OtherGate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Vector2 direction = collision.GetComponent<PlayerMovement>().GetCurrentDirection();

        GameObject player = Instantiate(collision.gameObject, OtherGate.transform.position + new Vector3(0.5f, 0, 0), collision.gameObject.transform.rotation);

        player.GetComponent<PlayerMovement>().Move(direction);

        Destroy(collision.gameObject);

        Collider2D otherGateCollider = OtherGate.GetComponent<Collider2D>();
        StartCoroutine(DisableColliderForTime(otherGateCollider, 0.5f));
    }


    private IEnumerator DisableColliderForTime(Collider2D collider2D, float time)
    {
        collider2D.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(time);

        collider2D.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}
