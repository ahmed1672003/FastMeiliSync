namespace FastMeiliSync.Shared.ValidationMessages;

public class ValidationMessages
{
    public const string Success = "operation done successfully";

    public static class MeiliSearch
    {
        public const string InstanceNotFound = "Meili search instance not found.";
        public const string InstanceLabelExist = "Meili search label exist.";
        public const string InstanceUrlExist = "Meili search url exist.";
    }

    public static class Source
    {
        public const string LabelRequired = "Source label required.";
        public const string DatabaseRequired = "Database required.";
        public const string UrlRequired = "Url required.";
        public const string SourceNotFound = "Source not found.";
        public const string LabelExist = "Source label exist.";
        public const string UrlExist = "Source url exist.";
        public const string DatabaseExist = "Database exist.";
    }
}
