using System;
using UnityEngine;

using KOS.Events;
using KOS.Particles;
using KOS.ItemData;
using KOS.Damageables;
using KOS.Interactables;

namespace KOS.Towers
{
    [SelectionBase]
    abstract public class Tower : MonoBehaviour, IDataItem<TowerData>, IInteractable, IDamageable
    {
        //> FACTORIES
        [SerializeField] protected ParticleFactory particleFactory = default;
        internal TowerFactory originFactory;

       //> OBJECT STATS
        public TowerData Data => tower;
        [SerializeField] protected TowerData tower = default;
        public void SetData(TowerData data) => tower = data;

        //> INTERACTIONS
        public string interactionPrompt => "Edit";
        virtual public void InteractWith()
        {
            EventManager.Active.ToggleCursorLock();
            EventManager.Active.OpenTowerOptions(this);
        }

        [HideInInspector] public bool active;
        
        protected RangeIndicator rangeIndicator;
        protected float fireInterval;
        protected int current = 0;
        // public HexCell hexCell;

        //> POSITION HELPER
        public Vector3 position
        {   
            get => transform.position;
            set => transform.position = value;
        }

        public void TakeDamage(float amount, string origin)
        {
            // TODO allow towers to take damage, shouldn't be frustrating tho
        }

        //> INITIALIZATION
        virtual protected void Awake()
        {
            active = false;
            fireInterval = 60f;
            rangeIndicator = GetComponentInChildren<RangeIndicator>(true);
        }

        //> CHECK IF TOWER HAS TARGET AND CAN FIRE
        virtual protected void Update()
        {
            if (!active) return; // do nothing if inactive

            if (Math.Abs(rangeIndicator.scale - tower.range.value) > 0.0001f) rangeIndicator.scale = tower.range.value;
            
            // monitor fire rate
            if ((fireInterval += tower.fireRate.value * Time.deltaTime) >= 60f)
            {
                fireInterval = 0;
                Fire();
            }
        }

        //> ABSTRACT FIRE METHOD
        abstract protected void Fire();

        //> TOGGLE VISUAL IN-GAME RANGE INDICATOR
        public void ToggleRange() => rangeIndicator.active = !rangeIndicator.active;

        //> DRAW TOWER RANGE
        virtual protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, tower.range.value);
        }
    }
}

