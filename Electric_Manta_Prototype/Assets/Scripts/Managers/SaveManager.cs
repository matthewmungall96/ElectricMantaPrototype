using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SaveFile(GameManager gm)
    {
        //we create a new BinaryFormatter.
        BinaryFormatter formatter = new BinaryFormatter();

        //set a destination path and file name with our own file type.
        string path = Application.persistentDataPath + "/electric_manta.test";

        //create a new Filestream.
        FileStream stream = new FileStream(path, FileMode.Create);

        //create data which we'll call via the Save_File class.
        SaveFile data = new SaveFile(gm);

        //we serialize the data via our formatter.
        formatter.Serialize(stream, data);

        //we close the stream to stop any possible glitches.
        stream.Close();

        Debug.Log("Saved File Successfully");
    }

    public static SaveFile LoadPlayerFile(GameManager gm)
    {
        //we recall our path from earlier for loading.
        string path = Application.persistentDataPath + "/electric_manta.test";

        //if the file is detected by the Save Manager.
        if (File.Exists(path))
        {
            //we create a new BinaryFormatter.
            BinaryFormatter formatter = new BinaryFormatter();

            //create a new Filestream.
            FileStream stream = new FileStream(path, FileMode.Open);

            //we will load the variables from the save file using our Save_File class.
            SaveFile data = formatter.Deserialize(stream) as SaveFile;

            //we close the stream to stop any possible glitches.
            stream.Close();

            Debug.Log("Save File Loaded");
            return data;
        }

        //if the file is NOT detected by the Save Manager.
        //we will create a new file using the same method in the SavePlayer function.
        else
        {
            Debug.Log("Error");
            BinaryFormatter formatter = new BinaryFormatter();

            path = Application.persistentDataPath + "/electric_manta.test";
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveFile data = new SaveFile(gm);

            formatter.Serialize(stream, data);
            stream.Close();
            Debug.Log("Created New Save");
            LoadPlayerFile(gm);
            return data;
        }
    }
}
