
public class RocketLauncher : Gun
{
    public PlayerArmory PlayerArmory;

    public override void Shot()
    {
        PlayerArmory.TakeGunByIndex(2);
        base.Shot();
    }

}
