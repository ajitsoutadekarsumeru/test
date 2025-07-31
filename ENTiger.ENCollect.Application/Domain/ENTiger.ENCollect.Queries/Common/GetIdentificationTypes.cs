using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetIdentificationTypes : FlexiQueryEnumerableBridgeAsync<GetIdentificationTypesDto>
    {
        protected readonly ILogger<GetIdentificationTypes> _logger;
        protected GetIdentificationTypesParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetIdentificationTypes(ILogger<GetIdentificationTypes> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetIdentificationTypes AssignParameters(GetIdentificationTypesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetIdentificationTypesDto>> Fetch()
        {
            _repoFactory.Init(_params);

            ICollection<TFlexIdentificationType> identificationTypes;

            //var result = _repoFactory.GetRepo().FindAll<TFlexIdentificationType>().FlexInclude(d => d.IdentificationDocTypes).SelectTo<GetIdentificationTypesDto>().ToListAsync();

            identificationTypes = await _repoFactory.GetRepo().FindAll<TFlexIdentificationType>().FlexInclude(d => d.IdentificationDocTypes).ToListAsync();

            List<GetIdentificationTypesDto> outputmodel = new List<GetIdentificationTypesDto>();

            foreach (TFlexIdentificationType identificationtype in identificationTypes)
            {
                GetIdentificationTypesDto identificationmodel = new GetIdentificationTypesDto();//FlexOpus.Convert<TFlexIdentificationType, GetIdentificationTypesDto>(identificationtype);
                identificationmodel.Id = identificationtype.Id;
                identificationmodel.IdentificationType = identificationtype.Description;
                identificationmodel.IdentificationDocTypes = new List<IdentificationDoctypeOutputModel>();
                foreach (TFlexIdentificationDocType doctype in identificationtype.IdentificationDocTypes)
                {
                    IdentificationDoctypeOutputModel docApimodel = new IdentificationDoctypeOutputModel();//FlexOpus.Convert<TFlexIdentificationDocType, IdentificationDoctypeOutputModel>(doctype);
                    docApimodel.Id = doctype.Id;
                    docApimodel.IdentificationDoc = doctype.Description;

                    identificationmodel.IdentificationDocTypes.Add(docApimodel);
                }
                outputmodel.Add(identificationmodel);
            }
            return outputmodel;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetIdentificationTypesParams : DtoBridge
    {
    }
}