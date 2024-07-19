using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;

namespace DefaultNamespace
{
    public class CustomBuildScript: BuildScriptPackedMode
    {
        protected override TResult DoBuild<TResult>(AddressablesDataBuilderInput builderInput, AddressableAssetsBuildContext aaContext)
        {
            return base.DoBuild<TResult>(builderInput, aaContext);
        }
    }
}