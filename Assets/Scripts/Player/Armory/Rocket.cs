using UnityEngine;

public class Rocket : Bullet
{
    [Header("Rocket")]
    public ParticleSystem hitParticle;

    public override void Hit(GameObject collisionGameObject)
    {
        // ������������� ��������� � ���������� ����
        Instantiate(hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override void Ricochet()
    {
        Instantiate(hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
