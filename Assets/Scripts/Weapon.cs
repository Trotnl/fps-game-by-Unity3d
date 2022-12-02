using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiYu
{
    public class Weapon : MonoBehaviour
    {
        #region Variables
        public Gun[] loadout;
        public Transform weaponParent;
    
        private GameObject currentWeapon;
        #endregion

        #region Callbacks

        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Equip(0);
                }
        }
        #endregion

        #region Private Methods

        void Equip (int p_ind)
        {
            if (currentWeapon != null) Destroy(currentWeapon);
    
            GameObject t_newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            t_newWeapon.transform.localPosition = Vector3.zero;
            t_newWeapon.transform.localEulerAngles = Vector3.zero;
    
            currentWeapon = t_newWeapon;
        }
        #endregion
    }
}

