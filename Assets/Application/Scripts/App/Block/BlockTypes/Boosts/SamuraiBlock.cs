namespace winterStage
{
    public class SamuraiBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnSamuraiSlash.Invoke();
        }
    }
}
