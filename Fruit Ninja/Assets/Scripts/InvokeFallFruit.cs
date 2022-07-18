using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeFallFruit : MonoBehaviour
{
    public bool fall = true;
    public void OnDestroy()
    {
        if (fall)
        {
            GameEvents.fruitLost.Invoke();
        }
    }
}
