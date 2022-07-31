using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBlade : MonoBehaviour
{
    [SerializeField]
    private Blade _blade;

    [SerializeField]
    private BladeTouches _bladeTouches;
    
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _blade.enabled = false;

            _bladeTouches.enabled = true;
        }
        else
        {
            _blade.enabled = true;

            _bladeTouches.enabled = false;
        }
        
    }
    
}
