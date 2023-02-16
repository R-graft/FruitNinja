using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SeriesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _serieFruit;
    [SerializeField] private TextMeshProUGUI _serieMeltiplicator;

    [SerializeField] private RectTransform _transform;

    private Vector2 _bannerCurrentPosition = Vector2.zero;
    private Vector2 _bannerExitPosition = new Vector2(-15, 6);
    private Vector2 _bannerStartPosition = new Vector2(15, -6);

    private const string FruitTextLow = "фрукта";
    private const string FruitTextHigh = "фруктов";

    private int _currentCount;

    private bool _isCounting;

    private const float _timeScale = 0.1f;

    public static Action<Vector2> OnSlashFruit;

    private void SlashFruit(Vector2 pos)
    {
        _bannerCurrentPosition = pos;

        _currentCount++;

        if (!_isCounting)
        {
            StartCoroutine(Counter());
        }
    }
    private IEnumerator Counter()
    {
        _isCounting = true;

        var startCount = 1;

        while (true)
        {
            yield return new WaitForSeconds(_timeScale);

            if (_currentCount > startCount)
            {
                startCount = _currentCount;
            }

            else
            {
                SetBanner(_currentCount);

                _currentCount = 0;

                _isCounting = false;

                yield break;
            }
        }
    }

    private void SetBanner(int count)
    {
        if (count < 3)
        {
            return;
        }
        else if (count < 5)
        {
            _serieFruit.text = count.ToString() + " " + FruitTextLow;
        }
        else if (count >= 5)
        {
            _serieFruit.text = count.ToString() + " " + FruitTextHigh;
        }

        _serieMeltiplicator.text = "x" + count.ToString();

        ShowBanner();
    }
    private void ShowBanner()
    {
        _transform.position = _bannerStartPosition;

        DOTween.Sequence().Append(_transform.DOMove(_bannerCurrentPosition, 0.3f)).Insert(0, _transform.DOScale(new Vector2(0.005f, 0.005f), 0.3f)).
            AppendInterval(0.3f).Append(_transform.DOMove(_bannerExitPosition, 0.3f)).AppendCallback(()=> _transform.localScale = Vector2.zero);
    }

    private void OnEnable()
    {
        OnSlashFruit += SlashFruit;
    }

    private void OnDisable()
    {
        OnSlashFruit -= SlashFruit;
    }
}
