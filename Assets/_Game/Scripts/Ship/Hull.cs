using System;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;

namespace Ship
{
    public class Hull : MonoBehaviour
    {
        //[SerializeField] private IntVariable _health;
        [SerializeField] private ScriptableEventIntReference _onHealthChangedEvent;
        [SerializeField] private IntReference _healthRef;
        [SerializeField] private IntObservable _healthObservable;


        private void Start() {
            _healthRef.SetValue(GameSettingsHolder.GameSettings.GameSettingsSo.PlayerHealth);
            _healthObservable.SetValue(GameSettingsHolder.GameSettings.GameSettingsSo.PlayerHealth);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.Equals(other.gameObject.tag, "Asteroid"))
            {
                Debug.Log("Hull collided with Asteroid");
                // TODO can we bake this into one call?
                //_healthRef.ApplyChange(-1);
                //_onHealthChangedEvent.Raise(_healthRef);
                _healthObservable.ApplyChange(-GameSettingsHolder.GameSettings.GameSettingsSo.AsteroidDamage);
            }
        }
    }
}
