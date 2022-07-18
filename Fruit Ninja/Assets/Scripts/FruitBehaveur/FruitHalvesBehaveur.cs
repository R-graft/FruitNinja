using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHalvesBehaveur : MonoBehaviour
{
    [SerializeField]
    private GameObject _halfOne;

    [SerializeField]
    private GameObject _halfTwo;

    [SerializeField]
    private GameObject _halfOneSprite;

    [SerializeField]
    private GameObject _halfTwoSprite;

    private float _rotationValue;

    private float _velocityValue;

    private void Start()
    {
        _rotationValue = Random.Range(1, 1.5f);

        _velocityValue = Random.Range(0.5f, 1);
    }
    void Update()
    {
        HalvesMove();
    }
    private void HalvesMove()
    {
        _halfOne.transform.Translate(Vector2.left * Time.deltaTime / _velocityValue);

        _halfOneSprite.transform.Rotate(0, 0, _rotationValue);

        _halfTwo.transform.Translate(Vector2.right * Time.deltaTime / _velocityValue);

        _halfTwoSprite.transform.Rotate(0, 0, _rotationValue);
    }
    
}
