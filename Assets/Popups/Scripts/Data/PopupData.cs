namespace Popups
{
    public abstract class PopupData
    {
        public string Title { get; private set; }
        
        protected PopupData(string title)
        {
            Title = title;
        }
    }
}