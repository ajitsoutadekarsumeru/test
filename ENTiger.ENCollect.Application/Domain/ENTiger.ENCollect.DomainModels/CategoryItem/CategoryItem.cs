using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CategoryItem : DomainModelBridge
    {
        protected readonly ILogger<CategoryItem> _logger;

        public CategoryItem()
        {
        }

        public CategoryItem(ILogger<CategoryItem> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(250)]
        public string? Name { get; protected set; }

        [StringLength(32)]
        public string? CategoryMasterId { get; protected set; }

        public CategoryMaster CategoryMaster { get; protected set; }

        [StringLength(32)]
        public string? ParentId { get; protected set; }

        public CategoryItem Parent { get; protected set; }

        [StringLength(100)]
        public string? Code { get; protected set; }

        [StringLength(700)]
        public string? Description { get; protected set; }

        public bool? IsDisabled { get; protected set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Private Methods"

        public CategoryItem disableCategoryItem(CategoryItem categoryItem, string userId)
        {
            this.IsDeleted = true;
            this.LastModifiedBy = userId;
            return categoryItem;
        }

        #endregion "Private Methods"
    }
}