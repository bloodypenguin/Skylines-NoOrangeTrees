using NoOrangeTrees.Detours;
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
                if (!NaturalResourceManager.exists)
                {
                    return;
                }
                if (NaturalResourceManager.instance.m_resourceTexture != null)
                {
                    Shader.SetGlobalTexture("_NaturalResources", (Texture)NaturalResourceManager.instance.m_resourceTexture);
                }
                if (NaturalResourceManager.instance.m_destructionTexture != null)
                {
                    Shader.SetGlobalTexture("_NaturalDestruction", (Texture)NaturalResourceManager.instance.m_destructionTexture);
                }
            }
            else
            {
                if (NaturalResourcesManagerDetour.infoViewTexture != null)
                {
                    Shader.SetGlobalTexture("_NaturalResources", (Texture)NaturalResourcesManagerDetour.infoViewTexture);
                }
                if (NaturalResourcesManagerDetour.infoViewTextureB != null)
                {
                    Shader.SetGlobalTexture("_NaturalDestruction", (Texture)NaturalResourcesManagerDetour.infoViewTextureB);
                }
            }
            cachedInfoMode = InfoManager.instance.CurrentMode;
        }
    }
}