using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static UnityEvent fruitSlashed = new UnityEvent();

    public static UnityEvent bombSlashing = new UnityEvent();

    public static UnityEvent fruitLost = new UnityEvent();

    public static UnityEvent gameOver = new UnityEvent();

    public static UnityEvent iceBlockSlashed = new UnityEvent();

    public static UnityEvent heartBlockSlashed = new UnityEvent();

    public static UnityEvent magnetBlockSlashed = new UnityEvent();
}
