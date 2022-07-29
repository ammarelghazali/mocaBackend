namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Response
{
    public class FaqBaseDto
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int DisplayOrder { get; set; }
    }
}
