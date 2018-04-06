using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace JSONSaveLoad
{
    /// <summary>
    /// 
    /// For Help:
    ///     https://github.com/SAEBrisbaneGitHub/18T1_S1_T1/blob/master/README.md
    ///     
    /// </summary>
    public static class SaveLoadManager
    {
        /// <summary>
        /// 
        /// Saves data to a file
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_fileName">Name of the file."</param>
        /// <param name="_dataToSave">Class/Struct to save.</param>
        public static void SaveFile<T>(string _fileName, T _dataToSave)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath + "/JSONFiles" + "\\", _fileName + ".json");

            string savingData = JsonUtility.ToJson(_dataToSave);
            try
            {
                File.WriteAllText(filePath, savingData);
                Debug.Log("SAVED");
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }


        ///<summary>
        /// Loads data from a saved JSON file to a parameter
        /// </summary>
        public static void LoadFile<T>(string _fileName, T _dataToLoad)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath + "/JSONFiles" + "\\", _fileName + ".json");

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(dataAsJson,_dataToLoad);
                Debug.Log("LOADED");
            }
            else
            {
                Debug.LogError("FILE LOAD FAILED  -  USING DEFAULT VALUES");
            }
        }
    }
}
