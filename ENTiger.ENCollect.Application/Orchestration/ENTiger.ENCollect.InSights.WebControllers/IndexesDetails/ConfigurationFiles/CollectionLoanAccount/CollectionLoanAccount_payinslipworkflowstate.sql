			SELECT   collec.id                                          AS collections_id_paymentid,
                        payinslipWFS.StateChangedDate AS payinslipworkflowstate_lastmodifieddate,


							
					(
						CASE
                              WHEN (( payinslipWFS.discriminator IS NOT NULL)  AND  (  payinslipWFS.discriminator <> '')) THEN payinslipWFS.discriminator
                              WHEN (( collecBatchWFS.discriminator IS NOT NULL) AND  (collecBatchWFS.discriminator <> '')) THEN collecBatchWFS.discriminator
                              ELSE collecWFS.discriminator   END
					)            
																			AS paymentstatus
																			
                    
                   
                   
					
					
          FROM      (((((((((collections collec
		  
          LEFT JOIN applicationuser appUser     				ON  ((    collec.collectorid = appUser.id)))
          LEFT JOIN applicationorg appOrg       				ON  ((    appUser.agencyid = appOrg.id)))
          LEFT JOIN cheques chq          						ON  ((    collec.chequeid = chq.id)))
          LEFT JOIN collectionbatches collecBatch   			ON  ((    collec.collectionbatchid = collecBatch.id)))
          LEFT JOIN payinslips paynSlip          				ON  ((    collecBatch.payinslipid = paynSlip.id)))
          LEFT JOIN loanaccounts loanAcc         				ON  ((    collec.accountid = loanAcc.id)))
          LEFT JOIN payinslipworkflowstate payinslipWFS         ON  ((    payinslipWFS.id = paynSlip.payinslipworkflowstateid)))
          LEFT JOIN collectionbatchworkflowstate collecBatchWFS ON  ((    collecBatchWFS.id = collecBatch.collectionbatchworkflowstateid)))
          LEFT JOIN collectionworkflowstate collecWFS           ON  ((collecWFS.id = collec.collectionworkflowstateid)))
		  
		  
WHERE     payinslipWFS.StateChangedDate > :sql_last_value 

ORDER BY  collec.lastmodifieddate ASC



