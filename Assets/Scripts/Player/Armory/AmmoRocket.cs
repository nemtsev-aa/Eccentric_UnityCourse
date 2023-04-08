
public class AmmoRocket : Bullet
{
    public override void Ricochet()
    {
        Instantiate(HitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
