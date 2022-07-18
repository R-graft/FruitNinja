using UnityEngine;
public class Blot : MonoBehaviour
{
    private GameObject _blosContainer;
    void Start()
    {
        _blosContainer = GameObject.FindGameObjectWithTag("blot container");

        transform.SetParent(_blosContainer.transform);

        transform.rotation = Quaternion.identity;

        Destroy(gameObject, 2);
    }
}
