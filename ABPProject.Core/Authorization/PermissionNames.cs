namespace ABPProject.Authorization
{
    public static class PermissionNames
    {
        //public const string Pages = "Pages";
        //public const string Pages_Tenants = "Pages.Tenants";
        //public const string Pages_Users = "Pages.Users";

        public const string User = "User";
        public const string User_Create = "User.Create";
        public const string User_Delete = "User.Delete";
        public const string User_SetRole = "User.SetRole";

        public const string Role = "Role";
        public const string Role_Create = "Role.Create";
        public const string Role_Edit = "Role.Edit";
        public const string Role_Delete = "Role.Delete";
        public const string Role_SetPermission = "Role.SetPermission";

        public const string Project = "Project";
        public const string Project_Create = "Project.Create";
        public const string Project_Edit = "Project.Edit";
        public const string Project_Delete = "Project.Delete";

        public const string Product = "Product";
        public const string Product_Create = "Product.Create";
        public const string Product_Edit = "Product.Edit";
        public const string Product_Delete = "Product.Delete";
    }
}