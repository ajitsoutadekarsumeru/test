SELECT   
collec.id  AS collections_id_paymentid,			
collecBatch.lastmodifieddate AS collectionbatches_lastmodifieddate,
collecBatch.customid AS collectionbatches_customid_batchid,
collecBatch.createddate  AS collectionbatches_createddate_batchidcreateddate,
 COALESCE(collecBatch.amount,0.00)                                  AS collectionbatches_amount_batchamount,
(
	CASE
                              WHEN (( payinslipWFS.discriminator IS NOT NULL)  AND  (  payinslipWFS.discriminator <> '')) THEN payinslipWFS.discriminator
                              WHEN (( collecBatchWFS.discriminator IS NOT NULL) AND  (collecBatchWFS.discriminator <> '')) THEN collecBatchWFS.discriminator
                              ELSE collecWFS.discriminator   END
)            
																			AS paymentstatus
 FROM      collections collec
		  
          LEFT JOIN collectionbatches collecBatch   			ON   collec.collectionbatchid = collecBatch.id
          LEFT JOIN payinslips paynSlip          				ON   collecBatch.payinslipid = paynSlip.id
          LEFT JOIN payinslipworkflowstate payinslipWFS         ON   payinslipWFS.id = paynSlip.payinslipworkflowstateid
          LEFT JOIN collectionbatchworkflowstate collecBatchWFS ON   collecBatchWFS.id = collecBatch.collectionbatchworkflowstateid
          LEFT JOIN collectionworkflowstate collecWFS           ON   collecWFS.id = collec.collectionworkflowstateid
		  
		  
WHERE     collecBatch.lastmodifieddate > :sql_last_value 

ORDER BY  collec.lastmodifieddate ASC


