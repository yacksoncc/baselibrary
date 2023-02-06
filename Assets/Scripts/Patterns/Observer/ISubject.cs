using System;

namespace Patterns.Observer
{
   public interface ISubject
   {
      void SuscribeObserver(IObserver argIObserver);

      void UnsuscribeObserver(IObserver argIObserver);

      void NotifyToObservers(Enum argFlag);
   }
}