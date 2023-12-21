using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get { return instance == FindObject ? instance = FindObjectOfType<Player>() : instance; }
    }

    public bool InitFlagInventory { get; set; } = true;
    public bool InitFlagCharacters { get; set; } = true;

    [System.Serializable]
    public class PlayerData
    {
        public int Level { get; private set; }

        public int Exp { get; private set; }

        public int gold;
        public int fatigue;
        public int countNPC;
        public int indexNPC = -1;
        public int idNPCWeapon = -1;
        public int forgeLevel = 1;
        public int tempLevel = 1;
        public int hammerLevel = 1;
        public int handicraftLevel = 1;
        public int durability = 100;

        private int[] maxExp = new int[10] { 10, 20, 20, 20, 30, 30, 40, 40, 50, 50 };

        public int clearStage;
        public int storyScene;
        public bool[] storyPlayed = { false, false, false, false, false, false, false };

        public bool[] tutorialPlayed = new bool[8];

        public PlayerData(int level, int exp)
        {
            this.Level = level;
            this.Exp = exp;
            gold = 0;
            clearStage = 0;
            storyScene = 0;
        }

        public void SetExp(int _exp)
        {
            Exp = _exp;

            while (Exp > maxExp[Level])
            {
                Level++;
                Exp -= maxExp[Level];
            }
        }
    }
    
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public CharacterList characterList;
    [HideInInspector] public ForgeManager forgeManager;

    public PlayerData D_PlayerData { get; private set; }
    public int SelectThema { get; set; }
    public int SelectStage { get; set; }

    public int MaxNPC { get; private set; } = 5;
    public int MaxFatigue { get; private set; } = 3;

    public bool activeOption = false;

    // Save & Road
    private string[] fileNames = new string[3] { "PlayerData.json", "InventoryData.json", "CharactersData.json"};
    private string[] filePath = new string[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitFlagInventory = true;
        InitFlagCharacters = true;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        D_PlayerData = new PlayerData(1, 0);

        inventory = GetComponent<Inventory>();
        characterList = GetComponent<CharacterList>();
        forgeManager = GetComponent<ForgeManager>();
        inventory.Init();
        characterList.Init();

        SelectThema = 1;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < fileNames.Length; i++)
        {
            filePath[i] = sb.Append(Application.persistentDataPath).Append("/").Append(fileNames[i]).ToString();
            sb.Clear();
        }
        D_PlayerData.clearStage = 0;

        ResetNPC();
        ResetFatigue();
    }

    public bool LoadGameData()
    {
        InitFlagCharacters = false;
        InitFlagInventory = false;

        if (File.Exists(filePath[0]))
        {
            string fromJsonData = File.ReadAllText(filePath[0]);
            D_PlayerData = JsonUtility.FromJson<PlayerData>(fromJsonData);
        }
        else return false;

        if (File.Exists(filePath[1]))
        {
            string fromJsonData = File.ReadAllText(filePath[1]);
            inventory.bag = JsonUtility.FromJson<Inventory.Bag>(fromJsonData);
        }
        else return false;

        if (File.Exists(filePath[2]))
        {
            string fromJsonData = File.ReadAllText(filePath[2]);
            characterList.roster = JsonUtility.FromJson<CharacterList.Roster>(fromJsonData);
        }
        else return false;

        return true;
    }

    public void SaveGameData()
    {
        D_PlayerData.gold = GoldManager.Instance.CurrentGold;
        string toJsonDataPlayer = JsonUtility.ToJson(D_PlayerData, true);
        File.WriteAllText(filePath[0], toJsonDataPlayer);

        string toJsonDataInventory = JsonUtility.ToJson(inventory.bag, true);
        File.WriteAllText(filePath[1], toJsonDataInventory);

        string toJsonDataCharacters = JsonUtility.ToJson(characterList.roster, true);
        File.WriteAllText(filePath[2], toJsonDataCharacters);
    }

    public void ResetNPC()
    {
        D_PlayerData.countNPC = 0;
    }

    public void ResetFatigue()
    {
        D_PlayerData.fatigue = MaxFatigue;
    }
}
