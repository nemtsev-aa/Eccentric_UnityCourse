using UnityEngine;
public enum RopeState
{
    Disable,
    Fly,
    Active
}

public class RopeGun : MonoBehaviour
{
    [Tooltip("Скрипт движения игрока")]
    public PlayerMove PlayerMove; 
    [Tooltip("Крюк")]
    public Hook Hook;
    [Tooltip("Диапазон действия крюка")]
    public float _ropeDistance = 10f;
    [Tooltip("Точка появления крюка")]
    public Transform SpawnPoint;
    [Tooltip("Скорость полёта крюка")]
    public float Speed;
    [Tooltip("Состояние верёвки")]
    public RopeState CurrentRopeState;
    [Tooltip("Верёвка")]
    public RopeRenderer RopeRenderer;

    [Header("SpringJoint Settings")]
    [Tooltip("Компанент - пружина")]
    public SpringJoint SpringJoint;
    [Tooltip("Точка крепления пружины к игроку")]
    [SerializeField] Transform _startPoint;
    [Tooltip("Жёсткость пружины")]
    [SerializeField] float _spring = 100f;
    [Tooltip("Вязкость пружины")]
    [SerializeField] float _damper = 5f;
    [Tooltip("Максимальная дистанция")]
    [SerializeField] float _maxDistance = 3f;

    // Длина верёвки
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
                // Деактивируем крюк
                Hook.gameObject.SetActive(false);
                // Текущее состояние верёвки - неактивна
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
            // Aктивируем крюк
            Hook.gameObject.SetActive(true);
        }
    }

    private void Shot()
    {
        _lenght = 1f;
        // Уничтожаем пружину, если она существует
        if (SpringJoint)
            Destroy(SpringJoint);
        // Активируем крюк
        Hook.gameObject.SetActive(true);
        // Отвязываем крюк
        Hook.StopFix();
        // Присваиваем крюку начальные значения положения и поворота
        Hook.transform.position = SpawnPoint.position;
        Hook.transform.rotation = SpawnPoint.rotation;
        // Осуществляем выстрел путём примененеия силы к крюку
        Hook.Rigidbody.velocity = SpawnPoint.forward * Speed;
        // Текущее состояние верёвки - полёт
        CurrentRopeState = RopeState.Fly;
    }

    public void CreateSpring()
    {
        if (SpringJoint == null)
        {
            // Добавляем компанент "Пружина" и устанавливаем значения его свойствам
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

            // Текущее состояние верёвки - активная поддержка игрока
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
