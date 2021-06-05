
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KOS.Factories
{
    abstract public class Factory : ScriptableObject
    {
        private Scene scene {get; set;}

        //> CREATE AND INSTANCE ON PREFAB
        protected T CreateInstance<T>(T prefab) where T : MonoBehaviour
        {
            if (!scene.isLoaded) scene = SceneManager.CreateScene(name);

            T instance = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(instance.gameObject, scene);
            return instance;
        }
    }
}
