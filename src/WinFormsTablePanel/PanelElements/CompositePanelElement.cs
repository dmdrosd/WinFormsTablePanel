namespace WinFormsTablePanel.PanelElements
{
    public class CompositePanelElement : PanelElement
    {
        private readonly List<PanelElement> _children = [];

        public CompositePanelElement(IEnumerable<PanelElement> elements)
        {
            _children.AddRange(elements);
        }

        public void Add(PanelElement element)
        {
            _children.Add(element);
        }

        public void Remove(PanelElement element)
        {
            _children.Remove(element);
        }

        public override IEnumerable<Control> GetAllControls()
        {
            return _children.SelectMany(child => child.GetAllControls());
        }
    }
}