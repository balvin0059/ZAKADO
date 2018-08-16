using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class SaveLoadData
{
    public const string SAVE_NAME = "PlayerSave";
    public static GameSave LoadData()
    {
        GameSave result = new GameSave();
        if (PlayerPrefs.HasKey(SAVE_NAME))
        {
            string saveString = PlayerPrefs.GetString(SAVE_NAME);

            try
            {
                saveString = System.Text.Encoding.Unicode.GetString(System.Convert.FromBase64String(saveString));
                XmlSerializer ser = new XmlSerializer(typeof(GameSave));
                StringReader sr = new StringReader(saveString);
                result = ser.Deserialize(sr) as GameSave;
            }
            catch(System.Exception e)
            {
                Debug.LogError(e.Message);
                return new GameSave();
            }
        }
        return result;
    }
    public static void SaveData(GameSave saveData)
    {
        XmlSerializer ser = new XmlSerializer(typeof(GameSave));
        StringWriter sw = new StringWriter();
        ser.Serialize(sw, saveData);
        string saveString = sw.ToString();
        try
        {
            saveString = System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(saveString));
        }
        catch(System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        try
        {
            PlayerPrefs.SetString(SAVE_NAME, saveString);
        }
        catch (PlayerPrefsException err)
        {
            Debug.Log("Got:" + err);
        }
        PlayerPrefs.Save();
        Debug.Log("--- Saving Succed ... ---      StringLen = " + saveString.Length);
    }

}

