using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Bu (static) anahtar kelime, bu sınıf üyesinde saklanan değerlerin o sınıfın tüm örnekleri tarafından paylaşılacağı anlamına gelir.
    public static MainManager Instance;

    public Color teamColor;

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    // Rengi saklayan SaveData sınıfı 
    [System.Serializable]
    class SaveData
    {
        public Color teamColor; 
    }

    // Bu sınıfı JSON biçimine dönüştüren ve bir dosyaya yazan bir Save yöntemi.
    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.teamColor = teamColor;

        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // JSON dosyasındaki verileri tekrar SaveData sınıfına dönüştüren bir Load yöntemi.
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            teamColor = data.teamColor;
        }
    }

}
