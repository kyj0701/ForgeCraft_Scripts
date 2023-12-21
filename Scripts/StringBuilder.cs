using System.IO;
using UnityEngine;
using System.Text;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get { return instance == FindObject ? instance = FindObjectOfType<Player>() : instance; }
    }

    // Save & Road
    private string[] fileNames = new string[3] { "PlayerData.json", "InventoryData.json", "CharactersData.json" };
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
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < fileNames.Length; i++)
        {
            filePath[i] = sb.Append(Application.persistentDataPath).Append("/").Append(fileNames[i]).ToString();
            sb.Clear();
        }
    }
}
