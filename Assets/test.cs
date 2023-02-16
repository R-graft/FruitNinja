using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform bomb_;

    public Transform fruit_;

    public Transform obj3;

    private void Update()
    {
        Vector2 bomb = bomb_.transform.position;

        Vector2 fruit = fruit_.transform.position;

        Vector2 direct = fruit - bomb;

        float dist = direct.magnitude;

        fruit_.name = "fruit" + dist;

        if (dist < 5)
        {
            Debug.DrawRay(fruit, direct.normalized / dist *5,  Color.red, 1);
        }
    }
}
