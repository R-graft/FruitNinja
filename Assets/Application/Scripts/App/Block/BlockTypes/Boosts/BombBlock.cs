namespace winterStage
{
    public class BombBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnBombSlash.Invoke(transform);

            HeartCounter.OnLoseHeart.Invoke();
        }
    }
}
