using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    [Tooltip("������ ����")]
    [SerializeField] private GameObject _bulletPrafab;
    [Tooltip("������������ ���������� ����")]
    [SerializeField] private Transform _bulletCreator;
    [Tooltip("�������� ����")]
    [SerializeField] private float _bulletSpeed = 10f;
    [Tooltip("����������������")]
    public float ShotPeriod = 0.2f;
    [Tooltip("������ ��������")]
    [SerializeField] private GameObject _flash;
    [Tooltip("���� ��������")]
    [SerializeField] private AudioSource _shotSound;

    private float _timer;
    internal bool _isOverUI;

    void Update()
    {
        _timer += Time.unscaledDeltaTime;
        _isOverUI = EventSystem.current.IsPointerOverGameObject();

        if (_timer > ShotPeriod)
        {
            if (Input.GetMouseButton(0) && !_isOverUI)
            {
                _timer = 0;
                Shot();
            }
        }     
    }

    public virtual void Shot()
    {
        GameObject newBullet = Instantiate(_bulletPrafab, _bulletCreator.position, _bulletCreator.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = _bulletCreator.forward * _bulletSpeed;

        ShowShotEffects();
    }

    private void HideFlash()
    {
        _flash.SetActive(false);
    }

    private void ShowShotEffects()
    {
        _shotSound.Play();

        _flash.SetActive(true);
        Invoke(nameof(HideFlash), 0.1f);
    }

    public virtual void Activate()
    {
        
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void AddBullets(int numberOfBullets)
    {
        
    }

} 
