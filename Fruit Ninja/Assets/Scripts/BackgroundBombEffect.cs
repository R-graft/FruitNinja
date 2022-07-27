using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBombEffect : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    private void Awake()
    {
        GameEvents.bombSlashing.AddListener(Animation);
    }
    private void Animation()
    {
        _anim.SetTrigger("Boom");
    }
}
