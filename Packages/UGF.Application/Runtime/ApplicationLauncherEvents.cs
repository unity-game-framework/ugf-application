﻿using UnityEngine;

namespace UGF.Application.Runtime
{
    public sealed class ApplicationLauncherEvents : MonoBehaviour
    {
        [SerializeField] private ApplicationLauncher m_launcher;
        [SerializeField] private ApplicationHandlerEvent m_launchedEvent = new ApplicationHandlerEvent();
        [SerializeField] private ApplicationHandlerEvent m_stoppedEvent = new ApplicationHandlerEvent();
        [SerializeField] private ApplicationHandlerEvent m_quittingEvent = new ApplicationHandlerEvent();

        public ApplicationLauncher Launcher { get { return m_launcher; } set { m_launcher = value; } }
        public ApplicationHandlerEvent LaunchedEvent { get { return m_launchedEvent; } }
        public ApplicationHandlerEvent StoppedEvent { get { return m_stoppedEvent; } }
        public ApplicationHandlerEvent QuittingEvent { get { return m_quittingEvent; } }

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

        private void OnStopped(IApplication application)
        {
            m_stoppedEvent.Invoke(application);
        }

        private void OnQuitting(IApplication application)
        {
            m_quittingEvent.Invoke(application);
        }
    }
}
