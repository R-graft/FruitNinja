using UnityEngine;

public class LoadPanel : MonoBehaviour//, IAnimatedElement
{
    [SerializeField]
    private GameObject _loadIcon;

    private void OnEnable()
    {
        InAnimation();
    }
    public void InAnimation() { } //=> _loadIcon.transform.DORotate(new Vector3(0, 0, -720), 1, RotateMode.WorldAxisAdd).SetLoops(-1).SetLink(gameObject);  

    public void OutAnimation()
    {
    }
}
