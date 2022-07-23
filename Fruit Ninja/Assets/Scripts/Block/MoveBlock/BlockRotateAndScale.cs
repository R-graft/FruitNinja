using System.Collections;
using UnityEngine;

public class BlockRotateAndScale : MonoBehaviour
{
    [SerializeField]
    private GameObject _blockShadow;

    private float _directionRotate;

    private float _endPosition = -10;

    private float _scaleStep;

    public void StartRotateAndSale()
    {
        _directionRotate = Random.Range(-4f, 4f);

        _scaleStep = Random.Range(-0.001f, 0.001f);

        if (gameObject.active)
        {
            StartCoroutine(ObjectRotate(gameObject));

            StartCoroutine(ObjectRotate(_blockShadow));

            StartCoroutine(ObjectScale(gameObject));

            StartCoroutine(ObjectScale(_blockShadow));
        }
    }

    private IEnumerator ObjectRotate(GameObject _obj)
    {
        while (_obj.transform.position.y > _endPosition)
        {
            yield return new WaitForFixedUpdate();

            _obj.transform.Rotate(0, 0, _directionRotate);
        }
        yield break;
    }

    private IEnumerator ObjectScale(GameObject _obj)
    {
        _obj.transform.localScale = Vector2.one;


        while (_obj.transform.position.y > _endPosition)
        {
            yield return new WaitForFixedUpdate();

            _obj.transform.localScale += new Vector3(_scaleStep, _scaleStep);
        }
        yield break;
    }
}