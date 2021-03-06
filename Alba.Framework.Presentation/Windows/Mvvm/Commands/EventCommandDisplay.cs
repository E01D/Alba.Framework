using System.ComponentModel;
using System.Windows.Media;

namespace Alba.Framework.Windows.Mvvm
{
    [TypeConverter (typeof(TypeConverter))] // otherwise doesn't work as an argument for markup extension in data templates
    public class EventCommandDisplay
    {
        public EventCommand Command { get; set; }
        public string Label { get; set; }
        public string ShortLabel { get; set; }
        public ImageSource Icon { get; set; }
        public ImageSource LargeIcon { get; set; }
    }
}