using UnityEngine;

public class SetTriggerEveryNSeconds : MonoBehaviour
{
    [SerializeField] private float _period = 7f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _triggerName;
    // Время с прошлого переключения
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _period)
        {
            _timer = 0;
            _animator.SetTrigger(_triggerName);
        }
    }
}
