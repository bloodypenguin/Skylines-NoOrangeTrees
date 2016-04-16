using UnityEngine;

namespace NoOrangeTrees
{
    public class InfoViewMonitor : MonoBehaviour
    {

        private InfoManager.InfoMode cachedInfoMode = InfoManager.InfoMode.None;

        public void Update()
        {
            if (!InfoManager.exists || InfoManager.instance.CurrentMode == cachedInfoMode)
            {
                return;
            }
            if (InfoManager.instance.CurrentMode == InfoManager.InfoMode.None)
            {
                if (!NaturalResourceManager.exists || NaturalResourceManager.instance.m_resourceTexture == null)
                {
                    return;
                }
                UnityEngine.Debug.Log("Setting game view texture");
                Shader.SetGlobalTexture("_NaturalResources", (Texture)NaturalResourceManager.instance.m_resourceTexture);
            }
            else
            {
                if (NaturalResourcesManagerDetour.infoViewTexture == null)
                {
                    return;
                }
                UnityEngine.Debug.Log("Setting info view texture");
                Shader.SetGlobalTexture("_NaturalResources", (Texture)NaturalResourcesManagerDetour.infoViewTexture);
            }
            cachedInfoMode = InfoManager.instance.CurrentMode;
        }
    }
}