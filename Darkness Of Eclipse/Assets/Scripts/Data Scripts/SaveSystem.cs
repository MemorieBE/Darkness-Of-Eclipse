using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*! \brief A class that saves and loads the game data to and from a file.
 *
 *  [Data Script]
 */
public static class SaveSystem
{
    /*!
     *  A method that saves the game data to a file.
     */
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/gamedata.dta";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /*!
     *  A method that loads the game data from a file.
     */
    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.dta";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("No save file to load [" + path + "]" );
            return null;
        }
    }
}
