using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaveManager
{
    public static void SavePlayer(CharacterScript player)
    {
        // open binaryFormatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // open filestreem. tell it two things where it is going to be saving this file and how you want to be opening it
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/player.save", FileMode.Create);

        PlayerData data = new PlayerData(player);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static int[] LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/player.save", FileMode.Create);
            PlayerData data = binaryFormatter.Deserialize(fileStream) as PlayerData;
            
            fileStream.Close();

            return data.statsOne;
            
        }
        Debug.LogError("File does not exist");
        return new int[3];
    }
}

[Serializable]
    public class PlayerData
    {
        public int[] statsOne;
        public float[] statsTwo;
        public PlayerData(CharacterScript player)
        {
            statsOne = new int[3];
            statsOne[0] = player.health;
            statsOne[1] = player.maxHealth;
            statsOne[2] = player.damage;
        }
    }
    

