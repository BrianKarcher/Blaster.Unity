//using Rewired;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Camera;
//using BlueOrb.Physics.Helpers;
//using BlueOrb.Controller.Player;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using PlayerController = BlueOrb.Controller.Player.PlayerController;

//namespace BlueOrb.Scripts.AI.AtomActions.Camera
//{
//    public class ChooseLockTargetAtom : AtomActionBase
//    {
//        public string _targetLockAction;
//        public float _clickNextTargetTime = 1f;

//        private LayerMask _targetLockLayers;
//        private string[] _tags;

//        private ThirdPersonCameraController _camera;
//        private Action _centerAction;
//        private Action _lockOn;

//        //private Collider[] hitResults = new Collider[10];
//        //private List<GameObject> autoLockableObjects = new List<GameObject>();

//        private Dictionary<int, GameObject> tempGameObjectDict = new Dictionary<int, GameObject>();
//        private PlayerController _playerController;
//        private IEntity _mainCharacter;
//        private Rewired.Player _rewiredPlayer;

//        private List<GameObject> _lockList = null;
//        private int _lockListIndex = 0;
//        private float _lastLockReleaseTime;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_mainCharacter == null)
//                _mainCharacter = EntityContainer.Instance.GetMainCharacter();
//            _playerController = _mainCharacter.Components
//                .GetComponent<PlayerController>();
//            if (_camera == null)
//                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
//            if (_rewiredPlayer == null)
//                _rewiredPlayer = ReInput.players.GetPlayer(0);
//            _lastLockReleaseTime = Time.time;
//        }

//        public void Update()
//        {
//            if (_rewiredPlayer.GetButtonDown(_targetLockAction))
//            {
//                Debug.Log("Target Lock button pressed");
//                GameObject lockGO = null;
//                // Determine whether to get the next target, or to create the list and get the first target.

//                // Button pressed within the alotted time? Select the next target in the previously generated list
//                if (_lockList != null && _lockList.Count != 0 && Time.time - _lastLockReleaseTime < _clickNextTargetTime)
//                {
//                    lockGO = GetIncrementTargetLock();
//                }
//                else
//                {
//                    _lockList = (List<GameObject>)CreateTargetList();
//                    if (_lockList != null && _lockList.Count != 0)
//                    {
//                        _lockListIndex = 0;
//                        lockGO = _lockList[_lockListIndex];
//                    }
//                }
//                TargetLockPressed(lockGO);
//            }
//        }

//        private GameObject GetIncrementTargetLock()
//        {
//            if (_lockList == null)
//                return null;
//            _lockListIndex++;
//            if (_lockListIndex > _lockList.Count - 1)
//                _lockListIndex = 0;
//            return _lockList[_lockListIndex];
//        }

//        public void TargetLockPressed(GameObject lockGameObject)
//        {
//            if (_camera == null)
//                _camera = UnityEngine.Camera.main.GetComponentInParent<ThirdPersonCameraController>();

//            if (lockGameObject == null)
//            {
//                _centerAction?.Invoke();
//                //MessageDispatcher.Instance.DispatchMsg("CenterCamera", 0f, _mainCharacter.GetId(), _camera.GetComponentRepository().GetId(), null);
//                //_camera.CenterCamera(true);
//            }
//            else
//            {
//                _camera.SetLockedOnTarget(lockGameObject);
//                //closest?.Destroy();
//                _lockOn?.Invoke();
//            }
//        }

//        /// <summary>
//        /// Finds all the close lock targets, sorted by distance
//        /// </summary>
//        /// <returns></returns>
//        private IList<GameObject> CreateTargetList()
//        {
//            int layerMask = 0;
//            layerMask = _targetLockLayers.value;
//            //for (int i = 0; i < _targetLockLayers.)
//            //if (!UnityEngine.Physics.SphereCast(transform.position, _targetLockRadius, transform.forward, out var hit,
//            //    _targetLockDistance, layerMask))

//            //for (int i = 0; i < hitResults.Length; i++)
//            //{
//            //    hitResults[i] = null;
//            //}

//            //var hitResults = UnityEngine.Physics.OverlapSphere(transform.position, _targetLockRadius);
//            //var hitResults = UnityEngine.Physics.OverlapBox(transform.position,
//            //    new Vector3(_targetLockRadius, .1f, _targetLockRadius));

//            //DrawBoxCast.DrawBoxCastBox(transform.position,
//            //    new Vector3(_targetLockRadius, .1f, _targetLockRadius), Quaternion.identity, transform.forward, _targetLockRadius, Color.red);

//            //var hitResults = UnityEngine.Physics.BoxCastAll(transform.position,
//            //    new Vector3(_targetLockRadius, .1f, _targetLockRadius), transform.forward, Quaternion.identity, _targetLockRadius);

//            var position = _playerController.transform.position + (_playerController.transform.forward * 
//                _playerController.TargetLockRadius) + new Vector3(0, 1, 0);

//            //DrawBoxCast.DrawBox(new Box(position,
//            //    new Vector3(_targetLockRadius, 1f, _targetLockRadius)), Color.red, 5f);

//            DrawBoxCast.DrawBox(position,
//                new Vector3(_playerController.TargetLockRadius, 1f, _playerController.TargetLockRadius), _playerController.transform.rotation, Color.red, 5f);

//            var hitResults = UnityEngine.Physics.OverlapBox(position,
//                new Vector3(_playerController.TargetLockRadius, 1f, _playerController.TargetLockRadius), _playerController.transform.rotation, layerMask);

//            if (hitResults.Length == 0)
//            {
//                Debug.Log("Could not locate nearest target");
//                return null;
//            }
//            else
//            {
//                Debug.Log($"Found {hitResults.Length} hit targets");

