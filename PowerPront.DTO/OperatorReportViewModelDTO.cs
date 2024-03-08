namespace PowerFront.DTO
{
    public class OperatorReportViewModelDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProactiveSent { get; set; }
        public int ProactiveAnswered { get; set; }
        public int ProactiveResponseRate { get; set; }
        public int ReactiveReceived { get; set; }
        public int ReactiveAnswered { get; set; }
        public int ReactiveResponseRate { get; set; }
        public string TotalChatLength { get; set; }
        public string AverageChatLength { get; set; }
    }

    public class OperatorReportItemsDTO
    {
        public List<OperatorReportViewModelDTO> OperatorProductivity { get; set; }
    }
}
