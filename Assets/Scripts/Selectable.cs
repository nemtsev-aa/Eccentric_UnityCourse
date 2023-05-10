
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public GameObject canvas;

    public void Show(bool status)
    {
        canvas.SetActive(status);
        DownToTopMoving moving = gameObject.GetComponent<DownToTopMoving>();
        bool isStopped = moving.IsStopped;
        Direction direction = moving.CurrentDirection;

        if (isStopped)
        {
            if (direction == Direction.Top)
            {
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                canvas.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                canvas.transform.GetChild(0).gameObject.SetActive(false);
                canvas.transform.GetChild(1).gameObject.SetActive(true);

            }
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
