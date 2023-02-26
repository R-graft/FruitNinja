using System.Collections;
using UnityEngine;

namespace winterStage
{
    public class BrickBonus : Bonus
    {
        private BladeHandler _blade;
        public BrickBonus(BladeHandler blade)
        {
            _blade = blade;
        }

        public IEnumerator BrickAction()
        {
            _blade.DisableBlade();

            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            _blade.EnableBlade();
        }

    }
}
