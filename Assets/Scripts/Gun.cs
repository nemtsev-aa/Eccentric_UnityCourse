using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("Префаб пули")]
    [SerializeField] private GameObject _bulletPrafab;
    [Tooltip("Расположение генератора пуль")]
    [SerializeField] private Transform _bulletCreator;
    [Tooltip("Скорость пули")]
    [SerializeField] private float _bulletSpeed = 10f;
    [Tooltip("Скорострельность")]
    [SerializeField] private float _shotPeriod = 0.2f;
    [Tooltip("Эффект выстрела")]
    [SerializeField] private GameObject _flash;
    [Tooltip("Звук выстрела")]
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
