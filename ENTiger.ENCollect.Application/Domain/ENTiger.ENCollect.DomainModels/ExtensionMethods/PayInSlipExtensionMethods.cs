using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class PayInSlipExtensionMethods
    {
        public static IQueryable<PayInSlip> CustomId(this IQueryable<PayInSlip> model, string value)
        {
            return model.Where(cm => cm.CustomId == value);
        }

        public static IQueryable<T> ById<T>(this IQueryable<T> model, string value) where T : PayInSlip
        {
            return model.Where(cm => cm.Id == value);
        }

        public static IQueryable<T> GetByCMSPayInSlipNo<T>(this IQueryable<T> model, string value) where T : PayInSlip
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CMSPayInSlipNo == value);
            }
            return model;
        }

        public static IQueryable<T> DateRange<T>(this IQueryable<T> model, DateTime? FromDate, DateTime? ToDate) where T : PayInSlip
        {
            if(FromDate.HasValue && FromDate != DateTime.MinValue)
            {
                DateTime startDate = FromDate.Value.Date;
                model = model.Where(c => c.CreatedDate >= startDate);
            }
            if (ToDate.HasValue && ToDate != DateTime.MinValue)
            {
                DateTime startDate = ToDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.CreatedDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByPayInSlipMode<T>(this IQueryable<T> model, string value) where T : PayInSlip
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.ModeOfPayment == value);
            }
            return model;
        }


        public static IQueryable<T> ByPayInSlipProductGroup<T>(this IQueryable<T> payInSlip, string ProductGroup) where T : PayInSlip
        {
            if (!string.IsNullOrEmpty(ProductGroup) && !string.Equals(ProductGroup, ProductGroupEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                payInSlip = payInSlip.Where(c => c.ProductGroup == ProductGroup);
            }
            return payInSlip;
        }


        public static IQueryable<T> ByPayInSlipTypeMode<T>(this IQueryable<T> payInSlip, string PayInSlipType) where T : PayInSlip
        {
            if (!string.IsNullOrEmpty(PayInSlipType))
            {
                payInSlip = payInSlip.Where(c => c.PayinslipType == PayInSlipType);
            }

            return payInSlip;
        }

        public static IQueryable<T> ByloggedInUser<T>(this IQueryable<T> model, string value) where T : PayInSlip
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CreatedBy == value);
            }
            return model;
        }

        public static IQueryable<T> ByAmount<T>(this IQueryable<T> model, decimal? value) where T : PayInSlip
        {
            if (value != null)
            {
                model = model.Where(c => c.Amount == value);
            }
            return model;
        }

        public static IQueryable<T> ByPaySlipIds<T>(this IQueryable<T> model, List<string> values) where T : TFlex
        {
            return model.Where(i => values.Contains(i.Id));
        }

        public static IQueryable<T> ByPayInSlipStatus<T>(this IQueryable<T> model) where T : PayInSlip
        {
            return model.Where(c => c.PayInSlipWorkflowState.Name == "PayInSlipCreated");
        }

        public static IQueryable<T> GetByCMSPayInSlipCode<T>(this IQueryable<T> model, string value) where T : PayInSlip
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> IncludePayInSlipUserWorkflow<T>(this IQueryable<T> model) where T : PayInSlip
        {
            return model.FlexInclude(x => x.PayInSlipWorkflowState);
        }
    }
}