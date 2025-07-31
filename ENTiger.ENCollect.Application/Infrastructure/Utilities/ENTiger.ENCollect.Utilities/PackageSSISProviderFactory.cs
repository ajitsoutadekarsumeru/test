namespace ENTiger.ENCollect
{
    public class PackageSSISProviderFactory
    {
        public PackageSSISProviderFactory()
        {
        }

        public ISSISPackageProvider GetSSISPackageProvider(string fileType)
        {
            return fileType switch
            {
                "PrimaryAllocation" => new PrimaryAllocationPackage(),
                "SecondaryAllocation" => new SecondaryAllocationPackage(),
                "BulkTrailUpload" => new BulkTrailImportPackage(),
                "AccountImportFile" => new AccountImportPackage(),
                "CollectionBulkUpload" => new CollectionBulkImportPackage(),
                _ => throw new InvalidOperationException("Invalid file type")
            };
        }
    }
}