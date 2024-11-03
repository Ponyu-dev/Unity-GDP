using Popups;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopupApplyData : PopupData
    {
        public string Description { get; private set; }
        public string Apply { get; private set; }
        
        public TestPopupApplyData(string title, string description, string apply)
            : base(title)
        {
            Description = description;
            Apply = apply;
        }
    }
}