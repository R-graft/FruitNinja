using UnityEngine;
public class Blot : MonoBehaviour
{
    private GameObject _blosContainer;
    void Start()
    {
        transform.SetParent(null);

        Destroy(gameObject, 2);
    }
}
