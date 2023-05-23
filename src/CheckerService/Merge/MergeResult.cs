namespace CheckerService.Merge
{
    public class MergeResult
    {
        public bool ResultEquals { get; set; }
        public string? Name { get; set; }
        public string? Body { get; set; }

        public override string? ToString()
        {
            return ResultEquals
                ? "Статус не изменился"
                : Name + "\n" + Body;
        }
    }
}
