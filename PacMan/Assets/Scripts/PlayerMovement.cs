using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.3f;
    private MainInput controls;

    private Vector2 direction = Vector2.right;
    private bool canMove = true;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void Awake()
    {
        controls = new MainInput();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Start()
    {
        StartCoroutine(MoveBlockOverTime());
    }

    private void Move(Vector2 direction)
    {
        this.direction = new Vector2(Mathf.CeilToInt(direction.x), Mathf.CeilToInt(direction.y));

        if(this.direction.x > 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (this.direction.x < 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (this.direction.y > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (this.direction.y < 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.eulerAngles = new Vector3(0, 0, 270);
        }
    }

    private IEnumerator MoveBlockOverTime()
    {
        float elepsedTime = 0;
        Vector3 startPos = this.transform.position;
        Vector3 endPos = this.transform.position + new Vector3(direction.x, direction.y, 0);

        while (elepsedTime < speed)
        {
            this.transform.position = Vector3.Lerp(startPos, endPos, (elepsedTime / speed));
            elepsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        this.transform.position = endPos;

        yield return MoveBlockOverTime();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
