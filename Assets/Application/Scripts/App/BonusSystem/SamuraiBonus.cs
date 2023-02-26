using System.Collections;
using TMPro;
using UnityEngine;

namespace winterStage
{
    public class SamuraiBonus : Bonus
    {
        private TextMeshProUGUI _samuraiEffect;

        private BlocksController _blocks;

        private BonusController _controller;

        private SpawnSystem _spawner;

        private int _samuraiTime;

        private int _countdownTimescale = 1;

        private int _startHold = 4;

        public SamuraiBonus(TextMeshProUGUI _smauraiEffect, SpawnSystem spawner, BlocksController blocks, BonusController controller, int samuraiTime)
        {
            _samuraiEffect = _smauraiEffect;
            _spawner = spawner;
            _blocks = blocks;
            _controller = controller;
            _samuraiTime = samuraiTime;
        }

        public IEnumerator SamuraiBonusAction()
        {
            _spawner.StopSystem();

            _samuraiEffect.gameObject.SetActive(true);

            DisableSplashEffect(false);

            _blocks.SetHellMode(true);

            _spawner.SetHellMode(true);

            int currentTime = _samuraiTime;

            while (currentTime >= 0)
            {
                _samuraiEffect.text = currentTime.ToString();

                currentTime --;

                yield return new WaitForSecondsRealtime(_countdownTimescale);
            }

            _samuraiEffect.gameObject.SetActive(false);

            _spawner.StopSystem();

            yield return new WaitForSecondsRealtime(_startHold);

            DisableSplashEffect(true);

            _blocks.SetHellMode(false);

            _spawner.SetHellMode(false);

            _controller.isSamurai = false;
        }

        private void DisableSplashEffect(bool enable)
        {
            SplashView.foolMode = enable;
        }
    }
}
