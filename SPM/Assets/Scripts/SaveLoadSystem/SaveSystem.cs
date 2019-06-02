using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(GameController gameController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(gameController);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveEnemyData(Enemy enemy)
    {
        EnemyData data = new EnemyData(enemy);
        Debug.Log(data.EnemyName + " " + data.EnemyHealth + " " + data.EnemyPositionX + " " + data.EnemyRotationX);
        GameController.Instance.enemies.Add(data);
    }

    public static void WriteEnemyDataToFile(List<EnemyData> enemies)
    {

        DeleteSaveFile();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemies.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, enemies);
        stream.Close();

        GameController.Instance.enemies.Clear();
    }

    public static List<EnemyData> LoadEnemies()
    {

        string path = Application.persistentDataPath + "/enemies.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<EnemyData> enemies = formatter.Deserialize(stream) as List<EnemyData>;

            stream.Close();

            return enemies;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/enemies.sav";

        if (File.Exists(path))
        {
            File.Delete(path);
        }

    }
}
