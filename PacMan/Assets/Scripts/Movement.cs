using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.3f;

    protected Vector2 Direction = Vector2.right;
    protected bool CanMove;

    private void Start()
    {
        StartCoroutine(MoveBlockOverTime());
    }

    protected void ChangeDirection(Vector2 direction)
    {
        this.Direction = direction;
    }


    private IEnumerator MoveBlockOverTime()
    {
        float elepsedTime = 0;
        Vector3 startPos = this.transform.position;
        Vector3 endPos = this.transform.position + new Vector3(Direction.x, Direction.y, 0);

        if (!CanMove)
        {
            yield return new WaitForSeconds(0.01f);
            yield return MoveBlockOverTime();
        }
        else
        {
            while (elepsedTime < speed)
            {
                this.transform.position = Vector3.Lerp(startPos, endPos, (elepsedTime / speed));
                elepsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            this.transform.position = endPos;

            yield return MoveBlockOverTime();
        }
    }
}
