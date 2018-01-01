using System;
using UnityEngine;

namespace GameCamera
{
    [ExecuteInEditMode]
    public class PostProcessingCamera : MonoBehaviour
    {
        [Range(0, 5)]
        public int KernalSize;
        public bool UseGlitch;
        public GlitchConfiguration GlitchConfig;
        public bool UseBlur;
        public BlurConfiguration BlurConfig;

        private bool usedRenderTexture;
        
        [Serializable]
        public class GlitchConfiguration
        {
            public float Distortion;
            public float Cutoff;
            public bool Normalize;
            public bool UseBlend;
            public bool UseColor;
            public Color Color;
            public Texture2D BlendTex;
            public Texture2D TransitionTex;
            public Material Material;
        }

        [Serializable]
        public class BlurConfiguration
        {
            public float Blur;
            public float Bright;
            public Vector2 Origin;
            public Material Material;
        }

        private void ConfigBlur()
        {
            BlurConfig.Material.SetFloat("_Blur", BlurConfig.Blur);
            BlurConfig.Material.SetFloat("_Bright", BlurConfig.Bright);
            BlurConfig.Material.SetVector("_Origin", BlurConfig.Origin);
        }
        
        private void ConfigGlitch()
        {
            GlitchConfig.Material.SetFloat("_Cutoff", GlitchConfig.Cutoff);
            GlitchConfig.Material.SetFloat("_Distortion", GlitchConfig.Distortion);
            GlitchConfig.Material.SetColor("_Color", GlitchConfig.Color);
            GlitchConfig.Material.SetTexture("_TransitionTex", GlitchConfig.TransitionTex);
            GlitchConfig.Material.SetTexture("_BlendTex", GlitchConfig.BlendTex);
            var normalize = GlitchConfig.Normalize ? 1 : 0;
            GlitchConfig.Material.SetFloat("_Normalize", normalize);
            var blend = GlitchConfig.UseBlend ? 1 : 0;
            GlitchConfig.Material.SetFloat("_UseBlend", blend);
            var color = GlitchConfig.UseColor ? 1 : 0;
            GlitchConfig.Material.SetFloat("_UseColor", color);
        }
        
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var temp = src;
            usedRenderTexture = false;
            if (UseBlur)
            {
                temp = RenderTexture.GetTemporary(
                    src.width >> KernalSize, src.height >> KernalSize);
                usedRenderTexture = true;
                Blur(src, temp); 
            }
            if (UseGlitch)
            {
                Glitch(temp, dest);
            }
            if (!UseGlitch)
                Graphics.Blit(temp, dest);
            if (usedRenderTexture)
                RenderTexture.ReleaseTemporary(temp);
        }

        private void Blur(Texture src, RenderTexture dest)
        {
            ConfigBlur();
            Graphics.Blit(src, dest, BlurConfig.Material);   
        }

        private void Glitch(Texture src, RenderTexture dest)
        {
            ConfigGlitch();
            Graphics.Blit(src, dest, GlitchConfig.Material);
        }
    }
}
