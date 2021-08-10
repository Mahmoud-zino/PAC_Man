using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    private MainInput controls;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void Awake()
    {
        controls = new MainInput();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Update()
    {
        base.CanMove = GetComponentInChildren<PathDetector>().CanMove;
    }

    private void Move(Vector2 direction)
    {
        base.Direction = new Vector2(Mathf.CeilToInt(direction.x), Mathf.CeilToInt(direction.y));

        if(base.Direction.x > 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (base.Direction.x < 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (base.Direction.y > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 90);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (base.Direction.y < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 270);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
