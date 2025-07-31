using ENTiger.ENCollect.DesignationsModule;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Data Transfer Object (DTO) for retrieving designation details based on a permission scheme ID.
    /// Contains designation's Id, name, remarks, and associated designations list.
    /// </summary>
    public partial class GetDesignationDetailsBySchemeIdDto : DtoBridge
    {
        /// <summary>
        /// Gets or sets the unique identifier for the designation.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the designation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the remarks or description related to the designation.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the list of designations associated with the given permission scheme ID.
        /// </summary>
        public List<GetDesignationByPermissionSchemeIdDto> Designations { get; set; }
    }
}
