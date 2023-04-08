using UnityEngine;
public enum RopeState
{
    Disable,
    Fly,
    Active
}

public class RopeGun : MonoBehaviour
{
    [Tooltip("������ �������� ������")]
    public PlayerMove PlayerMove; 
    [Tooltip("����")]
    public Hook Hook;
    [Tooltip("�������� �������� �����")]
    public float _ropeDistance = 10f;
    [Tooltip("����� ��������� �����")]
    public Transform SpawnPoint;
    [Tooltip("�������� ����� �����")]
    public float Speed;
    [Tooltip("��������� ������")]
    public RopeState CurrentRopeState;
    [Tooltip("������")]
    public RopeRenderer RopeRenderer;

    [Header("SpringJoint Settings")]
    [Tooltip("��������� - �������")]
    public SpringJoint SpringJoint;
    [Tooltip("����� ��������� ������� � ������")]
    [SerializeField] Transform _startPoint;
    [Tooltip("Ƹ������� �������")]
    [SerializeField] float _spring = 100f;
    [Tooltip("�������� �������")]
    [SerializeField] float _damper = 5f;
    [Tooltip("������������ ���������")]
    [SerializeField] float _maxDistance = 3f;

    // ����� ������
    private float _lenght;
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
            Shot();

        if (CurrentRopeState == RopeState.Fly)
        {
            float distance = Vector3.Distance(_startPoint.position, Hook.transform.position);
            if (distance >= _ropeDistance)
            {
                // ������������ ����
                Hook.gameObject.SetActive(false);
                // ������� ��������� ������ - ���������
                CurrentRopeState = RopeState.Disable;
                RopeRenderer.Hide();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentRopeState == RopeState.Active && !PlayerMove.Grounded)
                PlayerMove.Jump();
            
            DestroySpring();
        }


        if (CurrentRopeState == RopeState.Fly || CurrentRopeState == RopeState.Active)
        {
            RopeRenderer.Draw(_startPoint.position, Hook.transform.position, _lenght);
            // A��������� ����
            Hook.gameObject.SetActive(true);
        }
    }

    private void Shot()
    {
        _lenght = 1f;
        // ���������� �������, ���� ��� ����������
        if (SpringJoint)
            Destroy(SpringJoint);
        // ���������� ����
        Hook.gameObject.SetActive(true);
        // ���������� ����
        Hook.StopFix();
        // ����������� ����� ��������� �������� ��������� � ��������
        Hook.transform.position = SpawnPoint.position;
        Hook.transform.rotation = SpawnPoint.rotation;
        // ������������ ������� ���� ����������� ���� � �����
        Hook.Rigidbody.velocity = SpawnPoint.forward * Speed;
        // ������� ��������� ������ - ����
        CurrentRopeState = RopeState.Fly;
    }

    public void CreateSpring()
    {
        if (SpringJoint == null)
        {
            // ��������� ��������� "�������" � ������������� �������� ��� ���������
            SpringJoint = gameObject.AddComponent<SpringJoint>();
            SpringJoint.connectedBody = Hook.Rigidbody;
            SpringJoint.autoConfigureConnectedAnchor = false;
            SpringJoint.connectedAnchor = Vector3.zero;
            SpringJoint.anchor = _startPoint.localPosition;
            SpringJoint.spring = _spring;
            SpringJoint.damper = _damper;

            _lenght = Vector3.Distance(_startPoint.position, Hook.transform.position);
            _maxDistance = _lenght;
            SpringJoint.maxDistance = _maxDistance;

            // ������� ��������� ������ - �������� ��������� ������
            CurrentRopeState = RopeState.Active;
        }
    }

    public void DestroySpring()
    {
        if (SpringJoint)
        {
            Destroy(SpringJoint);
            CurrentRopeState = RopeState.Disable;
            Hook.gameObject.SetActive(false);
            RopeRenderer.Hide();
        }
    }
}
