using UnityEngine;

public class JumpGun : MonoBehaviour
{
    [Tooltip("������ ���� ������")]
    public Rigidbody PlayerRigidbody;
    [Tooltip("�������� ������������ ������ �� ����� ��������")]
    public float Speed;
    [Tooltip("��������")]
    public Transform Spawn;
    [Tooltip("������")]
    public PlayerArmory PlayerArmory;
    [Tooltip("������������ �����")]
    public float MaxChange = 3f;
    [Tooltip("������� �������� ������")]
    private float _currentChange;
    [Tooltip("������ ������")]
    private bool _isChanged;

    public ChargeIcon ChargeIcon;
    void Update()
    {
        if (_isChanged)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayerRigidbody.AddForce(-Spawn.forward * Speed, ForceMode.VelocityChange);
                PlayerArmory.Guns[PlayerArmory.CurrentGunIndex].Shot();
                _currentChange = 0f;
                _isChanged = false;
                ChargeIcon.StartCharge();
            }
        }
        else
        {
            _currentChange += Time.unscaledDeltaTime;
            ChargeIcon.SetChargeValue(_currentChange, MaxChange);
            if (_currentChange >= MaxChange)
            {
                _isChanged = true;
                ChargeIcon.StopCharge();
            }
        }
    }
}
