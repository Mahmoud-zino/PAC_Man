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

    public override bool CanMove()
    {
        return GetComponentInChildren<PathDetector>().CanMove;
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);

        if(direction.x > 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.y > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 90);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.y < 0)
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
