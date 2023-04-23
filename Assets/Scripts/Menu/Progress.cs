using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private int _coins;
    [SerializeField] private int _level;
    [SerializeField] private bool _isMusicOn;
    //[SerializeField] private Color _backgroundColor;
    //public Color BackgroundColor => _backgroundColor;
    public bool IsMusicOn => _isMusicOn;
    public int Coins => _coins;
    public int Level => _level;

    public static Progress Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        Load();
    }

    public void SetLevel(int level)
    {
        _level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        _coins += value;
        Save();
    }

    [ContextMenu("DeleteFile")]
    public void DeleteFile()
    {
        SaveSystem.DeleteFile();
    }
    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();
        if (progressData != null)
        {
            _coins = progressData.Coins;
            _level = progressData.Level;
            _isMusicOn = progressData.IsMusicOn;

            //Color color = new Color();
            //color.r = progressData.BackgroundColor[0];
            //color.g = progressData.BackgroundColor[1];
            //color.b = progressData.BackgroundColor[2];
            //_backgroundColor = color;
        }
        else
        {
            _coins = 0;
            _level = 1;
            _isMusicOn = true;
        }
    }
}
