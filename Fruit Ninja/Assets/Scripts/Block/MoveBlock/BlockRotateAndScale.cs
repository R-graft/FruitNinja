using System.Collections;
using UnityEngine;

public class BlockRotateAndScale : MonoBehaviour
{
    [SerializeField]
    private GameObject _blockShadow;

    private SpriteRenderer _spriteRenderer;

    private float _directionRotate;

    private float _endPosition;

    private float _scaleStep;

    private void Awake()
    {
        _endPosition = -5.5f;

        SpriteRenderer spriteRenderer = _blockShadow.GetComponent<SpriteRenderer>();
    }
    public void StartRotateAndSale()
    {
        _directionRotate = Random.Range(-4f, 4f);

        _scaleStep = Random.Range(-0.05f, 0.05f) ;

        if (gameObject.activeSelf)
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

            _obj.transform.localScale += new Vector3(_scaleStep , _scaleStep) * Time.fixedDeltaTime;
        }
        yield break;
    }
}