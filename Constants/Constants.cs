namespace EstateManager.Constants;

public static class GlobalConstants
{
    public const string DefaultCountry = "Nigeria";
    public const int MaxPageSize = 50;
    public const int DefaultPageSize = 10;
}

public static class PermissionConstants
{
    public const string CreateEstate = "CreateEstate";
    public const string UpdateEstate = "UpdateEstate";
    public const string DeleteEstate = "DeleteEstate";
    public const string CreateEstateBuilding = "CreateEstateBuilding";
    public const string UpdateEstateBuilding = "UpdateEstateBuilding";
    public const string DeleteEstateBuilding = "DeleteEstateBuilding";
}

public static class CustomClaimTypes
{
    public const string IsAdmin = "IsAdmin";
}
