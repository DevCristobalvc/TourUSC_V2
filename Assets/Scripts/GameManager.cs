using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer sphereRenderer;
    [SerializeField] private SceneContainer[] containers;
    [SerializeField] private GameObject startMenu;

    private string currentSphere;
    private SceneContainer currentScene;

    public SceneContainer GetSceneContainer(string id) => containers.FirstOrDefault(c => c.sceneId == id);
    public bool SceneExists(string id) => containers.Any(c => c.sceneId == id);

    public SphereContainer GetSphereContainer(SceneContainer scene, string id) =>
        scene.container.FirstOrDefault(c => c.id == id);

    public bool SphereExists(SceneContainer scene, string id) => scene.container.Any(c => c.id == id);
    
    public void SelectScene(string id)
    {
        if (!SceneExists(id)) return;

        startMenu.SetActive(false);
        currentScene = GetSceneContainer(id);
        sphereRenderer.gameObject.SetActive(true);
        SelectSphere(currentScene.container[0].id);
    }

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

[System.Serializable]
public class SceneContainer
{
    public string sceneId;
    public SphereContainer[] container;
}

[System.Serializable]
public class SphereContainer
{
    public string id;
    public Material sphereMaterial;
    public GameObject[] sphereObjects;
}
