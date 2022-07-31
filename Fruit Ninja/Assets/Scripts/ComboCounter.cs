using System.Collections;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _comboValue;

    [SerializeField]
    private GameObject _comboPanel;

    private int _blocksCounter;

    private void Awake()
    {
        GameEvents.fruitSlashed.AddListener(IncrementBlocks);
    }

    private void Start()
    {
        StartCoroutine(Combo());
    }
    private IEnumerator Combo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (_blocksCounter > 2)
            {
                _comboValue.text = _blocksCounter.ToString();

                _comboPanel.SetActive(true);

                yield return new WaitForSeconds(2);

                _comboPanel.SetActive(false);
            }
            _blocksCounter = 0;
        }
    }
    void IncrementBlocks()
    {
        _blocksCounter ++;
    }
}
