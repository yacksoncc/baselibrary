using System;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.Observer
{
   public abstract class Subject : MonoBehaviour, ISubject
   {
      [SerializeField]
      protected bool notifyLastFlagToNewObserver;

      private Enum lastFlag;

      private readonly List<IObserver> listObservers = new List<IObserver>();

      public void SuscribeObserver(IObserver argIObserver)
      {
         listObservers.Add(argIObserver);

         if(notifyLastFlagToNewObserver)
            argIObserver.UpdateFromSubject(lastFlag);
      }

      public void UnsuscribeObserver(IObserver argIObserver)
      {
         listObservers.Remove(argIObserver);
      }

      public void NotifyToObservers(Enum argFlag)
      {
         foreach(var tmpObserver in listObservers)
            tmpObserver.UpdateFromSubject(argFlag);

         lastFlag = argFlag;
      }
   }
}