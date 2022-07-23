using UnityEngine;
using TMPro;

public class MenuBestScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _bestScore;
    void Start()
    {
        _bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
    }
}
