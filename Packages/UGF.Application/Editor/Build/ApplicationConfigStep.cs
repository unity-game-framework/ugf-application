using System;
using UGF.Application.Runtime;
using UGF.Build.Editor;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Application.Editor.Build
{
    public class ApplicationConfigStep : BuildStep
    {
        public ApplicationConfigAsset Config { get; }

        public ApplicationConfigStep(ApplicationConfigAsset config)
        {
            Config = config ? config : throw new ArgumentNullException(nameof(config));
        }

        protected override void OnExecute(IBuildSetup setup, IContext context)
        {
            ApplicationSettings.Config = Config;

            Log.Info("Application config for project set", new { Config });
        }
    }
}
