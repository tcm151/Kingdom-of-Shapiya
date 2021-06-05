using UnityEngine;

using KOS.Audio;
using KOS.Events;
using KOS.HexMap;

namespace KOS.Towers
{
    public class BuildManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TowerFactory factory = default;
        [SerializeField] private Camera view;
        
        private Tower ghostTower;
        private TowerType currentTower;
        private TowerData stats;
        
        private HexGrid hexGrid;
        private HexCell currentCell;

        [Header("Ghost Materials")]
        [SerializeField] private Material allowedMaterial;
        [SerializeField] private Material deniedMaterial;

        [Header("Booleans")]
        public bool buildMode = false;
        public bool towerSelectionUIActive = false;
        public bool placingTower = false;

        //> INITIALIZE
        private void Awake()
        {
            hexGrid = FindObjectOfType<HexGrid>();

            // register for events
            EventManager.Active.onPlacingTower += PlacingTower;
        }
        
        //> HANDLE INPUT
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B) && (buildMode || Cursor.lockState == CursorLockMode.Locked))
            {
                if (placingTower)
                {
                    if (ghostTower) factory.Reclaim(ghostTower); // cleanup ghost
                    
                    EventManager.Active.StoppedPlacingTower();
                    placingTower = false;
                    buildMode = false;
                    return;
                }

                currentCell = null; // allows ghost to appear if build mode toggled on same cell
                buildMode = !buildMode;
                EventManager.Active.ToggleBuildMenu();
                EventManager.Active.ToggleCursorLock();
                
                if (ghostTower) factory.Reclaim(ghostTower); // cleanup ghost
                
            }

            if (Cursor.lockState != CursorLockMode.Locked)
            {
                if (ghostTower) factory.Reclaim(ghostTower); // cleanup ghost
                return;
            }

            if (placingTower) Build();
        }

        //> BUILD TOWERS
        // ReSharper disable Unity.PerformanceAnalysis
        private void Build()
        {
            HexCell newCell = GetHoveredCell();

            if (!newCell) // not hovering cell
            {
                // clean up old cell
            }
            else if (newCell != currentCell) // hovering new cell
            {
                if (ghostTower) factory.Reclaim(ghostTower); // cleanup ghost
                
                if (!newCell.occupied) // handle new cell placement
                {
                    Material ghostMaterial;
                    ghostTower = factory.Get(currentTower);
                    ghostTower.position = newCell.transform.position;
                    ghostMaterial = (Bank.Connect.HasBalance(ghostTower.Data.buildCost)) ? allowedMaterial : deniedMaterial;
                    ghostMaterial = (newCell.Buildable) ? ghostMaterial : deniedMaterial;
                    
                    foreach (var mesh in ghostTower.GetComponentsInChildren<MeshRenderer>())
                    {
                        mesh.material = ghostMaterial;
                    }
                }
            }
            
            currentCell = newCell;
            
            if (Input.GetMouseButtonDown(0))
            {

                if (!newCell.Buildable)
                {
                    Debug.LogWarning("Cell not buildable!");
                    return;
                }

                if (!newCell)
                {
                    Debug.LogWarning("Cell not set!");
                    return;
                }

                if (newCell.occupied)
                {
                    Debug.Log("Cell is occupied!");
                    return;
                }

                stats = factory.GetStats(currentTower);
                if (!Bank.Connect.HasBalance(stats.buildCost))
                {
                    AudioManager.Active.PlayOneShot("towerDeny");
                    Debug.LogWarning("Not enough cash to build!");
                    return;
                }
                    
                // place a new tower
                Bank.Connect.Charge(stats.buildCost);
                Tower newTower = factory.Get(currentTower);
                newTower.position = newCell.transform.position;
                newTower.active = true;
                newCell.occupied = true;
                // newTower.hexCell = newCell;
                EventManager.Active.TowerBuilt(currentTower);
                AudioManager.Active.PlayAtPoint("towerBuild", newTower.position);

                // turn on meshes
                Collider[] colliders = newTower.GetComponentsInChildren<Collider>();
                foreach (var collider in colliders) collider.enabled = true;
                
                if (ghostTower) factory.Reclaim(ghostTower); // cleanup ghost
            }

        }

        //> GET PROPER HEX TILE
        private HexCell GetHoveredCell()
        {
            Ray inputRay = view.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(inputRay, out RaycastHit hit, 12f))
            {
                return hexGrid.GetCell(hit.point);
            }
            else
            {
                if (ghostTower) factory.Reclaim(ghostTower); // cleanup ghost
                return null;
            }
        }

        //> BEGIN PLACING GHOST & REAL TOWERS
        public void PlacingTower(TowerType newTower)
        {
            placingTower = true;
            currentTower = newTower;
            EventManager.Active.ToggleCursorLock();
        }
    }
}