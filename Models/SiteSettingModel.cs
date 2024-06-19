namespace WOPHRMSystem.Models
{
    public class SiteSettingModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}