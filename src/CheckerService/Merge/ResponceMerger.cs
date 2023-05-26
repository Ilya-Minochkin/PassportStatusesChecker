using CheckerService.Abstractions;
using CheckerService.Models;

namespace CheckerService.Merge
{
    internal static class ResponceMerger
    {
        public static MergeResult Merge(ReadinessResponse left, ReadinessResponse right)
        {
            if (left.Uid == null)
                return new MergeResult()
                {
                    Name = "Новый статус",
                    Body = right.ToMessage()
                };

            var isPublicStatusEquals = left.PublicStatus.Equals(right.PublicStatus);
            var isInternalStatusEquals = left.InternalStatus.Equals(right.InternalStatus);
            var publicMergeMessage = GetMergeMessage(left.PublicStatus, right.PublicStatus);
            var internalMergeMessage = GetMergeMessage(left.InternalStatus, right.InternalStatus);

            if (isPublicStatusEquals && isInternalStatusEquals)
                return new MergeResult()
                {
                    ResultEquals = true
                };

            if (!isPublicStatusEquals && !isInternalStatusEquals)
                return new MergeResult()
                {
                    Name = "*Обновился публичный и внутренний статусы*",
                    Body = "*Публичный статус*:\n" 
                        + publicMergeMessage + "\n"
                        + "*Внутренний статус*:\n"
                        + internalMergeMessage
                };

            if (!isPublicStatusEquals)
                return new MergeResult()
                {
                    Name = "*Обновился публичный статус*",
                    Body = publicMergeMessage
                };

            if (!isInternalStatusEquals)
                return new MergeResult()
                {
                    Name = "*Обновился внутренний статус*",
                    Body = internalMergeMessage
                };

            throw new Exception("Responces merge failed");
        }

        private static string GetMergeMessage(IUserMessage left, IUserMessage right)
        {
            return "Было:\n"
                    + $"{left.ToMessage()}\n"
                    + "Стало:\n"
                    + $"{right.ToMessage()}\n";
        }
    }
}
