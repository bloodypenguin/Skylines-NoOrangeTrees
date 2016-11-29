using System.Collections.Generic;
using System.Reflection;
using ICities;
using NoOrangeTrees.Detours;
using NoOrangeTrees.Redirection;
using UnityEngine;

namespace NoOrangeTrees
{
    public class LoadingExtension : LoadingExtensionBase
    {

        private static Dictionary<MethodInfo, RedirectCallsState> _redirects;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            _redirects = RedirectionUtil.RedirectAssembly();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            new GameObject("NoOrangeTrees").AddComponent<InfoViewMonitor>();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            NaturalResourcesManagerDetour.m_modifiedX1 = null;
            NaturalResourcesManagerDetour.m_modifiedX2 = null;
            NaturalResourcesManagerDetour.m_modifiedBX1 = null;
            NaturalResourcesManagerDetour.m_modifiedBX2 = null;
            if (NaturalResourcesManagerDetour.infoViewTexture != null)
            {
                Object.Destroy(NaturalResourcesManagerDetour.infoViewTexture);
            }
            NaturalResourcesManagerDetour.infoViewTexture = null;
            if (NaturalResourcesManagerDetour.infoViewTextureB != null)
            {
                Object.Destroy(NaturalResourcesManagerDetour.infoViewTextureB);
            }
            NaturalResourcesManagerDetour.infoViewTextureB = null;
            var go = GameObject.Find("NoOrangeTrees");
            if (go != null)
            {
                Object.Destroy(go);
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
            RedirectionUtil.RevertRedirects(_redirects);
        }
    }
}