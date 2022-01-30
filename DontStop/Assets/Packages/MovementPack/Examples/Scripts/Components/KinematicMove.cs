using ECM.Examples.Common;
using UnityEngine;

namespace ECM.Examples.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicMove : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        public float _moveTime = 3.0f;

        [SerializeField]
        private Vector3 _offset;

        #endregion

        #region PRIVATE FIELDS

        private Rigidbody _rigidbody;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        #endregion

        #region PROPERTIES
        
        public float moveTime
        {
            get { return _moveTime; }
            set { _moveTime = Mathf.Max(1.0f, value); }
        }

        public Vector3 offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        #endregion

        #region MONOBEHAVIOUR

        public void OnValidate()
        {
            moveTime = _moveTime;
        }

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            transform.position -= new Vector3(0, 0, offset.z / 2);
            
            _startPosition = transform.position;
            _targetPosition = _startPosition + offset;
        }

        public void FixedUpdate()
        {
            var t = Utils.EaseInOut(Mathf.PingPong(Time.time, _moveTime), _moveTime);
            var p = Vector3.Lerp(_startPosition, _targetPosition, t);

            _rigidbody.transform.position = (p);
        }

        #endregion
    }
}
