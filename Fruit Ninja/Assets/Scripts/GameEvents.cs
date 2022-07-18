using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static UnityEvent slash = new UnityEvent();

    public static UnityEvent fruitSlashed = new UnityEvent();

    public static UnityEvent fruitLost = new UnityEvent();
}
