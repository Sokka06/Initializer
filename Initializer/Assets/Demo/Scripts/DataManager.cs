using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Demo
{
    public interface IData { }
    
    /// <summary>
    /// A simple class for loading and saving data.
    /// </summary>
    public class DataManager : SingletonBehaviour<DataManager>
    {
        /// <summary>
        /// Loaded data.
        /// </summary>
        public Dictionary<string, IData> Data { get; private set; } = new Dictionary<string, IData>();
        
        public const string FOLDER_PATH = "data/";
        public const string EXTENSION_NAME = ".json";

        /// <summary>
        /// Loads data from file and returns it.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>() where T : IData, new()
        {
            //Check if already loaded
            var key = GetName<T>();
            if (IsLoaded<T>())
            {
                Debug.LogWarning($"Attempted to load data of type {key}, which is already loaded with the {GetType().Name}.");
                return Get<T>();
            }
            
            var data = FileHandler.Load<T>(FOLDER_PATH, BuildFileName(key));
            
            //Add to list of loaded data.
            Data.Add(key, data);
            return data;
        }
        
        /// <summary>
        /// Saves data to file.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public void Save<T>(T data) where T : IData
        {
            var key = GetName<T>();
            if (!IsLoaded<T>())
            {
                Debug.LogWarning($"Attempted to save data of type {key} which is not yet loaded with the {GetType().Name}.");
                return;
            }

            Data[key] = data;
            FileHandler.Save((T)Data[key], FOLDER_PATH, BuildFileName(key));
        }

        /// <summary>
        /// Unloads data from loaded data.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public void Unload<T>() where T : IData
        {
            var key = GetName<T>();
            if (!IsLoaded<T>())
            {
                Debug.LogWarning($"Attempted to unload data of type {key} which is not yet loaded with the {GetType().Name}.");
                return;
            }

            Data.Remove(key);
        }

        /// <summary>
        /// Gets loaded data. Be sure to load data before using.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IData
        {
            var key = GetName<T>();
            if (!IsLoaded<T>())
            {
                Debug.LogWarning($"{key} not registered with {GetType().Name}");
                return default;
            }
            
            return (T)Data[key];
        }

        /// <summary>
        /// Is data loaded.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsLoaded<T>() where T : IData
        {
            return Data.ContainsKey(GetName<T>());
        }
        
        private string GetName<T>() where T : IData
        {
            return typeof(T).Name;
        }

        private string BuildFileName(string dataName)
        {
            return $"{dataName}{EXTENSION_NAME}";
        }
    }
}