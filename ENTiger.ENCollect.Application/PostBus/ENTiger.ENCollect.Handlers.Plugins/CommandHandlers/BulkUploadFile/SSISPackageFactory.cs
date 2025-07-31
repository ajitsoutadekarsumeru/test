public static class SSISPackageFactory
{
    public static ISSISPackageStrategy GetStrategy(string fileType)
    {
        switch (fileType)
        {
            case "PrimaryAllocation":
            // return new PrimaryAllocStrategy();
            case "SecondaryAllocation":
            //return new SecondaryAllocStrategy();
            case "BulkTrailUpload":
                return new FeedbackTrailStrategy();

            case "AccountImport":
            //return new AccountImportStrategy();
            //// Add more cases as new file types are introduced
            //case "User Upload":
            //    return new UserUploadStrategy();
            default:
                throw new InvalidOperationException("Unsupported file type");
        }
    }
}