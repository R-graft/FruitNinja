using System;
using UnityEngine;

namespace winterStage
{
    public class BombBlock : Block, IBonusBlock
    {
        public static Action<Vector2> OnBombBlow;
        public void BonusEffect()
        {
            OnBombBlow?.Invoke(transform.position);
        }
    }
}
