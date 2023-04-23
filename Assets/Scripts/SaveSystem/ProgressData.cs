[System.Serializable]
public class ProgressData 
{
    private int _coins;
    private int _level;
    private bool _isMusicOn;
    //private float[] _backgroundColor;

    //public float[] BackgroundColor => _backgroundColor;
    public bool IsMusicOn => _isMusicOn;
    public int Coins => _coins;
    public int Level => _level;
    public ProgressData(Progress progress)
    {
        _coins = progress.Coins;
        _level = progress.Level;
        _isMusicOn = progress.IsMusicOn;

        //_backgroundColor = new float[3];
        //_backgroundColor[0] = progress.BackgroundColor.r;
        //_backgroundColor[1] = progress.BackgroundColor.g;
        //_backgroundColor[2] = progress.BackgroundColor.b;
    }
}
