namespace MOCA.Core.DTOs.MocaSettings.TopUpDtos.Response
{
    public class TopUpDto
    {
        public long Id { get; set; }

        public string TermsOfUse { get; set; }

        public long TopUpTypeId { get; set; }

        public string TopUpType { get; set; }
    }
}
