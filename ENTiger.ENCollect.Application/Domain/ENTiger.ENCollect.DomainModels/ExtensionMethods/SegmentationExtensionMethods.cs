namespace ENTiger.ENCollect
{
    public static class SegmentationExtensionMethods
    {
        public static IQueryable<T> BySegmentName<T>(this IQueryable<T> segments, string Name) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(Name))
            {
                segments = segments.Where(a => a.Name == Name);
            }
            return segments;
        }

        public static IQueryable<T> BySegmentCreatedDate<T>(this IQueryable<T> segments, DateTime? createdDate) where T : Segmentation
        {
            if (createdDate.HasValue && createdDate != DateTime.MinValue)
            {
                DateTime startDate = createdDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                segments = segments.Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDate);
            }
            return segments;
        }

        public static IQueryable<T> BySegmentCreatedBy<T>(this IQueryable<T> segments, string CreatedBy) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(CreatedBy))
            {
                segments = segments.Where(a => a.CreatedBy == CreatedBy);
            }
            return segments;
        }

        public static IQueryable<T> ByIsDeleted<T>(this IQueryable<T> segments) where T : Segmentation
        {
            segments = segments.Where(a => a.IsDeleted == false);

            return segments;
        }

        public static IQueryable<T> BySegmentProductGroup<T>(this IQueryable<T> segments, string ProductGroup) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(ProductGroup))
            {
                segments = segments.Where(a => a.ProductGroup.Trim().Equals(ProductGroup.Trim()));
            }
            return segments;
        }


        public static IQueryable<T> BySegmentProduct<T>(this IQueryable<T> segments, string Product) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(Product))
            {
                segments = segments.Where(a => a.Product.Trim().Equals(Product.Trim()));
            }
            return segments;
        }


        public static IQueryable<T> BySegmentSubProduct<T>(this IQueryable<T> segments, string SubProduct) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(SubProduct))
            {
                segments = segments.Where(a => a.SubProduct.Trim().Equals(SubProduct.Trim()));
            }
            return segments;
        }

        public static IQueryable<T> BySegmentBOM_Bucket<T>(this IQueryable<T> segments, string Bucket) where T : Segmentation
        {
            if (!string.IsNullOrEmpty(Bucket) && !Bucket.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                segments = segments.Where(a => a.BOM_Bucket.Equals(Bucket));
            }
            return segments;
        }

        public static IQueryable<T> BySegmentCurrentBucket<T>(this IQueryable<T> segments, string CurrentBucket) where T : Segmentation
        {
            if (!string.IsNullOrEmpty(CurrentBucket) && !CurrentBucket.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                segments = segments.Where(a => a.CurrentBucket.Equals(CurrentBucket));
            }

            return segments;
        }

        public static IQueryable<T> BySegmentNPAFlag<T>(this IQueryable<T> segments, string NpaFlag) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(NpaFlag))
            {
                segments = segments.Where(a => a.SegmentAdvanceFilter.NPA_STAGEID.Equals(NpaFlag));
            }
            return segments;
        }

        public static IQueryable<T> BySegmentZone<T>(this IQueryable<T> segments, string Zone) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(Zone))
            {
                segments = segments.Where(a => a.Zone.Equals(Zone));
            }
            return segments;
        }

        public static IQueryable<T> BySegmentState<T>(this IQueryable<T> segments, string State) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(State))
            {
                segments = segments.Where(a => a.State.Equals(State));
            }
            return segments;
        }

        public static IQueryable<T> BySegmentCity<T>(this IQueryable<T> segments, string City) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(City))
            {
                segments = segments.Where(a => a.City.Equals(City));
            }
            return segments;
        }

        public static IQueryable<T> BySegmentBranch<T>(this IQueryable<T> segments, string BranchId) where T : Segmentation
        {
            if (!String.IsNullOrEmpty(BranchId))
            {
                segments = segments.Where(a => a.Branch.Trim().Equals(BranchId.Trim()));
            }
            return segments;
        }

    }
}