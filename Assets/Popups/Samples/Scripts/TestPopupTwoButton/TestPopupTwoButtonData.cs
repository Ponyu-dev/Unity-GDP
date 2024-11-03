using Popups;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopupTwoButtonData : PopupData
    {
        public string Description { get; private set; }
        public string Apply { get; private set; }
        public string Cancel { get; private set; }
        
        public TestPopupTwoButtonData(string title, string description, string apply, string cancel)
            : base(title)
        {
            Description = description;
            Apply = apply;
            Cancel = cancel;
        }
    }
}