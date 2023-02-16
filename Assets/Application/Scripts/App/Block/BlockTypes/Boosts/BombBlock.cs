namespace winterStage
{
    public class BombBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnBombSlash.Invoke(transform);

            HeartCounter.OnLoseHeart.Invoke();
        }
    }
}
