SELECT    coalesce(la.branch,'')                               AS branch_loanaccounts, 
          coalesce(la.paymentStatus ,'')                         AS paymentStatus_loanaccounts, 
          coalesce(la.customid,'')                             AS customid_loanaccounts, 
          coalesce(la.customername,'')                         AS customername_loanaccounts, 
          coalesce(la.productgroup ,'')                        AS productgroup_loanaccounts, 
          coalesce(la.product,'')                              AS product_loanaccounts, 
          coalesce(la.subproduct,'')                           AS subproduct_loanaccounts, 
          coalesce(la.bucket,'')                               AS bucket_loanaccounts, 
          CASE 
			WHEN la.current_bucket > 6 THEN '6+'
			ELSE COALESCE(la.current_bucket, '')
			END AS current_bucket_loanaccounts, 
          coalesce(la.bom_pos,0.00)                              AS bom_pos_loanaccounts, 
          coalesce(la.current_pos,0.00)                          AS bom_current_pos_loanaccounts, 
          coalesce(la.state,'')                                AS state_loanaccounts, 
		  coalesce(la.city,'')                                 AS city_loanaccounts, 
		  coalesce(la.npa_stageid ,'')                         AS npa_stageid_loanaccounts, 
          coalesce(la.zone,'')                                 AS zone_loanaccounts, 
          coalesce(la.region,'')                               AS region_loanaccounts, 
          coalesce(la.telecallingagencyid,'')                  AS telecallingagencyid_loanaccounts, 
          coalesce(la.telecallerid ,'')                        AS telecallerid_loanaccounts, 
          coalesce(la.agencyid ,'')                            AS agencyid_loanaccounts, 
          coalesce(la.collectorid,'')                          AS collectorid_loanaccounts, 
          coalesce(la.allocationownerid,'')                    AS allocationownerid_loanaccounts, 
          coalesce(la.agreementid,'')                          AS agreementid_loanacounts, 

          la.lastmodifieddate as            lastmodifieddate_loanaccounts,

          la.id                                   AS id_loanaccounts, 

          la.lastmodifieddate as                lastmodifieddate,
         
          coalesce(la.principal_od,0.00) as principal_od,
		  coalesce(la.interest_od,0.00) as interest_od,
		  null as charge_overdue,
          null as total_overdue,
		  coalesce(la.disbursedamount,0.00) as total_disbursed_amount,
		  coalesce(la.emiamt,0.00) as emi,
		  la.pan_card_details as pan_no,
		  la.residential_customer_city as comu_city_name,
		  la.residential_customer_state as comu_state_name,
		  la.residential_pin_code as cust_comu_pin_code,		  
		  '' as occupation,
		  la.email_id as email,
		  la.co_applicant1_name as coappl_name,		  
		  la.emi_start_date as emi_start_date,
		  null as last_due_date,
		  la.scheme_desc as scheme_code,
		  la.overdue_days as overduedays,
		  null as sanct_lim,
		  coalesce(la.total_outstanding,0.00) as amountoutstanding,
		  
         
		  
		  '' as cust_mobile_no,
		  '' as last_disb_date,
		  '' as bob_group_emp_flag,
		  '' as coappl_mobileno,
		  '' as emi_frequency,
          
          agency.customid                         AS agency_applicationorg_customid, 
          REPLACE(CONCAT(coalesce(agency.firstname,''), ' ', coalesce(agency.middlename,''), ' ', coalesce(agency.lastname,'')), '  ', ' ')          AS agency_applicationorg_firstname, 
          REPLACE(CONCAT(agency.firstname, ' ', agency.middlename, ' ', agency.lastname), '  ', ' ')          AS agency_applicationorg_primaryownerfirstname, 
          coalesce(agency.id,'')                               AS agency_applicationorg_id, 
		  agency.lastmodifieddate                 AS agency_applicationorg_lastmodifieddate, 
          
          
          telecallingagency.customid              AS telecallingagency_applicationorg_customid, 
          REPLACE(CONCAT(coalesce(telecallingagency.firstname,''), ' ', coalesce(telecallingagency.middlename,''), ' ', coalesce(telecallingagency.lastname,'')), '  ', ' ')          AS telecallingagency_applicationorg_firstname, 
          REPLACE(CONCAT(telecallingagency.firstname, ' ', telecallingagency.middlename, ' ', telecallingagency.lastname), '  ', ' ')          AS telecallingagency_applicationorg_primaryownerfirstname, 
          coalesce(telecallingagency.id,'')              AS telecallingagency_applicationorg_id, 
		  telecallingagency.lastmodifieddate        AS telecallingagency_applicationorg_lastmodifieddate, 
          
          
          allocationowner.primaryemail            AS allocationowner_applicationuser_primaryemail,
          REPLACE(CONCAT(coalesce(allocationowner.firstname,''), ' ', coalesce(allocationowner.middlename,''), ' ', coalesce(allocationowner.lastname,'')), '  ', ' ')          AS allocationowner_applicationuser_firstname, 
          allocationowner.customid                AS allocationowner_applicationuser_customid, -- Allocation_Owner_Code
		  allocationowner.customid                AS allocation_owner_code_allocationowner_applicationuser_customid, 
		  
          allocationowner.primarymobilenumber     AS allocationowner_applicationuser_primarymobilenumber,
          coalesce(allocationowner.id,'')                	AS allocationowner_applicationuser_id , 
		  allocationowner.lastmodifieddate        AS allocationowner_applicationuser_lastmodifieddate ,	

	  
          collector.primaryemail                  AS collector_applicationuser_primaryemail, 
          REPLACE(CONCAT(coalesce(collector.firstname,''), ' ', coalesce(collector.middlename,''), ' ', coalesce(collector.lastname,'')), '  ', ' ')          AS collector_applicationuser_firstname, 
          collector.customid                      AS collector_applicationuser_customid, 
          collector.primarymobilenumber           AS collector_applicationuser_primarymobilenumber,
          coalesce(collector.id,'')                            AS collector_applicationuser_id ,
		collector.lastmodifieddate                AS collector_applicationuser_lastmodifieddate ,	
 
          tellecaller.primaryemail                AS tellecaller_applicationuser_primaryemail, 
          REPLACE(CONCAT(coalesce(tellecaller.firstname,''), ' ', coalesce(tellecaller.middlename,''), ' ', coalesce(tellecaller.lastname,'')), '  ', ' ')          AS tellecaller_applicationuser_firstname,  
          tellecaller.customid                    AS tellecaller_applicationuser_customid, 
          tellecaller.primarymobilenumber         AS tellecaller_applicationuser_primarymobilenumber,
          coalesce(tellecaller.id,'')                    AS tellecaller_applicationuser_id , 
		  tellecaller.lastmodifieddate             AS tellecaller_applicationuser_lastmodifieddate , 
          
        
          desig.name                              AS desig_designation_name, -- Allocation_Owner_Role
		  desig.name                              AS allocation_owner_role_desig_designation_name, -- Allocation_Owner_Role
          desig.id                                AS desig_designation_id, 
		  desig.lastmodifieddate                  AS desig_designation_lastmodifieddate, 
	      year AS year_loanaccounts,
		  month as month_loanaccounts,
				 
				 ( CASE WHEN ( la.telecallingagencyid IS NULL  or la.telecallingagencyid = '') THEN 'Gap' ELSE 'Allocated' end) AS primaryallocationstatusfortelecallingagency, 	 
				 
				 ( CASE WHEN ( la.agencyid IS NULL or la.agencyid = '') THEN 'Gap' ELSE 'Allocated' end) AS primaryallocationstatusforfieldagency,
				 ( CASE WHEN ( la.telecallerid IS NULL or la.telecallerid = '') THEN 'Gap' ELSE 'Allocated' end) AS secondaryallocationstatusfortelecallingagent, 
				 ( CASE WHEN ( la.collectorid IS NULL or la.collectorid = '') THEN 'Gap' ELSE 'Allocated' end) AS secondaryallocationstatusforfieldagent, 
				 
				 
				  ( CASE WHEN  ( (la.agencyid <> '' AND la.agencyid IS NOT NULL )  or (la.telecallingagencyid <> '' AND la.telecallingagencyid IS NOT NULL)) THEN 'Account Allocated' else 'Account Not Allocated' END ) AS primaryallocationstatus,
				  
				    ( CASE WHEN  ( (la.collectorid <> '' AND la.collectorid IS NOT NULL)  or (la.telecallerid <> '' AND la.telecallerid IS NOT NULL)) THEN 'Account Allocated to secondary' else 'Account Not Allocated to secondary' END ) AS secondaryallocationstatus,

					(case when la.TeleCallingAgencyId is not null and la.AgencyId is null then 'Telecaller'
     when la.TeleCallingAgencyId is   null and la.AgencyId is not null then 'Agency'
	 when la.TeleCallingAgencyId is not null and la.AgencyId is not null then 'Both'
	 else ''
	 end) as entity,
	 (case when la.telecallerid is not null and la.collectorid is null then 'Telecaller Secondary'
     when la.telecallerid is   null and la.collectorid is not null then 'Field Secondary'
	 when la.telecallerid is not null and la.collectorid is not null then 'Both'
	 else ''
	 end) as secondaryentity,
     (
	 case when coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) >= 0.00 and coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0)<=10000.00 then '0-10k'
     when coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) > 10000.00 and coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) <= 20000.00  then '11-20k'
	 when coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) > 20000.00 and coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) <= 30000.00 then '21-30k'
	 when coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) > 30000.00 and coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) <= 40000.00 then '31-40k'
	 when coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) > 40000.00 and coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) <= 50000.00 then '41-50k'
	 when coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0) > cast(50000.00 as decimal(18,2)) then '>50k' end  ) as loan_bucket,


                    (CASE WHEN 
	  ( ( la.agencyid IS NOT NULL)	 OR ( la.telecallerid IS NOT NULL) 
				 OR ( la.telecallingagencyid IS NOT NULL) OR ( la.collectorid IS NOT NULL))  
				 THEN 'Account Allocated' 
				 
				 
				 ELSE 'Account Not Allocated' end ) AS accountallocationstatus,
				   (
						CASE
                              WHEN  ( (collector.discriminator = 'CompanyUser')
											OR (collector.discriminator = 'ApplicationUser'))THEN 'STAFF'
							   WHEN  ((collector.discriminator = 'AgencyUser')) THEN 'AGENT'
                              END
					)            
																			AS collector_applicationuser_stafforagent,
					(
						CASE
                              WHEN  ((agency.discriminator = 'Agency') or (telecallingagency.discriminator = 'Agency'))THEN 'Agency'
							   WHEN  ((agency.discriminator = 'BaseBranch') or (telecallingagency.discriminator = 'BaseBranch')) THEN 'Branch'
							   ELSE ''
                              END
					)   AS application_org_discriminator,
					coalesce(la.CURRENT_TOTAL_AMOUNT_DUE,0.00) as total_overdue_amt,
							coalesce((select coalesce(fb.DispositionCode,'') from feedback fb where fb.accountid = la.id order by fb.createddate desc limit 1),'') AS fbak_dispositioncode,
					coalesce((select coalesce(fb.DispositionGroup,'') from feedback fb where fb.accountid = la.id order by fb.createddate desc limit 1),'') AS fbak_dispositiongroup,
				   			(SELECT Count(id) FROM feedback WHERE accountid = la.id ) AS fbak_totalfeedbackstilldate, 
			
	      	      (case when ( SELECT COALESCE(Count(id),0)  FROM feedback WHERE accountid = la.id AND Month(lastmodifieddate) = Month(CURRENT_DATE()) AND Year(lastmodifieddate) = Year(CURRENT_DATE()) 
          ) > 0 then 'ATTEMPTED' else 'UNATTEMPTED' end) 
          AS feedback_status,
		    (case when ( SELECT COALESCE(Count(id),0)  FROM collections WHERE accountid = la.id AND Month(lastmodifieddate) = Month(CURRENT_DATE()) AND Year(lastmodifieddate) = Year(CURRENT_DATE()) 
          ) > 0 then 'COLLECTED' else 'UNCOLLECTED' end) 
          AS collection_status,
		             ( SELECT COALESCE(Count(id),0) FROM feedback WHERE accountid = la.id AND Month(lastmodifieddate) = Month(CURRENT_DATE()) AND Year(lastmodifieddate) = Year(CURRENT_DATE())) 
           AS fbak_totalfeedbacksthismonth

FROM      loanaccounts          AS la 

LEFT JOIN applicationuser tellecaller ON        la.telecallerid = tellecaller.id 
LEFT JOIN applicationuser collector ON        la.collectorid = collector.id 
LEFT JOIN applicationuser allocationowner ON        la.allocationownerid = allocationowner.id 
LEFT JOIN applicationorg telecallingagency ON        la.telecallingagencyid = telecallingagency.id 
LEFT JOIN applicationorg agency ON        la.agencyid = agency.id 
LEFT JOIN companyuserdesignation compdesig ON        allocationowner.id = compdesig.companyuserid 
LEFT JOIN designation desig ON        desig.id = compdesig.designationid 
  
WHERE     la.lastmodifieddate > :sql_last_value 

ORDER BY  la.lastmodifieddate ASC

