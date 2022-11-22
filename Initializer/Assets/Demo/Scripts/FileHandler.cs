using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// A simple class for saving data to a JSON file on disk.
    /// NOTE: Change Application.dataPath to Application.persistentDataPath in GetSavePath() if you want to use this outside editor.
    /// </summary>
    public static class FileHandler
    {
        /// <summary>
        /// Loads data from a file.
        /// </summary>
        /// <param name="path">Relative path where file is located, e.g. "data/"</param>
        /// <param name="file">Name of file with extension, e.g. "file.json".</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>(string path, string file) where T : new()
        {
            var filePath = GetFilePath(path, file);
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"No file found for '{file}', returned empty file!");
                return new T();
            }

            return Deserialize<T>(filePath);
        }

        /// <summary>
        /// Saves data to a file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path">Relative path where data file will be saved, e.g. "data/"</param>
        /// <param name="file">Name of file with extension, e.g. "file.json".</param>
        /// <typeparam name="T"></typeparam>
        public static void Save<T>(T data, string path, string file)
        {
            // Make sure folder exists
            var folderPath = GetFolderPath(path);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            var filePath = GetFilePath(path, file);
            
            Serialize(data, filePath);
        }

        #region Path Handling

        /// <summary>
        /// Path to folder where the file will be saved.
        /// </summary>
        /// <param name="path">Relative path, e.g. "data/".</param>
        /// <returns></returns>
        public static string GetFolderPath(string path)
        {
            return $"{Application.dataPath}/{path}";
        }

        /// <summary>
        /// Full path to file.
        /// </summary>
        /// <param name="path">Relative path, e.g. "data/".</param>
        /// <param name="file">Name of file with extension, e.g. "file.json".</param>
        /// <returns></returns>
        public static string GetFilePath(string path, string file)
        {
            return $"{GetFolderPath(path)}{file}";
        }
        #endregion

        #region Private file handling

        /// <summary>
        /// Serializes data to a file.
        /// </summary>
        /// <param name="data">Data that will be serialized to a file.</param>
        /// <param name="filePath">Full file path.</param>
        /// <typeparam name="T"></typeparam>
        private static void Serialize<T>(T data, string filePath)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Deserializes data from a file.
        /// </summary>
        /// <param name="filePath">Full path to file that will be Deserialized to an object.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T Deserialize<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        /// <summary>
        /// Deletes file from path.
        /// </summary>
        /// <param name="filePath">Full path to file that will be deleted.</param>
        private static void Delete(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Can't Delete '{filePath}', No File Was Found!");
                return;
            }

            File.Delete(filePath);
        }
        #endregion
    }
}