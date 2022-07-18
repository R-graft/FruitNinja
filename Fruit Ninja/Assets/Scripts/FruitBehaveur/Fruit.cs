using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    private GameObject _fruit;

    [SerializeField]
    private GameObject _fruitShadow;

    [SerializeField]
    private GameObject _halves;

    public InvokeFallFruit _fallFruit;

    private float _directionRotate;

    private float _startScale;

    private float _endScale;

    void Start()
    {
        GameEvents.slash.AddListener(SlashFruit);

        _fallFruit = GetComponent<InvokeFallFruit>();

        _directionRotate = Random.Range(-3f, 3f);

        _startScale = Random.Range(0.5f, 1.2f);

        _endScale = Random.Range(0.5f, 1.2f);

        StartCoroutine(FruitScale());
    }
    void Update()
    {
        FruitMove();
    }
    private void FruitMove()
    {
        _fruit.transform.Rotate(0, 0, _directionRotate);

        _fruitShadow.transform.Rotate(0, 0, _directionRotate);
    }
    private IEnumerator FruitScale()
    {
        _fruit.transform.localScale = new Vector2(_startScale, _startScale);

        _fruitShadow.transform.localScale = new Vector2(_startScale, _startScale);

        float scaleStep = (_endScale - _startScale) * Time.fixedDeltaTime/2;

        while (_fruit.transform.localScale != new Vector3(_endScale, _endScale))
        {
            yield return new WaitForFixedUpdate();

            _fruit.transform.localScale += new Vector3(scaleStep, scaleStep);

            _fruitShadow.transform.localScale += new Vector3(scaleStep, scaleStep);
        }
    }
    private void SlashFruit()
    {
        foreach (var item in Blade.Positions)
        {
            if (Vector2.Distance(item, transform.position) < 1)
            {
                ActivateHalves();

                _fallFruit.fall = false;

                GameEvents.fruitSlashed.Invoke();

                GameEvents.slash.RemoveListener(SlashFruit);

                break;
            }
        }
    }
    private void ActivateHalves()
    {
        _fruitShadow.SetActive(false);

        _fruit.SetActive(false);

        _halves.transform.localScale = _fruit.transform.localScale;

        _halves.transform.Rotate(0,0, Vector2.Angle(_halves.transform.position, Blade.StartPoint));

        _halves.SetActive(true);
    }
}
