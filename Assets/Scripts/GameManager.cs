using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variable de la Esfera
    [SerializeField] private MeshRenderer sphereRenderer;
    //Array que contiene las "Escenas"
    [SerializeField] private SceneContainer[] containers;
    //Plataforma de Inicio
    [SerializeField] private GameObject startMenu;

    //Identificador del momento actual del Recorrido
    private string currentSphere;
    //Identificador de la escena actual
    private SceneContainer currentScene;

    //Funcion que retorna una Escena basado en su id
    public SceneContainer GetSceneContainer(string id) => containers.FirstOrDefault(c => c.sceneId == id);
    
    //Funcion que retorna si existe una Escena con el ID dado
    public bool SceneExists(string id) => containers.Any(c => c.sceneId == id);

    //Funcion que retorna un momento del Recorrido basado en la escena y el ID dado
    public SphereContainer GetSphereContainer(SceneContainer scene, string id) =>
        scene.container.FirstOrDefault(c => c.id == id);

    //Funcion que retorna si un momento del Recorrido existe basado en su escena y el ID dado
    public bool SphereExists(SceneContainer scene, string id) => scene.container.Any(c => c.id == id);
    
    //Funcion para cambiar la Escena con un ID
    public void SelectScene(string id)
    {
        if (!SceneExists(id)) return;

        startMenu.SetActive(false);
        currentScene = GetSceneContainer(id);
        sphereRenderer.gameObject.SetActive(true);
        SelectSphere(currentScene.container[0].id);
    }

    //Funcion para cambiar el momento del Recorrido con su ID
    public void SelectSphere(string id)
    {
        if (!SphereExists(currentScene, id)) return;
        
        var currSc = GetSphereContainer(currentScene, currentSphere);
        foreach (var go in currSc.sphereObjects) go.SetActive(false);
            
        var newSc = GetSphereContainer(currentScene, id);
        currentSphere = id;
        sphereRenderer.material = newSc.sphereMaterial;
        foreach (var go in newSc.sphereObjects) go.SetActive(true);
    }
}

/*
    Clase que contiene las Escenas. Una Escena es considerada como uno de los recorridos.
    Ejemplo: Una escena es el Bloque 1, que dentro de si contiene los momentos o partes del recorrido
 */
[System.Serializable]
public class SceneContainer
{
    //Identificador de la Escena
    public string sceneId;
    //Array de momentos/partes del recorrido
    public SphereContainer[] container;
}

/*
    Clase que contiene los Momentos/Partes del recorrido. Estas son todas las imagenes o partes
    del recorrido que se hace
 */
[System.Serializable]
public class SphereContainer
{
    //Identificador del Momento/Parte
    public string id;
    //Identificador de la Textura/Material
    public Material sphereMaterial;
    //Botones de Recorrido a Activar
    public GameObject[] sphereObjects;
}
