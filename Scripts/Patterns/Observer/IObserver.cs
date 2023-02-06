using System;

namespace Patterns.Observer
{
   public interface IObserver
   {
      void UpdateFromSubject(Enum argFlag);
   }
}