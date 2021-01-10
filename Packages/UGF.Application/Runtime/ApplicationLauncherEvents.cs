using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Launcher Events", 2000)]
    public class ApplicationLauncherEvents : MonoBehaviour
    {
        [SerializeField] private ApplicationLauncherComponent m_launcher;
        [SerializeField] private ApplicationHandlerEvent m_launchedEvent = new ApplicationHandlerEvent();
        [SerializeField] private ApplicationHandlerEvent m_stoppedEvent = new ApplicationHandlerEvent();

        public ApplicationLauncherComponent Launcher { get { return m_launcher; } set { m_launcher = value; } }
        public ApplicationHandlerEvent LaunchedEvent { get { return m_launchedEvent; } }
        public ApplicationHandlerEvent StoppedEvent { get { return m_stoppedEvent; } }

        private void Start()
        {
            if (m_launcher != null)
            {
                m_launcher.Launched += OnLaunched;
                m_launcher.Stopped += OnStopped;
            }
        }

        private void OnDestroy()
        {
            if (m_launcher != null)
            {
                m_launcher.Launched -= OnLaunched;
                m_launcher.Stopped -= OnStopped;
            }
        }

        private void OnLaunched(IApplication application)
        {
            m_launchedEvent.Invoke(application);
        }

        private void OnStopped(IApplication application)
        {
            m_stoppedEvent.Invoke(application);
        }
    }
}
