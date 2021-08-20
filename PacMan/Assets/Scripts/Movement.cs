using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.3f;

    private Vector2 direction = Vector2.right;

    protected virtual void Move(Vector2 direction)
    {
        this.direction = direction;
    }

    protected virtual bool CanMove()
    {
        return true;
    }

    protected IEnumerator MoveBlockOverTime()
    {
        float elepsedTime = 0;
        Vector3 startPos = this.transform.position;
        Vector3 endPos = this.transform.position + new Vector3(direction.x, direction.y, 0);

        if (!this.CanMove())
        {
            yield return new WaitForSeconds(0.2f);
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
        }
    }
}
