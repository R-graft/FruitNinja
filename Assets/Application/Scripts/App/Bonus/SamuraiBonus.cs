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

        private GameUI _ui;

        private int _samuraiTime;

        private int slashedCount;

        private int _startCountdown = 3;
        private int _countdownTimescale = 1;
        private int _startHold = 4;
        private int _endHold = 2;
        private float _endTimescale = 0.02f;

        public SamuraiBonus(TextMeshProUGUI _smauraiEffect, SpawnSystem spawner, GameUI ui, BlocksController blocks, BonusController controller, int samuraiTime)
        {
            _samuraiEffect = _smauraiEffect;
            _spawner = spawner;
            _ui = ui;
            _blocks = blocks;
            _controller = controller;
            _samuraiTime = samuraiTime;
        }

        public IEnumerator SamuraiBonusAction()
        {
            _spawner.StopSystem();

            _samuraiEffect.gameObject.SetActive(true);

            DisableScore(false);

            int countdown = _startCountdown;

            while (countdown != 0)
            {
                _samuraiEffect.text = countdown.ToString();

                yield return new WaitForSecondsRealtime(_countdownTimescale);

                countdown--;
            }
            _samuraiEffect.gameObject.SetActive(false);

            _ui.EnableSeriesCounter(false);

            _blocks.SetHellMode(true);
            _spawner.SetHellMode(true);

            slashedCount = 0;
            SeriesCounter.OnSlash += IncremetnSlashed;

            yield return new WaitForSecondsRealtime(_samuraiTime);

            _spawner.StopSystem();

            yield return new WaitForSecondsRealtime(_startHold);

            DisableScore(true);

            _samuraiEffect.gameObject.SetActive(true);
            int count = 0;
            _samuraiEffect.text = count.ToString();

            while (count != slashedCount)
            {
                count++;

                yield return new WaitForSecondsRealtime(_endTimescale);

                _samuraiEffect.text = count.ToString();
            }

            yield return new WaitForSecondsRealtime(_endHold);

            _samuraiEffect.gameObject.SetActive(false);


            _blocks.SetHellMode(false);
            _spawner.SetHellMode(false);

            _ui.EnableSeriesCounter(true);

            SeriesCounter.OnSlash -= IncremetnSlashed;

            _controller.isSamurai = false;
        }

        private void IncremetnSlashed()
        {
            slashedCount++;
        }

        private void DisableScore(bool enable)
        {
            SplashView.foolMode = enable;
        }
    }
}
