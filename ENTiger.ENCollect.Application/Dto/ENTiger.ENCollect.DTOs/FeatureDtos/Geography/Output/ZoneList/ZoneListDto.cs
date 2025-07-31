using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ZoneListDto : DtoBridge
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }
    }
}