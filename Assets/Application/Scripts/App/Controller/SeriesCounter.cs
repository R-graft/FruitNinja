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

    private Vector2 _bannerCurrentScale = new Vector2(0.005f, 0.005f);
    private Vector2 _bannerStartPosition;

    private const string FruitTextLow = "фрукта";
    private const string FruitTextHigh = "фруктов";

    private int _currentCount;

    private bool _isCounting;

    private const float _timeScale = 0.1f;

    public static Action<Vector2> OnSlashFruit;

    public static Action OnSlash;

    private bool _isShow;

    public bool enable = true;

    private void SlashFruit(Vector2 pos)
    {
        OnSlash?.Invoke();

        if (enable)
        {
            _bannerStartPosition = pos;

            _currentCount++;

            if (!_isCounting)
            {
                StartCoroutine(Counter());
            }
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

        if (!_isShow)
        {
            ShowBanner();
        }
    }
    private void ShowBanner()
    {
        _isShow = true;

        if (_bannerStartPosition.magnitude > 4)
        {
            _bannerStartPosition = new Vector2(_bannerStartPosition.x * 0.3f, _bannerStartPosition.y * 0.5f);
        }
        _transform.position = _bannerStartPosition;

        DOTween.Sequence().Append(_transform.DOScale(_bannerCurrentScale, 0.3f)).
            AppendInterval(0.3f).Append(_transform.DOScale(new Vector2(0.005f, 0.005f), 0.3f)).
            Append(_transform.DOScale(Vector3.zero, 0.3f)).AppendCallback(() => _isShow = false); ;
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
