using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public int score;

    [SerializeField] private float powerUpTime = 5f;

    private Animator playerAnim;
    private Coroutine lastRoutine;
    private bool isPowerUpActive;
    private GameManager gameManager;

    private void Start()
    {
        playerAnim = this.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


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
            {
                playerAnim.SetTrigger("Die");

                gameManager.GameOver();
            }
        }
        else if(collision.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);

            playerAnim.SetTrigger("Eat");

            if(lastRoutine != null)
                StopCoroutine(lastRoutine);

            lastRoutine = StartCoroutine(PowerUpRoutine());
        }
        else if(collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            playerAnim.SetTrigger("Eat");
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
