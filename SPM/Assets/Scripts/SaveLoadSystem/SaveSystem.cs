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
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/enemies.sav";
        if (File.Exists(path) == false)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            EnemyData data = new EnemyData(enemy);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Append);
            EnemyData data = new EnemyData(enemy);

            formatter.Serialize(stream, data);
            stream.Close();
        }

    }

    public static EnemyData LoadEnemies()
    {
        string path = Application.persistentDataPath + "/enemies.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;

            stream.Close();

            return data;
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
