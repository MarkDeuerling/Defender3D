using UnityEngine;

namespace GameCamera
{
    [ExecuteInEditMode]
    public class PostProcessingCamera : MonoBehaviour 
    {
        public Material PostProcessingMaterial;
        public Material RadialBlur;
        
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var temp = RenderTexture.GetTemporary(src.width, src.height);
            Blur(src, temp);
            Glitch(temp, dest);
            RenderTexture.ReleaseTemporary(temp);
        }

        private void Blur(RenderTexture src, RenderTexture dest)
        {
             Graphics.Blit(src, dest, RadialBlur);   
        }

        private void Glitch(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, PostProcessingMaterial);
        }
    }
}
