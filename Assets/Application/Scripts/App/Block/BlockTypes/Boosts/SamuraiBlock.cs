namespace winterStage
{
    public class SamuraiBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnSamuraiSlash.Invoke();
        }
    }
}
