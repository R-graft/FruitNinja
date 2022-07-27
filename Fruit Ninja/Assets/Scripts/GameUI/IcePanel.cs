using System.Collections;
using UnityEngine;

public class IcePanel : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);

        GameEvents.iceBlockSlashed.AddListener(ActiveIcePanel);
    }
    private void ActiveIcePanel()
    {
        gameObject.SetActive(true);

        StartCoroutine(IcePanelIsActive());
    }

    IEnumerator IcePanelIsActive()
    {
        yield return new WaitForSeconds(5);
        {
            gameObject.SetActive(false);

            yield break;
        }
    }
    
}
