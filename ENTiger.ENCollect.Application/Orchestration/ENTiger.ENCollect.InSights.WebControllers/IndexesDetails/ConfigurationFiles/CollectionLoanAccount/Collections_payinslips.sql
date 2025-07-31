			SELECT   collec.id                                          AS collections_id_paymentid,
					paynSlip.createddate                                AS payinslips_createddate_depositdate,
					(case when paynSlip.createddate is null then ''  
					  else DATE_FORMAT(paynSlip.createddate, '%Y-%m') end )AS deposit_month, 
					
					
                        paynSlip.lastmodifieddate AS payinslips_lastmodifieddate,
					(
						CASE
                              WHEN ( paynSlip.id = '1') THEN paynSlip.id
                              ELSE paynSlip.customid  END
					)                    									
																			AS payinslips_encollectpayinslipid,
					paynSlip.cmspayinslipno 							AS payinslips_cmspayinslipno_cmspayinslipid,
                    paynSlip.bankaccountno  							AS payinslips_bankaccountno_depositaccountnumber,
                    paynSlip.bankname       							AS payinslips_bankname_depositebankname,
                    COALESCE(paynSlip.amount,0.00)         							AS payinslips_amount_depositamount,
					
                    paynSlip.bankname                                 	AS payinslips_bankname_payinslipdepositbankname,				
					 
					 ''                                                		AS merchantreferencenumber,
                    ''                                                		AS mid,
                    ''                                                		AS banktransactionid,
                    ''                                                		AS bankid,
					
                    ''                                                		AS statuscode,
                    ''                                                		AS rrn,
                    ''                                                		AS authcode,
                    ''                                                		AS cardnumber,
					''                                                		AS cardtype,
                    ''                                                		AS cardholdername,
                    ''                                                		AS merchantid,
					''                                                		AS merchanttransactionid,
					''                                                   	AS paymenttowards,
					
					
                    
                    		  
                    
					(
						CASE
                              WHEN (( payinslipWFS.discriminator IS NOT NULL)  AND  (  payinslipWFS.discriminator <> '')) THEN payinslipWFS.discriminator
                              WHEN (( collecBatchWFS.discriminator IS NOT NULL) AND  (collecBatchWFS.discriminator <> '')) THEN collecBatchWFS.discriminator
                              ELSE collecWFS.discriminator   END
					)            
																			AS paymentstatus
                    
					
          FROM      collections collec
		  
          LEFT JOIN collectionbatches collecBatch   			ON  collec.collectionbatchid = collecBatch.id
          LEFT JOIN payinslips paynSlip          				ON  collecBatch.payinslipid = paynSlip.id
          LEFT JOIN payinslipworkflowstate payinslipWFS         ON  payinslipWFS.id = paynSlip.payinslipworkflowstateid
          LEFT JOIN collectionbatchworkflowstate collecBatchWFS ON  collecBatchWFS.id = collecBatch.collectionbatchworkflowstateid
          LEFT JOIN collectionworkflowstate collecWFS           ON  collecWFS.id = collec.collectionworkflowstateid
		  
		  
 WHERE     paynSlip.lastmodifieddate > :sql_last_value 

ORDER BY  collec.lastmodifieddate ASC

