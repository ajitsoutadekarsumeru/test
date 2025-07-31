using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserSearchCriteriaDtoWithId : UserSearchCriteriaDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}