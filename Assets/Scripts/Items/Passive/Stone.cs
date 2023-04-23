using UnityEngine;

public class Stone : PassiveItem
{
    [Tooltip("Текущий уровень объекта")]
    [Range(0, 2)]
    [SerializeField] int _level = 2;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Stone _stonePrefab;
    [Tooltip("Визуальный эффект разрушения")]
    [SerializeField] private GameObject _dieEffect;

    public override void OnAffect()
    {
        base.OnAffect();
        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
                CreateChildRock(_level - 1);
        }
        else
            ScoreManager.Instance.AddScore(ItemType, transform.position);
        
        Die();
    }

    void CreateChildRock(int level)
    {
        Stone newRock = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
        newRock.SetLevel(level);
    }

    public void SetLevel(int level)
    {
        _level = level;
        float scale = 1f;
        switch (level)
        {
            case 2:
                scale = 1f;
                break;
            case 1:
                scale = 0.7f;
                break;
            case 0:
                scale = 0.45f;
                break;      
        }
        _visualTransform.localScale = Vector3.one * scale;
    }

    void Die()
    {
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
