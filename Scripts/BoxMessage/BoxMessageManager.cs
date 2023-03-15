#pragma warning disable 0649
using Singleton;
using UnityEngine;

namespace BoxMessage
{
   public class BoxMessageManager : AbstractSingleton<BoxMessageManager>
   {
      /// <summary>
      ///    Prefabricado del la caja de mensaje que muestra una informacion
      /// </summary>
      [SerializeField]
      [Tooltip("Prefabricado del la caja de mensaje que muestra una informacion")]
      private GameObject prefabMessageInfo;

      /// <summary>
      ///    Prefabricado de la caja de mensaje que muestra un mensaje y botones para tomar una decision
      /// </summary>
      [SerializeField]
      [Tooltip("Prefabricado de la caja de mensaje que muestra un mensaje y botones para tomar una desicion")]
      private GameObject prefabMessageDecision;

      /// <summary>
      ///    Prefabricado de la caja de mensaje que muestra una pequeña informacion y desaparece
      /// </summary>
      [SerializeField]
      [Tooltip("Prefabricado de la caja de mensaje que muestra una pequeña informacion y desaparece ")]
      private GameObject prefabMessageMini;

      /// <summary>
      ///    Canvas en donde se mostraran las cajas de mensaje
      /// </summary>
      [SerializeField]
      [Tooltip("Canvas en donde se mostraran las cajas de mensaje")]
      private Transform canvas;

      private GameObject boxMessageDesicionActual;

      private GameObject boxMessageInfoActual;

      private GameObject boxMessageMiniActual;

      /// <summary>
      ///    Crea una nueva caja de mensaje para tomar una decision, con texto en sus botones
      /// </summary>
      /// <param name="argTextMessage">Mensaje que se mostrara</param>
      /// <param name="argTextButtonAccept">Texto que se colocara en el boton aceptar</param>
      /// <param name="argTextButtonCancel">Texto que se colocara en el boton cancelar</param>
      /// <param name="argFunctionExeAccept">Funcion que se ejecutara si el usuario acepta</param>
      /// <param name="argFunctionExeCancel">Funcion que se ejecutara si el usuario cancela</param>
      public AbstractBoxMessage CreateBoxMessageDecision(string argTextMessage, string argTextButtonCancel, string argTextButtonAccept, delegateOnBoxMessageButtonAccept argFunctionExeAccept = null, delegateOnBoxMessageButtonCancel argFunctionExeCancel = null)
      {
         if(!canvas)
            canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

         boxMessageDesicionActual = Instantiate(prefabMessageDecision, canvas);
         boxMessageDesicionActual.SetActive(false);
         var tmpBoxMessage = boxMessageDesicionActual.GetComponent<BoxMessageDecision>();
         tmpBoxMessage.OpenPanel();
         tmpBoxMessage.TextMessage = argTextMessage;
         tmpBoxMessage.TextButtonAccept = argTextButtonAccept;
         tmpBoxMessage.TextButtonCancel = argTextButtonCancel;
         tmpBoxMessage.AddDelegateButtonAccept(argFunctionExeAccept ?? delegate { });
         tmpBoxMessage.AddDelegateButtonCancel(argFunctionExeCancel ?? delegate { });
         return tmpBoxMessage;
      }

      /// <summary>
      ///    Crea una nueva caja de mensaje informativo que el usuario debe de cerrar precionando un boton de aceptar
      /// </summary>
      /// <param name="argTextMessage">Mensaje que se mostrara</param>
      /// <param name="argTextButtonAccept">Texto del mensaje del boton aceptar, o ok</param>
      /// <param name="argFunctionExeAccept">Funcion que se ejecutara si el usuario acepta</param>
      public AbstractBoxMessage CreateBoxMessageInfo(string argTextMessage, string argTextButtonAccept, delegateOnBoxMessageButtonAccept argFunctionExeAccept = null)
      {
         if(!canvas)
            canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

         boxMessageInfoActual = Instantiate(prefabMessageInfo, canvas);
         boxMessageInfoActual.SetActive(false);
         var tmpBoxMessage = boxMessageInfoActual.GetComponent<BoxMessageInfo>();
         tmpBoxMessage.OpenPanel();
         tmpBoxMessage.TextMessage = argTextMessage;
         tmpBoxMessage.TextButtonAccept = argTextButtonAccept;
         tmpBoxMessage.AddDelegateButtonAccept(argFunctionExeAccept ?? delegate { });
         return tmpBoxMessage;
      }

      /// <summary>
      ///    Crea un pequeño mensaje que se destruye en un tiempo determinado
      /// </summary>
      /// <param name="argTextMessage">Mensaje que se mostrara</param>
      /// <param name="argTimeDestroy">Tiempo en el que se destruira el mensaje</param>
      public void CreateBoxMessageMini(string argTextMessage, float argTimeDestroy = 1f, delegateOnBoxMessageButtonAccept argFunctionExeAccept = null)
      {
         if(!canvas)
            canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

         boxMessageMiniActual = Instantiate(prefabMessageMini, canvas);
         boxMessageMiniActual.SetActive(false);
         var tmpBoxMessage = boxMessageMiniActual.GetComponent<BoxMessageMini>();
         tmpBoxMessage.OpenPanel();
         tmpBoxMessage.TextMessage = argTextMessage;
         tmpBoxMessage.HiddeWithDelaySeconds = argTimeDestroy;
         tmpBoxMessage.AddDelegateButtonAccept(argFunctionExeAccept ?? delegate { });
      }
   }
}