//                //PrintCastHitNames(hitResults);

//                //return null;
//            }

//            //if (UnityEngine.Physics.OverlapSphereNonAlloc(transform.position, _targetLockRadius, hitResults, layerMask) == 0)
//            //{
//            //    print("Could not locate nearest target");
//            //    return null;
//            //}

//            return FilterCastResults(hitResults);
//        }

//        private static void PrintCastHitNames(Collider[] hitResults)
//        {
//            for (int i = 0; i < hitResults.Length; i++)
//            {
//                if (hitResults[i].attachedRigidbody != null)
//                    Debug.Log($"(AutoLock) Hit target {hitResults[i].attachedRigidbody.name}");
//            }
//        }

//        public IEnumerable<GameObject> FilterCastResults(RaycastHit[] hitResults)
//        {
//            if (hitResults.Length == 0)
//                return null;
//            // TODO Remove the LINQ statement
//            var sortedResults = hitResults.Where(i => i.rigidbody != null).OrderBy(i => i.distance);

//            //Array.Sort()
//            //Array.Sort(hitResults, PhysicsHelper.RaycastDistanceCompareDel);

//            //autoLockableObjects.Clear();
//            tempGameObjectDict.Clear();

//            //for (int i = 0; i < sortedResults.Count; i++)
//            foreach (var hit in sortedResults)
//            {
//                //var hit = hitResults[i];
//                var gameobject = hit.rigidbody.gameObject;
//                if (gameobject == null)
//                    continue;
//                // Don't process the same game object twice
//                if (tempGameObjectDict.ContainsKey(gameobject.GetInstanceID()))
//                    continue;
//                tempGameObjectDict.Add(gameobject.GetInstanceID(), gameobject);
//                Debug.Log($"Cast hit target {gameobject.name}");
//            }
//            //var closest = hitResults[0].attachedRigidbody.GetComponent<SpriteCommonComponent>();
//            //var closest = hit.rigidbody.GetComponent<SpriteCommonComponent>();
//            //return closest;
//            //return hitResults[0].attachedRigidbody.gameObject;
//            return tempGameObjectDict.Values;
//        }

//        public IList<GameObject> FilterCastResults(Collider[] hitResults)
//        {
//            if (hitResults.Length == 0)
//                return null;
//            // TODO Remove the LINQ statement
//            var sortedResults = hitResults.Where(i => i.attachedRigidbody != null).OrderBy(i => i.ClosestPoint(_playerController.transform.position).sqrMagnitude);

//            //Array.Sort()
//            //Array.Sort(hitResults, PhysicsHelper.RaycastDistanceCompareDel);

//            //autoLockableObjects.Clear();
//            tempGameObjectDict.Clear();

//            //for (int i = 0; i < sortedResults.Count(); i++)
//            foreach (var hit in sortedResults)
//            {
//                bool hasTag = Array.IndexOf(_tags, hit.tag) > -1;
//                if (!hasTag)
//                    continue;
//                //var hit = hitResults[i];
//                var gameobject = hit.attachedRigidbody.gameObject;
//                if (gameobject == null)
//                    continue;
//                // Don't process the same game object twice
//                if (tempGameObjectDict.ContainsKey(gameobject.GetInstanceID()))
//                    continue;
//                tempGameObjectDict.Add(gameobject.GetInstanceID(), gameobject);
//                Debug.Log($"Cast hit target {gameobject.name}");
//            }
//            //var closest = hitResults[0].attachedRigidbody.GetComponent<SpriteCommonComponent>();
//            //var closest = hit.rigidbody.GetComponent<SpriteCommonComponent>();
//            //return closest;
//            //return hitResults[0].attachedRigidbody.gameObject;
//            return tempGameObjectDict.Values.ToList();
//        }

//        //public IEnumerable<GameObject> FilterTargetResults()
//        //{
//        //    // TODO Remove the LINQ statement
//        //    var sortedResults = hitResults.Where(i => i != null && i.attachedRigidbody != null).OrderBy(i => i.ClosestPoint(transform.position).sqrMagnitude);

//        //    //Array.Sort()
//        //    //Array.Sort(hitResults, PhysicsHelper.RaycastDistanceCompareDel);

//        //    //autoLockableObjects.Clear();
//        //    tempGameObjectDict.Clear();

//        //    //for (int i = 0; i < sortedResults.Count; i++)
//        //    foreach (var hit in sortedResults)
//        //    {
//        //        //var hit = hitResults[i];
//        //        var gameobject = hit.attachedRigidbody?.gameObject;
//        //        if (gameobject == null)
//        //            continue;
//        //        // Don't process the same game object twice
//        //        if (tempGameObjectDict.ContainsKey(gameobject.GetInstanceID()))
//        //            continue;
//        //        tempGameObjectDict.Add(gameobject.GetInstanceID(), gameobject);
//        //    }
//        //    //var closest = hitResults[0].attachedRigidbody.GetComponent<SpriteCommonComponent>();
//        //    //var closest = hit.rigidbody.GetComponent<SpriteCommonComponent>();
//        //    //return closest;
//        //    //return hitResults[0].attachedRigidbody.gameObject;
//        //    return tempGameObjectDict.Values;
//        //}

//        public void SetTargetLockLayers(LayerMask layers)
//        {
//            _targetLockLayers = layers;
//        }

//        public void SetTags(string[] tags)
//        {
//            _tags = tags;
//        }

//        public void SetCenterAction(Action centerAction)
//        {
//            _centerAction = centerAction;
//        }

//        public void SetLockOnAction(Action lockOnAction)
//        {
//            _lockOn = lockOnAction;
//        }
//    }
//}
