using UnityEngine;
using System.Collections;

public class FlyingSimulation : MonoBehaviour
{
    private float directionX;

    private float directionY;

    private float speed;

    private float iceSpeed = 1;

    private float gravity;

    private float _fallPosition = -5.5f;

    public void MoveDirection()
    {
        StartCoroutine(FlyingTrajectory());
    }
    public void BombBlow(Vector3 bombPosition)
    {
        StartCoroutine(BombGravitation(bombPosition));
    }
    public void ActivateIceSpeed()
    {
        StartCoroutine(IceMove());
    }
    private IEnumerator IceMove()
    {
        iceSpeed = 5;

        yield return new WaitForSeconds(5);
        {
            iceSpeed = 1;

            yield break;
        }
    }
    private IEnumerator BombGravitation(Vector3 bombPosition)
    {
        Vector3 direction = transform.position - bombPosition;

        float sideForce = 3;

        float distance = direction.magnitude;

        while (gameObject.activeSelf)
        {
            yield return new WaitForFixedUpdate();

            transform.position += direction/distance * sideForce * Time.fixedDeltaTime;
        }
        yield break;
    }
    private IEnumerator FlyingTrajectory()
    {
        gameObject.SetActive(true);

        directionX = -transform.position.x;

        directionY = -transform.position.y;

        speed = Random.Range(0.5f, 0.6f);

        gravity = 0;

        while (transform.position.y > _fallPosition)
        {
            yield return new WaitForFixedUpdate();

            transform.position += new Vector3((directionX) * speed, (directionY + gravity), 0) * Time.deltaTime ;

            gravity -= 0.03f;
        }

        gameObject.SetActive(false);

        yield break;
    }
}
