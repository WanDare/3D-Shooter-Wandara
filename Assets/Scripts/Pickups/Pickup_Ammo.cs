using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using RangeAttribute = UnityEngine.RangeAttribute;


[System.Serializable]
public struct AmmoData
{
    public WeaponType weaponType;
    [Range(10, 100)] public int minAmount;
    [Range(10, 100)] public int maxAmount;
}

public enum AmmoBoxType { smallBox,bigBox }
public class Pickup_Ammo : Interactable
{
    [SerializeField] private AmmoBoxType boxType;





    [SerializeField] private List<AmmoData> smallBoxAmmo;
    [SerializeField] private List<AmmoData> bigBoxAmmo;

    [SerializeField] private GameObject[] boxModel;
    private void Start() => SetupBoxModel();

    public override void Interaction()
    {
        List<AmmoData> currenAmmoList = smallBoxAmmo;

        if(boxType == AmmoBoxType.bigBox)
            currenAmmoList = bigBoxAmmo;

        foreach(AmmoData ammo in currenAmmoList)
        {
            Weapon weapon = weaponController.WeaponInSlots(ammo.weaponType);
            AddBulletsToWeapon(weapon, GetBulletAmount(ammo));
        }

        ObjectPool.instance.ReturnObject(gameObject);
    }


    private int GetBulletAmount(AmmoData ammoData)
    {
        float min = Mathf.Min(ammoData.minAmount, ammoData.maxAmount);
        float max = Mathf.Max(ammoData.minAmount, ammoData.maxAmount);

        float randomAmmoAmount = Random.Range(min, max);

        return Mathf.RoundToInt(randomAmmoAmount);
    }
    private void AddBulletsToWeapon(Weapon weapon, int amount)
    {
        if (weapon == null)
            return;

        weapon.totalReserveAmmo += amount;
    }

    private void SetupBoxModel()
    {
        for (int i = 0; i < boxModel.Length; i++)
        {
            if (i == ((int)boxType))
            {
                boxModel[i].SetActive(true);
                UpdateMeshAndMaterial(boxModel[i].GetComponent<MeshRenderer>());
            }
        }
    }

}
