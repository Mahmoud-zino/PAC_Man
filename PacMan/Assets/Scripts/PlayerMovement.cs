using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    [SerializeField]
    private Transform rightBorder;
    [SerializeField]
    private Transform leftBorder;

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

    private void Start()
    {
        StartCoroutine(MovePlayer());
    }

    protected override bool CanMove()
    {
        return GetComponentInChildren<PathDetector>().CanMove;
    }

    public IEnumerator MovePlayer()
    {
        yield return base.MoveBlockOverTime();
        yield return MovePlayer();
    }

    private void Update()
    {
        if (this.transform.position.x > rightBorder.position.x)
            SnapToPos(leftBorder.position + new Vector3(1, 0, 0));
        else if (this.transform.position.x < leftBorder.position.x)
            SnapToPos(rightBorder.position + new Vector3(-1, 0, 0));
    }

    private void SnapToPos(Vector3 pos)
    {
        StopCoroutine(MovePlayer());
        this.transform.position = pos;
        StartCoroutine(MovePlayer());
    }

    protected override void Move(Vector2 direction)
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
