using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSegmentById : FlexiQueryBridgeAsync<Segmentation, GetSegmentByIdDto>
    {
        protected readonly ILogger<GetSegmentById> _logger;
        protected GetSegmentByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetSegmentById(ILogger<GetSegmentById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetSegmentById AssignParameters(GetSegmentByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetSegmentByIdDto> Fetch()
        {
            var result = await Build<Segmentation>().SelectTo<GetSegmentByIdDto>().FirstOrDefaultAsync();
            if (result != null)
            {
                if (!string.Equals(result.ProductGroup, ProductGroupEnum.All.Value))
                {
                    result.ProductGroup = await FetchProductGroupId(string.IsNullOrEmpty(result.ProductGroup) ? "" : result.ProductGroup);
                }

                if (!string.Equals(result.Product, ProductCodeEnum.All.Value))
                {
                    result.Product = await FetchProductId(string.IsNullOrEmpty(result.Product) ? "" : result.Product);
                }

                if (!string.Equals(result.SubProduct, SubProductEnum.All.Value))
                {
                    result.SubProduct = await FetchSubProductId(string.IsNullOrEmpty(result.SubProduct) ? "" : result.SubProduct);
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id);
            return query;
        }

        private async Task<string> FetchProductGroupId(string name)
        {
            string id = string.Empty;
            string categorymasterid = string.Empty;

            var cm = await _repoFactory.GetRepo().FindAll<CategoryMaster>().Where(a => string.Equals(a.Name, CategoryMasterEnum.ProductGroup.Value)).FirstOrDefaultAsync();

            if (cm != null)
            {
                categorymasterid = cm.Id;  // Assuming Id is not a string, so we convert it to string if necessary
            }

            var item = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => string.Equals(a.Name.Trim(), name.Trim()) && string.Equals(a.CategoryMasterId.ToString(), categorymasterid)).FirstOrDefaultAsync();

            if (item != null)
            {
                id = item.Id;
            }


            return id;
        }

        private async Task<string> FetchProductId(string name)
        {
            string id = string.Empty;
            string categorymasterid = string.Empty;

            var cm = await _repoFactory.GetRepo().FindAll<CategoryMaster>().Where(a => string.Equals(a.Name, CategoryMasterEnum.Product.Value)).FirstOrDefaultAsync();

            if (cm != null)
            {
                categorymasterid = cm.Id.ToString();  // Ensure Id is treated as a string (if not already)
            }

            var item = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => string.Equals(a.Name.Trim(), name.Trim()) && string.Equals(a.CategoryMasterId.ToString(), categorymasterid)).FirstOrDefaultAsync();

            if (item != null)
            {
                id = item.Id;
            }


            return id;
        }

        private async Task<string> FetchSubProductId(string name)
        {
            string id = string.Empty;
            string categorymasterid = string.Empty;

            var cm = await _repoFactory.GetRepo().FindAll<CategoryMaster>().Where(a => string.Equals(a.Id.ToString(), CategoryMasterEnum.SubProduct.Value)).FirstOrDefaultAsync();

            if (cm != null)
            {
                categorymasterid = cm.Id.ToString();  // Ensure Id is treated as a string (if not already)
            }

            var item = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => string.Equals(a.Name.Trim(), name.Trim()) && string.Equals(a.CategoryMasterId.ToString(), categorymasterid)).FirstOrDefaultAsync();

            if (item != null)
            {
                id = item.Id;
            }


            return id;
        }
    }

    public class GetSegmentByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}