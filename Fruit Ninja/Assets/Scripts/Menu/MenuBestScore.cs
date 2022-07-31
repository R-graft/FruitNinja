using UnityEngine;
using TMPro;

public class MenuBestScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _bestScore;
    void Awake()
    {
        _bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
    }
}
