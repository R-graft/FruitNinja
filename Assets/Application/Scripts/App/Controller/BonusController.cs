using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BonusController : MonoBehaviour
    {
        private List<Block> blocks;

        
        private void BombBonusAction()
        {
            HeartCounter.OnLoseHeart?.Invoke();

            foreach (var block in blocks)
            {

            }
        }
    }
}
