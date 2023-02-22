using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBlur : MonoBehaviour
{
    public Color Color { get => _color; set { _color = value; UpdateColor(); } }
#if UNITY_EDITOR
    /// <summary>
    /// (Editor Only).
    /// </summary>
#endif
    public float Intensity { get => _intensity; set { _intensity = Mathf.Clamp01(value); UpdateIntensity(); } }
    public float Multiplier { get => _multiplier; set { _multiplier = Mathf.Clamp01(value); UpdateMultiplier(); } }
    public UnityEvent OnBeginBlur { get => _onBeginBlur; set => _onBeginBlur = value; }
    public UnityEvent OnEndBlur { get => _onEndBlur; set => _onEndBlur = value; }
    public BlurChangedEvent OnBlurChanged { get => _onBlurChanged; set => _onBlurChanged = value; }

    [SerializeField]
    private Color _color = Color.white;
    [SerializeField, Range(0f, 1f)]
    private float _intensity;
    [SerializeField, Range(0f, 1f)]
    private float _multiplier = 0.15f;
    [SerializeField]
    private UnityEvent _onBeginBlur;
    [SerializeField]
    private UnityEvent _onEndBlur;
    [SerializeField]
    private BlurChangedEvent _onBlurChanged;

    [SerializeField]
    private Material _material;
    private int _colorId;
    private int _flipXId;
    private int _flipYId;
    private int _intensityId;
    private int _multiplierId;

    public void UpdateBlur()
    {
        SetBlur(Color, Intensity, Multiplier);
    }

    public void SetBlur(Color color, float intensity, float multiplier)
    {
        Color = color;
        Intensity = intensity;
        Multiplier = multiplier;
    }

    public void BeginBlur(float speed)
    {
        StopAllCoroutines();
        StartCoroutine(BeginBlurCoroutine(speed));
    }

    public void EndBlur(float speed)
    {
        StopAllCoroutines();
        StartCoroutine(EndBlurCoroutine(speed));
    }

    private void Start()
    {
        SetComponents();
        SetBlur(Color, Intensity, _multiplier);
    }

    private void SetComponents()
    {
        _colorId = Shader.PropertyToID("_Color");
        _flipXId = Shader.PropertyToID("_FlipX");
        _flipYId = Shader.PropertyToID("_FlipY");
        _intensityId = Shader.PropertyToID("_Intensity");
        _multiplierId = Shader.PropertyToID("_Multiplier");
    }

    private void UpdateColor()
    {
        _material.SetColor(_colorId, Color);
    }

    private void UpdateIntensity()
    {
        _material.SetFloat(_intensityId, Intensity);
    }

    private void UpdateMultiplier()
    {
        _material.SetFloat(_multiplierId, Multiplier);
    }

    private IEnumerator BeginBlurCoroutine(float speed)
    {
        OnBeginBlur?.Invoke();

        while (Intensity < 1f)
        {
            Intensity += speed * Time.deltaTime;

            UpdateIntensity();

            OnBlurChanged.Invoke(Intensity);

            yield return null;
        }
    }

    private IEnumerator EndBlurCoroutine(float speed)
    {
        while (Intensity > 0f)
        {
            Intensity -= speed * Time.deltaTime;

            UpdateIntensity();

            OnBlurChanged.Invoke(Intensity);

            yield return null;
        }

        OnEndBlur?.Invoke();
    }

    [Serializable]
    public class BlurChangedEvent : UnityEvent<float> { }

    #region Editor
#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateBlurInEditor();
    }

    private void UpdateBlurInEditor()
    {
        _material.SetColor("_Color", Color);
        _material.SetFloat("_Intensity", Intensity);
        _material.SetFloat("_Multiplier", Multiplier);

        EditorUtility.SetDirty(_material);
    }
#endif
    #endregion
}

