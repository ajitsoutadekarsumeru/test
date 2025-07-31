			SELECT   collec.id                                          AS collections_id_paymentid,
                        loanAcc.lastmodifieddate AS loanaccounts_lastmodifieddate,


		   COALESCE(loanAcc.product,'')                                     AS loanaccounts_product,
		 COALESCE(loanAcc.productgroup,'')                              	AS loanaccounts_productgroup,
					
					''                                AS loanaccounts_branch_code_location,
                    COALESCE(loanAcc.subproduct,'')                                 AS loanaccounts_subproduct,
                    COALESCE(loanAcc.state,'')                                       AS loanaccounts_state_state,
					loanAcc.collectorid                                       AS collectorid_loanaccounts,
					loanAcc.telecallerid                                       AS telecallerid_loanaccounts,
					loanAcc.telecallingagencyid                                       AS telecallingagencyid_loanaccounts,
					loanAcc.agencyid                                       AS agencyid_loanaccounts,
					COALESCE(loanAcc.region,'')                                    	AS loanaccounts_region_region,
						loanAcc.zone                                    	AS loanaccounts_zone,
					COALESCE(loanAcc.city,'')                                      	AS loanaccounts_city_city,
					
					loanAcc.OTHER_CHARGES                                      	AS loanaccounts_other_charges_other_charges,
		
                 
                    COALESCE(loanAcc.branch,'')                                      AS loanaccounts_branch_branchname,
                  
                    loanAcc.customid                                    AS loanaccounts_customid_referenceno,
                    
                    loanAcc.customerid                                  AS loanaccounts_customerid_customerid,
                    loanAcc.primary_card_number                         AS loanaccounts_primary_card_number_primary_card_number,
	                COALESCE(loanAcc.current_bucket,0) 								AS loanaccounts_current_bucket_currentbucket,
                    COALESCE(loanAcc.bucket,0)							      		AS loanaccounts_bucket_bombucket,
                    loanAcc.npa_stageid                               	AS loanaccounts_npa_stageid_npa_stageid,
                    
                    loanAcc.agreementid                               	AS loanaccounts_agreementid_agreementid,
                    cast(loanAcc.current_pos as DECIMAL(18,2))                              	AS loanaccounts_current_pos_current_pos,
					(
	 case when coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) >= 0.00 and coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0)<=10000.00 then '0-10k'
     when coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) > 10000.00 and coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) <= 20000.00  then '11-20k'
	 when coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) > 20000.00 and coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) <= 30000.00 then '21-30k'
	 when coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) > 30000.00 and coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) <= 40000.00 then '31-40k'
	 when coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) > 40000.00 and coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) <= 50000.00 then '41-50k'
	 when coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0) > cast(50000.00 as decimal(18,2)) then '>50k' end  ) as loan_amount_bucket,
	 coalesce(loanAcc.CURRENT_TOTAL_AMOUNT_DUE,0.00) as total_overdue_amt
				
				
					
     				
					
          FROM      collections collec
		  
          LEFT JOIN loanaccounts loanAcc         				ON  collec.accountid = loanAcc.id
          
		  
 WHERE     loanAcc.lastmodifieddate > :sql_last_value 

ORDER BY  collec.lastmodifieddate ASC



