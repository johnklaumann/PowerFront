using OperatorReport.Models;
using PowerFront.DTO;

namespace PowerFront.Mapping
{
    public static class OperatorReportMapper
    {
        public static OperatorReportViewModel MapToViewModel(OperatorReportViewModelDTO dto)
        {
            return new OperatorReportViewModel
            {
                ID = dto.ID,
                Name = dto.Name,
                ProactiveSent = dto.ProactiveSent,
                ProactiveAnswered = dto.ProactiveAnswered,
                ProactiveResponseRate = dto.ProactiveResponseRate,
                ReactiveReceived = dto.ReactiveReceived,
                ReactiveAnswered = dto.ReactiveAnswered,
                ReactiveResponseRate = dto.ReactiveResponseRate,
                TotalChatLength = dto.TotalChatLength,
                AverageChatLength = dto.AverageChatLength
            };
        }

        public static OperatorReportItems MapToViewModel(OperatorReportItemsDTO dto)
        {
            var items = new OperatorReportItems
            {
                OperatorProductivity = new List<OperatorReportViewModel>()
            };

            foreach (var dtoViewModel in dto.OperatorProductivity)
            {
                items.OperatorProductivity.Add(MapToViewModel(dtoViewModel));
            }

            return items;
        }
    }
}
