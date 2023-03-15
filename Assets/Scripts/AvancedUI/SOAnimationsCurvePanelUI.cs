#pragma warning disable 0649
using UnityEngine;

namespace AvancedUI
{
    [CreateAssetMenu(fileName = "SOAnimationsCurvePanelUI", menuName = "PanelUI/SOAnimationsCurvePanelUI")]
    public class SOAnimationsCurvePanelUI : ScriptableObject
    {
        /// <summary>
        /// Curva de animacion que se usara para definir la escala de aparicion
        /// </summary>
        [SerializeField] private AnimationCurve animationCurveEscalaAparecer;

        /// <summary>
        /// Curva de animacion que se usara para definir la escala de ocultacion
        /// </summary>
        [SerializeField] private AnimationCurve animationCurveEscalaOcultar;

        /// <summary>
        /// Curva de animacion que se usara para definir la transparencia de aparicion
        /// </summary>
        [SerializeField] private AnimationCurve animationCurveTransparenciaAparecer;

        /// <summary>
        /// Curva de animacion que se usara para definir la transparencia de ocultacion
        /// </summary>
        [SerializeField] private AnimationCurve animationCurveTransparenciaOcultar;

        /// <summary>
        /// Tiempo en segundos en el que el panel debe aparecer por completo
        /// </summary>
        [SerializeField, Tooltip("Tiempo en segundos en el que el panel debe aparecer por completo")]
        private float tiempoAparicion = 0.5f;
        
        /// <summary>
        /// Tiempo en segundos en el que el panel debe ocultarse por completo
        /// </summary>
        [SerializeField, Tooltip("Tiempo en segundos en el que el panel debe ocultarse por completo")]
        private float tiempoOcultacion = 0.5f;

        public AnimationCurve _animationCurveEscalaAparecer
        {
            get { return animationCurveEscalaAparecer; }
        }

        public AnimationCurve _animationCurveEscalaOcultar
        {
            get { return animationCurveEscalaOcultar; }
        }

        public AnimationCurve _animationCurveTransparenciaAparecer
        {
            get { return animationCurveTransparenciaAparecer; }
        }

        public AnimationCurve _animationCurveTransparenciaOcultar
        {
            get { return animationCurveTransparenciaOcultar; }
        }

        public float TiempoAparicion => tiempoAparicion;

        public float TiempoOcultacion => tiempoOcultacion;
    }
}