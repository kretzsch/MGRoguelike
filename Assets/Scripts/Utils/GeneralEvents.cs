using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    /// <summary>
    /// Provides a few events for easy use on objects to call some functions
    /// </summary>
    public class GeneralEvents : MonoBehaviour
    {
        public UnityEvent OnAwake, OnOnEnable, OnStart, OnTrigger, OnTrigger2D, OnCollision, OnCollision2D, OnOnDisable, OnOnDestroy;

        /// <summary> Awake invokes OnAwake </summary>
        private void Awake() { OnAwake.Invoke(); }
        /// <summary> OnEnable invokes OnOnEnable </summary>
        private void OnEnable() { OnOnEnable.Invoke(); }
        /// <summary> Start invokes OnStart </summary>
        private void Start() { OnStart.Invoke(); }
        /// <summary> OnTriggerEnter invokes OnTriger </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other) { OnTrigger.Invoke(); }
        /// <summary> OnTriggerEnter2D invokes OnTrigger2D </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other) { OnTrigger2D.Invoke(); }
        /// <summary> OnTriggerExit2D invokes OnTrigger2D </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other) { OnTrigger2D.Invoke(); }
        /// <summary> OnCollisionEnter2D invokes OnCollision2D </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision) { OnCollision2D.Invoke(); }
        /// <summary> OnCollisionEnter invokes OnCollision </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision) { OnCollision.Invoke(); }
        /// <summary> OnDisable invokes OnOnDisable </summary>
        private void OnDisable() { OnOnDisable.Invoke(); }
        /// <summary> OnDestroy invokes OnOnDestroy </summary>
        private void OnDestroy() { OnOnDestroy.Invoke(); }
    }
}