using UnityEngine;
using UnityEngine.Events;

namespace UGF.Application.Runtime
{
    public sealed class ApplicationLauncherEvents : MonoBehaviour
    {
        [SerializeField] private ApplicationLauncher m_launcher;
        [SerializeField] private ApplicationHandlerEvent m_launchedEvent = new ApplicationHandlerEvent();
        [SerializeField] private UnityEvent m_stoppedEvent = new UnityEvent();
        [SerializeField] private UnityEvent m_quittingEvent = new UnityEvent();

        /// <summary>
        /// Gets or sets application launcher to subscribe.
        /// </summary>
        public ApplicationLauncher Launcher { get { return m_launcher; } set { m_launcher = value; } }

        /// <summary>
        /// Triggered after all launch completed and application becomes available.
        /// </summary>
        public ApplicationHandlerEvent LaunchedEvent { get { return m_launchedEvent; } }

        /// <summary>
        /// Triggered after launcher is completely stopped and application no longer available.
        /// </summary>
        public UnityEvent StoppedEvent { get { return m_stoppedEvent; } }

        /// <summary>
        /// Triggered when Unity application performs quitting.
        /// </summary>
        public UnityEvent QuittingEvent { get { return m_quittingEvent; } }

        private void Awake()
        {
            if (m_launcher != null)
            {
                m_launcher.Launched += OnLaunched;
                m_launcher.Stopped += OnStopped;
                m_launcher.Quitting += OnQuitting;
            }
        }

        private void OnDestroy()
        {
            if (m_launcher != null)
            {
                m_launcher.Launched -= OnLaunched;
                m_launcher.Stopped -= OnStopped;
                m_launcher.Quitting -= OnQuitting;
            }
        }

        private void OnLaunched(IApplication application)
        {
            m_launchedEvent.Invoke(application);
        }

        private void OnStopped()
        {
            m_stoppedEvent.Invoke();
        }

        private void OnQuitting()
        {
            m_quittingEvent.Invoke();
        }
    }
}
