//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers
{
    class CameraMoverEventHandler : IScreenInputEventHandler
    {
        public int GetPriority()
        {
            return 30;
        }

        public bool TryHandle(ScreenInputEvent screenInputEvent)
        {
            if (screenInputEvent.isDragEvent == true)
            {
                // grab this and all future events with this finger id and hange it as drag
                return true;
            }

            return false;
        }
    }
}
