namespace FastMeiliSync.Shared.ValidationMessages;

public class ValidationMessages
{
    public const string Success = "operation done successfully";

    public static class MeiliSearch
    {
        public const string NotFound = "Meili search instance not found.";
        public const string LabelExist = "Meili search label exist.";
        public const string UrlExist = "Meili search url exist.";
        public const string LabelRequired = "Label required";
        public const string ApiKeyRequired = "Api key required";
        public const string UrlRequired = "Url required.";
    }

    public static class Sync
    {
        public const string LabelExist = "Sync label exist.";
        public const string NotFound = "Sync not found.";
        public const string SyncExist = "Sync exist.";
    }

    public static class User
    {
        public const string UserExist = "User exist.";
        public const string UserNameRequired = "User name required.";
        public const string EmailRequired = "Email required.";
        public const string EmailNotValid = "Email not valid.";
        public const string RolesRequired = "Roles required.";
        public const string MustRolesSet = "Must set role or more.";
        public const string RolesCannotbeDuplicated = "Roles can't be duplicated.";
        public const string PasswordRequired = "Password is required.";
        public const string ConfirmedPasswordRequired = "Confirmed password required.";
        public const string PasswordNotEqualConfurmedPassword =
            "Password not equal confirmed password.";
        public const string EmailExist = "Email exist.";
        public const string UserNameExist = "Username exist.";
        public const string NotFound = "User not found.";
        public const string EmailNotExist = "Email not exist.";
        public const string InvalidCredential = "Invalid login credential.";
        public const string CannotDeleteUser = "You haven't permission to delete this user.";
        public const string CannotUpdateUser = "You haven't permission to update this profile.";
    }

    public static class Role
    {
        public const string NameRequired = "Name is required.";
        public const string NameExist = "Role name exist.";
        public const string NotFound = "Role not found.";
        public const string RoleExist = "Role exist.";
        public const string AdminRoleNotFound =
            "Admin role not found, you must be go to seed roles first.";
        public const string CanntDeleteAdminRole = "can't delete admin role.";
    }

    public static class Source
    {
        public const string LabelRequired = "Source label required.";
        public const string DatabaseRequired = "Database required.";
        public const string UrlRequired = "Url required.";
        public const string NotFound = "Source not found.";
        public const string LabelExist = "Source label exist.";
        public const string UrlExist = "Source url exist.";
        public const string DatabaseExist = "Database exist.";
        public const string UnSuportedType = "Unsupported type.";
    }
}
