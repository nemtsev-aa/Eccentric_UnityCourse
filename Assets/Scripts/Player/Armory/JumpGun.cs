using UnityEngine;

public class JumpGun : MonoBehaviour
{
    [Tooltip("Твёрдое тело игрока")]
    public Rigidbody PlayerRigidbody;
    [Tooltip("Скорость передаваемая игроку во время выстрела")]
    public float Speed;
    [Tooltip("Источник")]
    public Transform Spawn;
    [Tooltip("Оружее")]
    public Pistol Pistol;
    [Tooltip("Максимальный заряд")]
    public float MaxChange = 3f;
    [Tooltip("Текущее значение заряда")]
    private float _currentChange;
    [Tooltip("Статус заряда")]
    private bool _isChanged;

    public ChargeIcon ChargeIcon;
    void Update()
    {
        if (_isChanged)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayerRigidbody.AddForce(-Spawn.forward * Speed, ForceMode.VelocityChange);
                Pistol.Shot();
                _currentChange = 0f;
                _isChanged = false;
                ChargeIcon.StartCharge();
            }
        }
        else
        {
            _currentChange += Time.deltaTime;
            ChargeIcon.SetChargeValue(_currentChange, MaxChange);
            if (_currentChange >= MaxChange)
            {
                _isChanged = true;
                ChargeIcon.StopCharge();
            }
        }
    }
}
