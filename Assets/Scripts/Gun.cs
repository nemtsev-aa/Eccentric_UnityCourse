using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("������ ����")]
    [SerializeField] private GameObject _bulletPrafab;
    [Tooltip("������������ ���������� ����")]
    [SerializeField] private Transform _bulletCreator;
    [Tooltip("�������� ����")]
    [SerializeField] private float _bulletSpeed = 10f;
    [Tooltip("����������������")]
    [SerializeField] private float _shotPeriod = 0.2f;
    [Tooltip("������ ��������")]
    [SerializeField] private GameObject _flash;
    [Tooltip("���� ��������")]
    [SerializeField] private AudioSource _shotSound;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _shotPeriod)
        {
            if ((Input.GetMouseButton(0)))
            {
                _timer = 0;
                GameObject newBullet = Instantiate(_bulletPrafab, _bulletCreator.position, _bulletCreator.rotation);
                newBullet.GetComponent<Rigidbody>().velocity = _bulletCreator.forward * _bulletSpeed;

                ShowShotEffects();
            }
        }     
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
} 
