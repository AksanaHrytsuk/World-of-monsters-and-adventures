public class IceBox : BaseClass
{
    private CharacterScript _character;
    protected override void StartAdditional()
    {
        base.StartAdditional();
        _character = FindObjectOfType<CharacterScript>();
    }
    // После уничтожения IceBox плеер может двигаться
    protected override void Death()
    {
        _character.Frozen = false;
        Destroy(gameObject);
    }
}
