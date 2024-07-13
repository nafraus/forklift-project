using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace SqdthUtils.PrefabSpawner.Editor
{
    public class Spawner : EditorWindow
    {
        [SerializeField] private VisualTreeAsset tree;
        
        private ObjectField spawnablePrefab;
        private ObjectField parentTransform;
        private LayerMaskField raycastLayerMask;
        private Vector3Field minRotation;
        private Vector3Field maxRotation;
        private ToolbarToggle alignToNormals;
        private ToolbarToggle spawnAsPrefab;
        private ToolbarToggle gridSnapped;
        private Toggle active;

        [MenuItem("Tools/Prefab Spawner")]
        public static void CreateWindow()
        {
            var window = GetWindow<Spawner>();
            window.titleContent = new GUIContent("Spawner");
        }

        private void CreateGUI()
        {
            tree.CloneTree(rootVisualElement);
            InitFields();
            
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void InitFields()
        {
            spawnablePrefab =
                rootVisualElement.Q<ObjectField>("SpawnablePrefab");
            raycastLayerMask = 
                rootVisualElement.Q<LayerMaskField>("RaycastLayerMask");
            parentTransform = rootVisualElement.Q<ObjectField>("ParentTransform");
            minRotation = rootVisualElement.Q<Vector3Field>("MinRotation");
            maxRotation = rootVisualElement.Q<Vector3Field>("MaxRotation");
            alignToNormals = rootVisualElement.Q<ToolbarToggle>("SnapToGrid");
            spawnAsPrefab = rootVisualElement.Q<ToolbarToggle>("SpawnAsPrefab");
            gridSnapped = rootVisualElement.Q<ToolbarToggle>("SnapToGrid");
            active = rootVisualElement.Q<Toggle>("Active");
        }

        private void OnGUI()
        {
            if (spawnablePrefab == null) return;
            bool spawnablePopulated = spawnablePrefab.value != null;
            if (spawnablePopulated)
            {
                if (active.style.display != DisplayStyle.Flex)
                    active.style.display = DisplayStyle.Flex;
                
                if (PrefabUtility.IsPartOfPrefabAsset(spawnablePrefab.value))
                {
                    if (spawnAsPrefab.style.display != DisplayStyle.Flex)
                        spawnAsPrefab.style.display = DisplayStyle.Flex;
                }
                else
                {
                    if (spawnAsPrefab.value) spawnAsPrefab.value = false;
                    if (spawnAsPrefab.style.display != DisplayStyle.None)
                        spawnAsPrefab.style.display = DisplayStyle.None;
                }
            }
            else
            {
                if (active.value) active.value = false;
                active.style.display = DisplayStyle.None;
                
                if (spawnAsPrefab.value) spawnAsPrefab.value = false;
                if (spawnAsPrefab.style.display != DisplayStyle.None)
                    spawnAsPrefab.style.display = DisplayStyle.None;
            }
        }

        private void OnSceneGUI(SceneView obj)
        {
            if (!active.value)
            {
                return;
            }

            Event eventCurrent = Event.current;
            
            // If left mouse down
            if (eventCurrent.type == EventType.MouseDown &&
                eventCurrent.button == 0)
            {
                RaycastSpawn(eventCurrent);
            }
        }

        private void RaycastSpawn(Event eventCurrent)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(eventCurrent.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,
                raycastLayerMask.value);

            if (hit.collider)
            {
                GameObject go;
                Transform parent = (Transform)parentTransform.value;
                if (spawnAsPrefab.value)
                {
                    go = SpawnAsPrefab(parent);
                }
                else
                {
                    go = SpawnAsGameObject(parent);
                }
                
                // Set position
                go.transform.position = 
                    gridSnapped.value ? 
                        Snapping.Snap(hit.point + hit.normal * .5f, EditorSnapSettings.move) : 
                        hit.point;
                
                // Set random rotation
                Vector3 offset = new Vector3(
                    Random.Range(minRotation.value.x, maxRotation.value.x),
                    Random.Range(minRotation.value.y, maxRotation.value.y),
                    Random.Range(minRotation.value.z, maxRotation.value.z));
                    
                if (alignToNormals.value)
                {
                    // Set random rotation based on normals
                    go.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal + offset);
                }
                else
                {
                    // Set random rotation based on default rotation
                    go.transform.rotation = Quaternion.Euler(Vector3.zero + offset);
                }
            }
        }

        private GameObject SpawnAsGameObject(Transform parent = null)
        {
            // Instantiate prefab
            GameObject go = (GameObject)Instantiate(spawnablePrefab.value, parent);
                // this would normally be of type Object not GameObject

            return go;
        }
        
        private GameObject SpawnAsPrefab(Transform parent = null)
        {
            // Instantiate prefab
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(spawnablePrefab.value, parent);
                // this would normally be of type Object not GameObject

            return go;
        }
    }
}
