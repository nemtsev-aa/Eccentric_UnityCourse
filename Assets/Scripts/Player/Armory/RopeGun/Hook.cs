using UnityEngine;

public class Hook : MonoBehaviour
{
    [Tooltip("Физическое тело крюка")]
    public Rigidbody Rigidbody;
    [Tooltip("Визуальное представление крюка")]
    public GameObject _hookObject;
    [Tooltip("Коллайдер крюка")]
    public Collider Collider;
    [Tooltip("Коллайдер игрока")]
    public Collider PlayerColloder;
    [Tooltip("Крюкострел")]
    public RopeGun RopeGun;

    // Неподвижное соединение 
    private FixedJoint _fixedJoint;
    private void Start()
    {
        Physics.IgnoreCollision(Collider, PlayerColloder);
    }

    private void Update()
    {
        if (_fixedJoint)
        {
            if (_fixedJoint.connectedBody == null)
                RopeGun.DestroySpring();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_fixedJoint == null)
        {
            // Создаём компанент FixedJoint
            _fixedJoint = gameObject.AddComponent<FixedJoint>();

            if (collision.rigidbody)
            {
                Debug.Log("collision.rigidbody" + collision.rigidbody.gameObject.name);
                // Прикрепляем FixedJoint к твёрдому телу, с которым столкнулся крюк
                _fixedJoint.connectedBody = collision.rigidbody;
                // Создаём верёвку
                RopeGun.CreateSpring();

                if (collision.rigidbody.GetComponent<EnemyAmmo>() || collision.rigidbody.GetComponent<Bullet>())
                    StopFix();
            } 
        }
    }

    public void StopFix()
    {
        // Уничтожаем неподвижное соединение
        if (_fixedJoint)
            Destroy(_fixedJoint);
    }
}
