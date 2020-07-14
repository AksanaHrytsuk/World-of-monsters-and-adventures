public class IceBall : Bullets
{
    public override void ApplyEffect(BaseClass obj)
    {
        base.ApplyEffect(obj);
        obj.Freeze();
    }
}
