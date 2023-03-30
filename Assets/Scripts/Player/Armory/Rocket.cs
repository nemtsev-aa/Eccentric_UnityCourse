using UnityEngine;

public class Rocket : Bullet
{
   
    public override void Hit(GameObject collisionGameObject)
    {
        // Визуализируем попадание и уничтожаем пулю
        Instantiate(HitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override void Ricochet()
    {
        Instantiate(HitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
