using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiYu
{

    public class Sway : MonoBehaviour
    {
        #region Variables

        public float intensity;
        public float smooth;

        private Quaternion origin_rotation;

        #endregion

        #region Callbacks

        void Start()
        {
            origin_rotation = transform.localRotation;
        }

        private void Update()
        {
            UpdateSway();
        }
        #endregion

        #region Private Methods

        private void UpdateSway()
        {
            //controls
            float t_x_mouse = Input.GetAxis("Mouse X");
            float t_y_mouse = Input.GetAxis("Mouse Y");

            //calculate target rotation
            Quaternion t_xadj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
            Quaternion t_yadj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
            Quaternion t_rotation = origin_rotation * t_xadj * t_yadj;

            //rotate towards target rotation
            transform.localRotation = Quaternion.Lerp(transform.localRotation, t_rotation, Time.deltaTime * smooth);
        }
        #endregion
    }

}