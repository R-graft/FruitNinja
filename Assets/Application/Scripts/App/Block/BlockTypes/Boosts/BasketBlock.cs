namespace winterStage
{
    public class BasketBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnBasketSlash.Invoke(transform.position);
        }
    }
}
