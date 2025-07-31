			SELECT   collec.id                                          AS collections_id_paymentid,

                        collecBatchWFS.StateChangedDate AS collectionbatchworkflowstate_lastmodifieddate,


				   
                  
					
					
					
					(
						CASE
                              WHEN (( payinslipWFS.discriminator IS NOT NULL)  AND  (  payinslipWFS.discriminator <> '')) THEN payinslipWFS.discriminator
                              WHEN (( collecBatchWFS.discriminator IS NOT NULL) AND  (collecBatchWFS.discriminator <> '')) THEN collecBatchWFS.discriminator
                              ELSE collecWFS.discriminator   END
					)            
																			AS paymentstatus,
																			
                    
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
                    ''                                                		AS bbpaypartneragentcode,
                    ''                                                		AS bbpaypartneragentemailid,
                    ''                                                		AS bbpaypartneragentmobilenumber,
                    ''                                                		AS bbpaypartnerbranchcode,
                    ''                                                		AS bbpaybatchackdate,
                    
                    REPLACE(CONCAT(appUser.firstname, ' ', appUser.middlename, ' ', appUser.lastname), '  ', ' ')          AS applicationuser_firstname_ackingagent, 
					(
						  CASE
                              WHEN  ((appUser.discriminator = 'NBFCCompanyUser') OR (appUser.discriminator = 'CompanyUser')
											OR (appUser.discriminator = 'ApplicationUser'))THEN 'STAFF'
							   WHEN  ((appUser.discriminator = 'NBFCCollectionAgencyUser') OR (appUser.discriminator = 'AgencyUser')) THEN 'AGENT'
                              END
					)            
																			AS applicationuser_stafforagent,
                    paynSlip.bankname                                 	AS payinslips_bankname_payinslipdepositbankname,
                
                    loanAcc.npa_stageid                               	AS loanaccounts_npa_stageid_npa_stageid,
                    
                    loanAcc.agreementid                               	AS loanaccounts_agreementid_agreementid,
                    loanAcc.current_pos                               	AS loanaccounts_current_pos_current_pos,
                    collec.lastmodifieddate							 	AS collections_lastmodifieddate_lastmodifieddate
					
					
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
		  
		  
WHERE     collecBatchWFS.StateChangedDate > :sql_last_value 

ORDER BY  collec.lastmodifieddate ASC


