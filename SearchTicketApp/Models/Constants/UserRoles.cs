using System.Reflection;

namespace SearchTicketApp.Models.Constants
{
    public static class UserRoles
    {
        public const string User = nameof(User);
        public const string Admin = nameof(Admin);

        public static string[] GetRoles()
        {
            var userRolesType = typeof(UserRoles);

            return userRolesType.GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string))
                .Select(f => (string)f.GetValue(null)!).ToArray();
        }
    }
}
