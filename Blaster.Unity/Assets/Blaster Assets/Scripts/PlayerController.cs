//using Rewired;
//using BlueOrb.Base.Components;
//using BlueOrb.Physics;
//using UnityEngine;

//namespace BlueOrb.Player
//{
//    [AddComponentMenu("RQ/Components/Player Controller")]
//    public class PlayerController : MonoBehaviour
//    {
//        [SerializeField]
//        private string _runAnim;
//        [SerializeField]
//        private LayerMask _targetLockLayers;
//        [SerializeField]
//        private float _targetLockRadius = 5;
//        [SerializeField]
//        private float _targetLockDistance = 40;
//        [SerializeField]
//        private string _targetLockAction;

//        private PhysicsComponent _physicsComponent;
//        private Animator _animator;
//        //private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
//        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
//        private Vector3 m_CamForward;             // The current forward direction of the camera
//        private Vector3 _move;
//        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
//        private Rewired.Player _player;
//        private bool _enablePlayerInput = true;
//        private bool _targetLockMode = false;

//        //private Vector3 _velocity;

//        private void Awake()
//        {
//            // get the transform of the main camera
//            if (Camera.main != null)
//            {
//                m_Cam = Camera.main.transform;
//            }
//            else
//            {
//                Debug.LogWarning(
//                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
//                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
//            }

//            // get the third person character ( this should never be null due to require component )
//            _physicsComponent = GetComponent<PhysicsComponent>();
//            _animator = GetComponent<Animator>();
//            _animator.applyRootMotion = true;
//        }

//        private void Start()
//        {
//            _player = ReInput.players.Players[0];
//        }

//        private void Update()
//        {
//            if (!_enablePlayerInput)
//                return;
//            // read inputs
//            float h = _player.GetAxis("Horizontal");
//            float v = _player.GetAxis("Vertical");

//            if (h != 0 || v != 0)
//            {
//                int i = 1;
//            }
//            //bool crouch = Input.GetKey(KeyCode.C);

//            // calculate move direction to pass to character
//            if (m_Cam != null)
//            {
//                // calculate camera relative direction to move:
//                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
//                _move = v * m_CamForward + h * m_Cam.right;
//                _move *= _physicsComponent.GetPhysicsData().MaxSpeed;
//            }
//            else
//            {
//                // we use world-relative directions in the case of no main camera
//                _move = v * Vector3.forward + h * Vector3.right;
//                _move *= _physicsComponent.GetPhysicsData().MaxSpeed;
//            }

//            bool isRunning = _move != Vector3.zero;

//            _animator.SetBool(_runAnim, isRunning);

//            var camRotation = m_Cam.rotation.eulerAngles;
//            if (isRunning)
//                transform.LookAt(transform.position + _move);
//            //Quaternion.

//            //transform.rotation = Quaternion.Euler(transform.rotation.x, camRotation.y, transform.rotation.z);


//            //_velocity = m_Move;
////#if !MOBILE_INPUT
////            // walk speed multiplier
////            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
////#endif

//            //if (!m_Jump)
//            //{
//            //    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
//            //}

//            if (_player.GetButtonDown(_targetLockAction))
//            {
//                TargetLockPressed();
//            }
//        }

//        // Fixed update is called in sync with physics
//        private void FixedUpdate()
//        {
//            // pass all parameters to the character control script
//            //m_Character.Move(m_Move, crouch, m_Jump);
//            //m_Jump = false;
//            if (_enablePlayerInput)
//            {
//                _physicsComponent.SetVelocity3(_move);
//            }
//        }

//        public void TargetLockPressed()
//        {
//            var closest = FindNearestTarget();
//            closest.Destroy();
//        }

//        private SpriteCommonComponent FindNearestTarget()
//        {
//            int layerMask = 0;
//            layerMask = _targetLockLayers.value;
//            //for (int i = 0; i < _targetLockLayers.)
//            if (!Physics.SphereCast(transform.position, _targetLockRadius, transform.forward, out var hit, _targetLockDistance, layerMask))
//            {
//                print("Could not locate nearest target");
//                return null;
//            }
//            var closest = hit.rigidbody.GetComponent<SpriteCommonComponent>();
//            return closest;
//        }

//        public void SetEnablePlayerInput(bool enable)
//        {
//            _enablePlayerInput = enable;
//        }
//    }
//}
