using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public int score;

    [SerializeField] private float powerUpTime = 5f;

    private Coroutine lastRoutine;
    private bool isPowerUpActive;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(isPowerUpActive)
            {
                Instantiate(collision.gameObject, new Vector3(-2, 0, 0), collision.gameObject.transform.rotation);
                Destroy(collision.gameObject);
                score += 100;
            }
            else
                Debug.LogError("Game Over");
        }
        else if(collision.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);

            if(lastRoutine != null)
                StopCoroutine(lastRoutine);

            lastRoutine = StartCoroutine(PowerUpRoutine());
        }
        else if(collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            score++;
        }
    }

    private IEnumerator PowerUpRoutine()
    {
        Debug.Log("start powerup");
        isPowerUpActive = true;

        yield return new WaitForSeconds(powerUpTime);

        isPowerUpActive = false;
        Debug.Log("end powerup");
    }
}
