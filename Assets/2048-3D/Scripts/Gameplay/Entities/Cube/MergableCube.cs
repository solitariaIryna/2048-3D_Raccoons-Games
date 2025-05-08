using DG.Tweening;
using G2048_3D.Configs.Cube;
using G2048_3D.Gameplay.Core;
using G2048_3D.Helpers;
using G2048_3D.Pool;
using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Entities.Cube
{ 
    public class MergableCube : MonoBehaviour, IPushableProvider, IResetable, IReleaseable<MergableCube>, ICoroutineRunner
    {
        [field: SerializeField] public MergableCubeVisual Visual { get; private set; }
        [SerializeField] private DirectionMovementPusher _directionPusher;
        [SerializeField] private float _pushUpForce = 12;
        [SerializeField] private CubeConfig _config;
        public NonAllocEvent<MergableCube> Released { get; private set; } = new NonAllocEvent<MergableCube>();

        private MergeCollisionHandler _collisionHandler;

        private MergableCubeData _data;
        public IPushable Pushable => _directionPusher;
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public MergableCubeData Data => _data;
        public int Level => Data.Level;
        public bool IsPushableTarget { get; set; }

        public Action<MergableCube> PushCompleted;
        public Action<MergableCube, MergableCube> CanBeMerged;

        public bool CanMerge => _collisionHandler.CanMerge;
    
        public void Awake()
        {
            _directionPusher.Construct(this);      
            _collisionHandler = new MergeCollisionHandler(this);    
        }

        private void OnPushed() => 
            Visual.EnableDirectionLine(false);

        private void OnEnable()
        {
            Pushable.PushCompleted += OnPushCompleted;
            Pushable.Pushed += OnPushed;
            _collisionHandler.StartMergeDelay();
        }
        private void OnDisable()
        {
            Pushable.PushCompleted -= OnPushCompleted;
            Pushable.Pushed -= OnPushed;
        }

        public void PushShow()
        {
            Vector3 pushDirection = UnityEngine.Random.insideUnitSphere;
            pushDirection.y = 1;
            pushDirection.z = Mathf.Abs(pushDirection.z);

            Pushable.Push(pushDirection * _pushUpForce);
            SimpleShow();
        }
        public void SimpleShow()
        {
            Visual.Show();
           
        }
        private void OnPushCompleted() => 
            PushCompleted?.Invoke(this);

        private void Update()
        {
            Data.Position = transform.position;
        }
        public void SetData(MergableCubeData data)
        {
            _data = data;
            transform.position = data.Position;        
            Visual.SetNumber(_data.Level);
            Visual.SetColor(_config.GetColorForNumber(Level));       
            Visual.EnableDirectionLine(IsPushableTarget);
           
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (_collisionHandler.TryHandleCollision(collision, out MergableCube mergableCube))
            {
                if (CanMerge && mergableCube.CanMerge)
                {
                    Taptic.Vibrate();
                    CanBeMerged?.Invoke(this, mergableCube);
                }
                
            }

        }

        public void Release()
        {
            _directionPusher.ResetPhysics();
            transform.eulerAngles = Vector3.zero;
            PushCompleted?.Invoke(this);
            Released?.Invoke(this);
        }

        public void ResetData()
        {
            Visual.ResetData();
            _directionPusher.ResetPhysics();
            transform.eulerAngles = Vector3.zero;
            Visual.SetColor(_config.GetColorForNumber(2));

        }
    }
}
