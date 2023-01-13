using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class GameSettings : EditorWindow {
    [SerializeField] private VisualTreeAsset _tree;

    private FloatField AsteroidRotationSpeed;
    private IntegerField PlayerHealth;
    private EnumField ShipMode;
    private IntegerField AsteroidDamage;
    private EnumField AsteroidIncomingDirection;
    private Button CreateScriptableObject;
    private GameObject AsteroidPrefab;
    private MinMaxSlider AsteroidRotation;
    private MinMaxSlider AsteroidSize;

    private GameSettingsSO GsSO;
    
    private enum shipmode {
        normal,
        hyperspeed,
        rapidfire
    }
    
    private enum asteroidincomingdirection {
        left,
        right,
        top,
        down,
        random
    }

    private bool clicked = false;
    
    
    [MenuItem("Tools/GameSettings")]
    public static void ShowEditor() {
        var window = GetWindow<GameSettings>();
        window.titleContent = new GUIContent("Game Settings Tool");
    }
    
    private void CreateGUI() {
        _tree.CloneTree(rootVisualElement);
        InitializeFields();
    }

    private void Update() {
        if (clicked == false) {
            CreateScriptableObject.clickable.clicked += GenerateSettingsSO;
            clicked = true;
        }
    }

    private void GenerateSettingsSO() {
        if (!File.Exists("Assets/Resources/GameSettings.asset")) {
            GsSO = ScriptableObject.CreateInstance<GameSettingsSO>();
            AssetDatabase.CreateAsset(GsSO, "Assets/Resources/GameSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            SetGameSettings(GsSO);
            Selection.activeObject = GsSO;
        }
        else {
            Debug.Log("File exists! Changing its settings instead.");
            GsSO = Resources.Load("GameSettings") as GameSettingsSO;
            Selection.activeObject = GsSO;
            SetGameSettings(GsSO);
        }
    }

    private void SetGameSettings(GameSettingsSO gamesettingsasset) {
        EditorUtility.SetDirty(GsSO);
        gamesettingsasset.AsteroidMinRotation = AsteroidRotation.minValue;
        gamesettingsasset.AsteroidMaxRotation = AsteroidRotation.maxValue;
        gamesettingsasset.AsteroidMinSize = AsteroidSize.minValue;
        gamesettingsasset.AsteroidMaxSize = AsteroidSize.maxValue;
        gamesettingsasset.PlayerHealth = PlayerHealth.value;
        gamesettingsasset.AsteroidPrefab = AsteroidPrefab;
        gamesettingsasset.AsteroidDamage = AsteroidDamage.value;
        gamesettingsasset._playershipmode = GameSettingsSO.PlayerShipMode.hyperspeed;
        switch (ShipMode.value) {
            case shipmode.hyperspeed:
                gamesettingsasset._playershipmode = GameSettingsSO.PlayerShipMode.hyperspeed;
                break;
            case shipmode.normal:
                gamesettingsasset._playershipmode = GameSettingsSO.PlayerShipMode.normal;
                break;
            case shipmode.rapidfire:
                gamesettingsasset._playershipmode = GameSettingsSO.PlayerShipMode.rapidfire;
                break;
        }
        switch (AsteroidIncomingDirection.value) {
            case asteroidincomingdirection.down:
                gamesettingsasset._asteroidsincomingdirection = GameSettingsSO.AsteroidIncomingDirection.down;
                break;
            case asteroidincomingdirection.left:
                gamesettingsasset._asteroidsincomingdirection = GameSettingsSO.AsteroidIncomingDirection.left;
                break;
            case asteroidincomingdirection.random:
                gamesettingsasset._asteroidsincomingdirection = GameSettingsSO.AsteroidIncomingDirection.random;
                break;
            case asteroidincomingdirection.right:
                gamesettingsasset._asteroidsincomingdirection = GameSettingsSO.AsteroidIncomingDirection.right;
                break;
            case asteroidincomingdirection.top:
                gamesettingsasset._asteroidsincomingdirection = GameSettingsSO.AsteroidIncomingDirection.top;
                break;
        }
    }

    private void InitializeFields() {
        PlayerHealth = rootVisualElement.Q<IntegerField>("PlayerHealth");
        ShipMode = rootVisualElement.Q<EnumField>("PlayerShipMode");
        ShipMode.Init(shipmode.normal);
        ShipMode.value = shipmode.hyperspeed;
        ShipMode.value = shipmode.rapidfire;
        AsteroidRotation = rootVisualElement.Q<MinMaxSlider>("AsteroidRotationSpeed");
        AsteroidSize = rootVisualElement.Q<MinMaxSlider>("AsteroidSize");
        AsteroidDamage = rootVisualElement.Q<IntegerField>("AsteroidDamage");
        AsteroidIncomingDirection = rootVisualElement.Q<EnumField>("AsteroidIncomingDirection");
        AsteroidIncomingDirection.Init(asteroidincomingdirection.random);
        AsteroidIncomingDirection.value = asteroidincomingdirection.down;
        AsteroidIncomingDirection.value = asteroidincomingdirection.left;
        AsteroidIncomingDirection.value = asteroidincomingdirection.right;
        AsteroidIncomingDirection.value = asteroidincomingdirection.top;
        CreateScriptableObject = rootVisualElement.Q<Button>("CreateScriptableObject");
        var prefabInput = rootVisualElement.Q<ObjectField>("AsteroidPrefab");
        prefabInput.RegisterValueChangedCallback(evt => {
            AsteroidPrefab = evt.newValue as GameObject;
        });
    }
}
