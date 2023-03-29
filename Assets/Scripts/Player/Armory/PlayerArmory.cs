using UnityEngine;
using UnityEngine.UI;

public class PlayerArmory : MonoBehaviour
{
    [Tooltip("Список оружия")]
    public Gun[] Guns;
    public Toggle[] GunsIcon;
    public int CurrentGunIndex;

    void Start()
    {
        TakeGunByIndex(CurrentGunIndex);
    }

    private void Update()
    {
        //int gunIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentGunIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentGunIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentGunIndex = 2;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") >= 0.1f)
        {
            if (CurrentGunIndex == Guns.Length - 1)
                CurrentGunIndex = 0;
            else
                CurrentGunIndex++;
            //GunsIcon[CurrentGunIndex].isOn = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.1f)
        {
            if (CurrentGunIndex == 0)
                CurrentGunIndex = Guns.Length - 1;
            else
                CurrentGunIndex--;
            //GunsIcon[CurrentGunIndex].isOn = true;
        }

        TakeGunByIndex(CurrentGunIndex);
        GunsIcon[CurrentGunIndex].isOn = true;
    }

    public void TakeGunByIndex(int gunIndex)
    {
        CurrentGunIndex = gunIndex;
        for (int i = 0; i < Guns.Length; i++)
        {
            Gun iGun = Guns[i];
            if (i == gunIndex)
            {
                iGun.Activate();
            }
            else
            {
                iGun.Deactivate();
            }
        }
    }

    public void AddBullets(int gunIndex, int numberOfBullets)
    {
        Guns[gunIndex].AddBullets(numberOfBullets);
    }
}
