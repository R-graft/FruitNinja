using UnityEngine;
using System.Collections;

public class FlyingSimulation : MonoBehaviour
{
    private float directionX;

    private float directionY;

    private  float _forceX;

    private  float _forceY;

    private float gravity;

    private float _fallPosition;

    private float _gravityStep;

    public bool magnetMove;

    public Vector2 magnetPos;

    private void Awake()
    {
        _forceY = 3f;

        _forceX = 0.3f;

        _fallPosition = -5.5f;

        _gravityStep = 0.08f;
    }

    private void Update()
    {
        if (!magnetMove)
        {
            if (transform.position.y < _fallPosition)
            {
                gameObject.SetActive(false);
            }
            transform.position += new Vector3((directionX) * _forceX, (directionY + gravity) * _forceY, 0) * Time.deltaTime;

            gravity -= _gravityStep;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, magnetPos, 0.15f);
        }
       
    }
    public void MoveDirection()
    {
        FlyingTrajectory();
    }
    public void BombBlow(Vector3 bombPosition)
    {
        StartCoroutine(BombGravitation(bombPosition));
    }
    public void ActivateIceSpeed(string speedMode)
    {
        if (speedMode == "ice")
        {
            _forceY = 1f;

            _forceX = 0.2f;

            _gravityStep = 0.04f;
        }
        else if (speedMode == "normal")
        {
            _forceY = 3f;

            _forceX = 0.3f;

            _gravityStep = 0.08f;
        }
    }
    private void FlyingTrajectory()
    {
        gameObject.SetActive(true);

        directionX = -transform.position.x;

        directionY = -transform.position.y;

        _forceX += Random.Range(0.1f, 0.3f);

        gravity = 0;
    }
    public void HeartSpeed()
    {
        _forceY = 5f;

        _forceX = 0.6f;

        _gravityStep = 0.12f;
    }
    
    private IEnumerator BombGravitation(Vector3 bombPosition)
    {
        Vector3 direction = transform.position - bombPosition;

        float sideForce = 3f;

        float distance = direction.magnitude;

        while (gameObject.activeSelf)
        {
            yield return new WaitForFixedUpdate();

            transform.position += direction/distance * sideForce * Time.fixedDeltaTime;
        }
        yield break;
    }
}
