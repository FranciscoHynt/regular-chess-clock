using UnityEngine.Events;

namespace Events
{
    public static class InterfaceEvents
    {
        public static readonly EnablePanelEvent EnablePanelEvent = new EnablePanelEvent();
    }
    
    public class EnablePanelEvent : UnityEvent<string>{}
}