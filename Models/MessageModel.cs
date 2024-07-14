namespace WOPHRMSystem.Models
{
    public class MessageModel
    {
        public string Status { get; set; } = "E";
        public string Text { get; set; } = string.Empty;

    }

    public class MessageModelWithData<TEntity>
    {
        public string Status { get; set; } = "E";
        public string Text { get; set; } = string.Empty;
        public TEntity DataList { get; set; }
    }

    public class MessageModelTwo
    {
        public string Status { get; set; } = "E";
        public string Text { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

    }
}