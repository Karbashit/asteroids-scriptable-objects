using System;
using UnityEngine;

namespace Ship
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Laser _laserPrefab;
        private float count;

        private void Update() {
            count += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && count > 0.5f) {
                Shoot();
                count = 0;
            }
            else if (GameSettingsHolder.GameSettings.GameSettingsSo._playershipmode ==
                     GameSettingsSO.PlayerShipMode.rapidfire) {
                if (Input.GetKey(KeyCode.Space)) {
                    Shoot();
                }
            }
        }
        
        private void Shoot()
        {
            var trans = transform;
            Instantiate(_laserPrefab, trans.position, trans.rotation);
        }
    }
}
