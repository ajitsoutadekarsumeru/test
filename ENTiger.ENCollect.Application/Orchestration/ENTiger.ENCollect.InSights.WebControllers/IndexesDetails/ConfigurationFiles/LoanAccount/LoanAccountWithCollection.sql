
WITH CollectionStats AS (
    SELECT 
        accountid,
        COUNT(a.id) AS total_collections,
        SUM(CASE 
                WHEN MONTH(lastmodifieddate) = MONTH(CURRENT_DATE()) AND YEAR(lastmodifieddate) = YEAR(CURRENT_DATE()) 
                THEN 1 ELSE 0 
            END) AS collections_this_month
    FROM collections a inner join collectionworkflowstate b on a.CollectionWorkflowStateId = b.Id
    where b.Name in (
    'CollectionAcknowledged',
    'AddedCollectionInBatch',
    'ReceivedByCollector',
    'ReadyForBatch',
    'CancellationRequested',
    'AddedCollectionInPartnerBatch',
    'CollectionSuccess',
    'CancellationRejected'
    )
    GROUP BY accountid
)
SELECT 
    la.id AS id_loanaccounts, 
    collec.lastmodifieddate AS lastmodifieddate_loanaccounts,
    COALESCE(collec.Collectiondate, '') AS collection_collectiondate,
    COALESCE(cs.collections_this_month, 0) AS collec_totalcollectionsthismonth,
    COALESCE(cs.total_collections, 0) AS collec_totalcollectionstilldate,
    CASE 
        WHEN cs.collections_this_month > 0 THEN 'COLLECTED' 
        ELSE 'UNCOLLECTED' 
    END AS collection_status,
    collec.lastmodifieddate AS collec_lastmodifieddate
FROM collections AS collec
LEFT JOIN loanaccounts AS la ON collec.accountid = la.id
LEFT JOIN applicationuser collectioncollector ON collec.collectorid = collectioncollector.id
LEFT JOIN CollectionStats AS cs ON collec.accountid = cs.accountid
WHERE     collec.lastmodifieddate > :sql_last_value
ORDER BY collec.lastmodifieddate ASC
