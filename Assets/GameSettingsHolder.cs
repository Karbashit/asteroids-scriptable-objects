using UnityEngine;
using UnityEngine.Windows;

public class GameSettingsHolder : MonoBehaviour {
    public GameSettingsSO GameSettingsSo;
    public static GameSettingsHolder GameSettings = null;

    private void Awake() {
        if (GameSettings == null)
            GameSettings = this;
        else if (GameSettings != this) {
            Destroy(gameObject);
        }
        if (File.Exists("Assets/Resources/GameSettings.asset")) {
            GameSettingsSo = Resources.Load("GameSettings") as GameSettingsSO;
        }
        else {
            Debug.LogError("Game Settings Asset Doesn't Exist! Navigate to Tools -> GameSettings to create one.");
        }
    }


}
