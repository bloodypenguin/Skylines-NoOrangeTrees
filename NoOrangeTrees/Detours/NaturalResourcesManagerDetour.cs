using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using NoOrangeTrees.OptionsFramework;
using NoOrangeTrees.Redirection;
using UnityEngine;

namespace NoOrangeTrees.Detours
{
    [TargetType(typeof(NaturalResourceManager))]
    public class NaturalResourcesManagerDetour : NaturalResourceManager
    {
        public static int[] m_modifiedX1;
        public static int[] m_modifiedX2;
        public static int[] m_modifiedBX1;
        public static int[] m_modifiedBX2;

        public static Texture2D infoViewTexture;
        public static Texture2D infoViewTextureB;

        public static void RefreshTexture(bool stub)
        {
            if (!SimulationManager.exists || !exists)
            {
                return;
            }
            SimulationManager.instance.AddAction(RefreshTextureAction());
        }

        private static IEnumerator RefreshTextureAction()
        {
            NaturalResourceManager.instance.AreaModified(0, 0, 511, 511);
            yield return null;
        }

        [RedirectMethod]
        private void UpdateTexture()
        {
            //begin mod
            if (infoViewTexture == null)
            {
                infoViewTexture = new Texture2D(m_resourceTexture.width, m_resourceTexture.height, TextureFormat.RGB24,
                    false, true);
                infoViewTexture.wrapMode = TextureWrapMode.Clamp;
            }
            if (m_modifiedX1 == null)
            {
                m_modifiedX1 = (int[]) typeof(NaturalResourceManager).GetField("m_modifiedX1",
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            }
            if (m_modifiedX2 == null)
            {
                m_modifiedX2 = (int[]) typeof(NaturalResourceManager).GetField("m_modifiedX2",
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            }
            //end mod
            for (int index = 0; index < 512; ++index)
            {
                if (m_modifiedX2[index] >= m_modifiedX1[index])
                {
                    do
                        ; while (
                        !Monitor.TryEnter((object) this.m_naturalResources, SimulationManager.SYNCHRONIZE_TIMEOUT));
                    int num1;
                    int num2;
                    try
                    {
                        num1 = m_modifiedX1[index];
                        num2 = m_modifiedX2[index];
                        m_modifiedX1[index] = 10000;
                        m_modifiedX2[index] = -10000;
                    }
                    finally
                    {
                        Monitor.Exit((object) this.m_naturalResources);
                    }
                    for (int x = num1; x <= num2; ++x)
                    {
                        Color color;
                        if (index == 0 || x == 0 || (index == 511 || x == 511))
                        {
                            color = new Color(0.5f, 0.5f, 0.5f, 0.0f);
                            //begin mod
                            infoViewTexture.SetPixel(x, index, color);
                            //end mod
                        }
                        else
                        {
                            int ore = 0;
                            int oil = 0;
                            int sand = 0;
                            int fertility = 0;
                            int forest = 0;
                            int shore = 0;
                            this.AddResource(x - 1, index - 1, 5, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x, index - 1, 7, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x + 1, index - 1, 5, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x - 1, index, 7, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x, index, 14, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x + 1, index, 7, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x - 1, index + 1, 5, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x, index + 1, 7, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            this.AddResource(x + 1, index + 1, 5, ref ore, ref oil, ref sand, ref fertility, ref forest,
                                ref shore);
                            //begin mod
                            var options = OptionsWrapper<Options>.Options;
                            color = CalculateColorComponents(
                                options.ore ? 0 : ore,
                                options.oil ? 0 : oil,
                                sand,
                                options.fertility ? 0 : fertility,
                                options.shore ? 0 : shore,
                                options.forest ? 0 : forest);
                            infoViewTexture.SetPixel(x, index,
                                CalculateColorComponents(ore, oil, sand, fertility, shore, forest));
                            //end mod
                        }
                        this.m_resourceTexture.SetPixel(x, index, color);
                    }
                }
            }
            this.m_resourceTexture.Apply();
            //begin mod
            infoViewTexture.Apply();
            //end mod
        }


        private static Color CalculateColorComponents(int ore, int oil, int sand, int fertility, int shore, int forest)
        {
            Color color;
            color.r = (float) (ore - oil + 15810)*3.162555E-05f;
            color.g = (float) (sand - fertility + 15810)*3.162555E-05f;
            int num3 = shore*4 - forest;
            color.b = num3 <= 0
                ? (float) (15810 + num3)*3.162555E-05f
                : (float) (15810 + num3/4)*3.162555E-05f;
            color.a = 1f;
            return color;
        }

        [RedirectMethod]
        private void UpdateTextureB()
        {
            //begin mod
            if (infoViewTextureB == null)
            {
                infoViewTextureB = new Texture2D(m_destructionTexture.width, m_destructionTexture.height,
                    TextureFormat.RGB24, false, true);
                infoViewTextureB.wrapMode = TextureWrapMode.Clamp;
            }
            if (m_modifiedBX1 == null)
            {
                m_modifiedBX1 = (int[]) typeof(NaturalResourceManager).GetField("m_modifiedBX1",
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            }
            if (m_modifiedBX2 == null)
            {
                m_modifiedBX2 = (int[]) typeof(NaturalResourceManager).GetField("m_modifiedBX2",
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            }
            //end mod
            for (int index = 0; index < 512; ++index)
            {
                if (m_modifiedBX2[index] >= m_modifiedBX1[index])
                {
                    do
                        ; while (
                        !Monitor.TryEnter((object) this.m_naturalResources, SimulationManager.SYNCHRONIZE_TIMEOUT));
                    int num1;
                    int num2;
                    try
                    {
                        num1 = m_modifiedBX1[index];
                        num2 = m_modifiedBX2[index];
                        m_modifiedBX1[index] = 10000;
                        m_modifiedBX2[index] = -10000;
                    }
                    finally
                    {
                        Monitor.Exit((object) this.m_naturalResources);
                    }
                    for (int x = num1; x <= num2; ++x)
                    {
                        Color color;
                        if (index == 0 || x == 0 || (index == 511 || x == 511))
                        {
                            color = new Color(0.0f, 0.0f, 0.0f, 1f);
                        }
                        else
                        {
                            int pollution = 0;
                            int burned = 0;
                            int destroyed = 0;
                            this.AddResource(x - 1, index - 1, 5, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x, index - 1, 7, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x + 1, index - 1, 5, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x - 1, index, 7, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x, index, 14, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x + 1, index, 7, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x - 1, index + 1, 5, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x, index + 1, 7, ref pollution, ref burned, ref destroyed);
                            this.AddResource(x + 1, index + 1, 5, ref pollution, ref burned, ref destroyed);
                            //begin mod
                            var options = OptionsWrapper<Options>.Options;
                            color = CalculateColorComponentsB(
                                options.pollution ? 0 : pollution,
                                options.burned ? 0 : burned,
                                destroyed);
                            infoViewTextureB.SetPixel(x, index, CalculateColorComponentsB(pollution, burned, destroyed));
                            //end mod


                        }
                        this.m_destructionTexture.SetPixel(x, index, color);
                    }
                }
            }
            this.m_destructionTexture.Apply();
            //begin mod
            infoViewTextureB.Apply();
            //end mod
        }

        private static Color CalculateColorComponentsB(int pollution, int burned, int destroyed)
        {
            Color color;
            color.r = (float) pollution*6.325111E-05f;
            color.g = (float) burned*6.325111E-05f;
            color.b = (float) destroyed*6.325111E-05f;
            color.a = 1f;
            return color;
        }



        [MethodImpl(MethodImplOptions.NoInlining)]
        [RedirectReverse]
        private void AddResource(int x, int z, int multiplier, ref int ore, ref int oil, ref int sand, ref int fertility,
            ref int forest, ref int shore)
        {
            UnityEngine.Debug.Log("NoOrangeTrees - failed to detour AddResource()");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [RedirectReverse]
        private void AddResource(int x, int z, int multiplier, ref int pollution, ref int burned, ref int destroyed)
        {
            UnityEngine.Debug.Log("NoOrangeTrees - failed to detour AddResource()");
        }
    }
}