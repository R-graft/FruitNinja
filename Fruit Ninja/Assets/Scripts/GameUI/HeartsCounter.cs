using UnityEngine;

public class HeartsCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartPanel;

    [SerializeField]
    private GameObject _heartPrefab;

    [SerializeField]
    private int _heartCount;

    void Start()
    {
        CreateHearts(_heartCount);

        GameEvents.fruitLost.AddListener(DeleteHeart);

        GameEvents.bombSlashing.AddListener(DeleteHeart);
    }
    private void DeleteHeart()
    {
        if (_heartPanel.transform.childCount > 0)
        {
            Destroy(_heartPanel.transform.GetChild(0).gameObject);
        }
        else
        {
            GameEvents.gameOver.Invoke();
        }
    }
    private void AddHeart()
    {
        CreateHearts(1);
    }
    private void CreateHearts(int _heartsCount)
    {
        if (_heartsCount < 1)
        {
            _heartsCount = 3;
        }
        for (int i = 0; i < _heartsCount; i++)
        {
            Instantiate(_heartPrefab, _heartPanel.transform);
        }
    }
}
