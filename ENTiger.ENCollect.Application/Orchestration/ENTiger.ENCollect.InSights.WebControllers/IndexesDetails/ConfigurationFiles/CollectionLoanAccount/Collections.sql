			SELECT   collec.id                                          AS collections_id_paymentid,
					collec.lastmodifieddate                    			AS lastmodifieddate , 
					collec.collectionorgid                    			AS collections_collectionorgid , 
					collec.collectorid                    			AS collections_collectorid , 
					
					collec.Latitude                                      	AS loanaccounts_latestlatitude_lat,
					collec.Longitude                                      	AS loanaccounts_latestlongitude_long,
			
                    COALESCE(appOrg.customid,'')                                 	AS applicationorg_customid_agencyid,
                    concat(COALESCE(appOrg.firstname,''),'',COALESCE(apporg.lastname,'')) 	AS applicationorg_agency_agencyname,			
                    COALESCE(appOrg.id,'')                                	        AS applicationorg_agency_id,
                    appUser.primaryemail                            	AS applicationuser_primaryemail_agentemail,
                   concat(COALESCE(appUser.firstname,''),'',COALESCE(appUser.lastname,''))     AS applicationuser_agent_agentname,
                    appUser.customid                                	AS applicationuser_customid_agentid,
					COALESCE(appUser.id,'')                             AS applicationuser_agent_id,
					appUser.PrimaryMobileNumber                         AS applicationuser_mobilenumber_agentid,
					collec.customid                                     AS collections_customid_receiptno,
                    collec.physicalreceiptnumber                        AS collections_physicalreceiptnumber_physicalreceiptno,
                    collec.createddate                                  AS collections_createddate_receiptdate,
					
                    collec.collectiondate  AS           collections_collectiondate_collectiondate,
                    
                    collec.customername                                 AS collections_customername_customername,
            		
					'0.00'                              AS collections_amountbreakup1_amountbreakup1,
                    ''                               AS collections_amountbreakup2_amountbreakup2,
                    ''                               AS collections_amountbreakup3_amountbreakup3,
                    ''                               AS collections_amountbreakup4_amountbreakup4,
                    ''                               AS collections_amountbreakup5_amountbreakup5,
                    ''                               AS collections_amountbreakup6_amountbreakup6,
					
			        COALESCE(collec.yoverdueamount,'0')                               AS collections_yoverdueamount_emiamt,
                    COALESCE(collec.ypenalinterest,'0')                               AS collections_ypenalinterest_latepaymentpenalty,
                    COALESCE(collec.ybouncecharges,'0')                             AS collections_ybouncecharges_bouncecharges,                   
                    COALESCE(collec.othercharges,'0')                               AS collections_othercharges_othercharges,
                    COALESCE(collec.yPenalInterest,'0')                               AS collections_yPenalInterest_yPenalInterest,
                    ''                                                   	AS excess,
                    ''                                                   	AS imd,
                    ''                                                   	AS procfee,
                    ''                                                   	AS swap,
                   '0'                                                  	AS ebccharge,
                     '0'                                                 	AS collectionpickupcharge,
					'0'                                                  	AS settlementamount,					
                    COALESCE(collec.yforeclosureamount,'0')                           AS collections_yforeclosureamount_foreclosureamount,
                    COALESCE(collec.amount,0.00)                                       AS collections_amount_totalreceiptamount,
                    collec.collectionmode                               AS collections_collectionmode_paymentmode,
                    chq.instrumentdate                               	AS cheques_instrumentdate_instrumentdate,
                    chq.instrumentno                                 	AS cheques_instrumentno_instrumentno,
                    COALESCE(collec.amount,0.00)                                       AS collections_amount_instrumentamount,
                    chq.micrcode                                     	AS cheques_micrcode_micrcode,
                    collec.ypanno                                       AS collections_ypanno_pancardno,
                    collecBatch.customid                                AS collectionbatches_customid_batchid,


                     collecBatch.createddate                                AS collectionbatches_createddate_batchidcreateddate,
                        
					 paynSlip.createddate AS payinslips_createddate_depositdate,
					  
					(case when paynSlip.createddate is null then ''  
					  else DATE_FORMAT(paynSlip.createddate, '%Y-%m') end )AS deposit_month, 
					  
					(case when collec.collectiondate is null then ''  
					  else DATE_FORMAT(collec.collectiondate, '%Y-%m') end )AS collections_month, 
					
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
                    COALESCE(collec.amount,0.00)                                       AS collections_amount_amount,
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
					''                                 AS loanaccounts_branch_code_branchid,
                    
                    REPLACE(CONCAT(appUser.firstname, ' ', appUser.middlename, ' ', appUser.lastname), '  ', ' ')          AS applicationuser_firstname_ackingagent, 
					(
						  
							    CASE
                              WHEN  (appUser.discriminator = 'CompanyUser') THEN 'STAFF'
							   WHEN  (appUser.discriminator = 'AgencyUser') THEN 'AGENT'
                              END
					)            
																			AS applicationuser_stafforagent,
                    paynSlip.bankname                                 	AS payinslips_bankname_payinslipdepositbankname,
                    collec.lastmodifieddate							 	AS collections_lastmodifieddate_lastmodifieddate,
					
					(case  
					WHEN DATEDIFF(collec.CollectionDate,CURDATE()) between 0 and 5 THEN '0-5 Days'
					WHEN DATEDIFF(collec.CollectionDate,CURDATE()) between 6 and 10 THEN '6-10 Days'
					WHEN DATEDIFF(collec.CollectionDate,CURDATE()) between 11 and 15 THEN '11-15 Days'
					WHEN DATEDIFF(collec.CollectionDate,CURDATE()) between 16 and 20 THEN '16-20 Days'
                    WHEN DATEDIFF(collec.CollectionDate,CURDATE()) between 21 and 25 THEN '21-25 Days'
					ELSE
					'> 26 Days'
					
					end)  as 'collections_days_bucket'
					
                    -- (case when collec.amount between 0.00 and 5000.00 then '0-5K'
                   --  when collec.amount between 5000.00 and 10000.00 then '6-10K'
                   -- when collec.amount between 10000.00 and 15000.00 then '11-15K'
                   --  when collec.amount between 15000.00 and 20000.00 then '16-20K'
                    -- when collec.amount between 20000.00 and 25000.00 then '21-25K'
                    -- when collec.amount  > 25000.00 then '>25K'end)  as 'loan_amount_bucket'
					
					
          FROM     collections collec
		  
          LEFT JOIN applicationuser appUser     				ON      collec.collectorid = appUser.id
          LEFT JOIN applicationorg appOrg       				ON      appUser.agencyid = appOrg.id
          LEFT JOIN cheques chq          						ON      collec.chequeid = chq.id
          LEFT JOIN collectionbatches collecBatch   			ON      collec.collectionbatchid = collecBatch.id
          LEFT JOIN payinslips paynSlip          				ON      collecBatch.payinslipid = paynSlip.id
          LEFT JOIN payinslipworkflowstate payinslipWFS         ON      payinslipWFS.id = paynSlip.payinslipworkflowstateid
          LEFT JOIN collectionbatchworkflowstate collecBatchWFS ON      collecBatchWFS.id = collecBatch.collectionbatchworkflowstateid
          LEFT JOIN collectionworkflowstate collecWFS           ON      collecWFS.id = collec.collectionworkflowstateid
		  
WHERE     collec.lastmodifieddate > :sql_last_value 

ORDER BY  collec.lastmodifieddate ASC




