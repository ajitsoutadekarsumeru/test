SELECT  
collec.id                                          AS collections_id_paymentid,
(CASE
WHEN DATEDIFF( now(),collec.collectiondate) < 5 THEN '0-5'
WHEN DATEDIFF( now(),collec.collectiondate) BETWEEN 5 AND 10 THEN '6-10'
WHEN DATEDIFF( now(),collec.collectiondate) BETWEEN 11 AND 15 THEN '11-15'
WHEN DATEDIFF(now(),collec.collectiondate ) BETWEEN 16 AND 20 THEN '16-20'
WHEN DATEDIFF(now(),collec.collectiondate ) BETWEEN 21 AND 25 THEN '21-25'
ELSE '>26' END) AS daysbucket,
(
CASE 
WHEN paynSlip.dateofdeposit is null THEN 
case when datediff(now(),collec.collectiondate) <= 0 then '0'
 when datediff(now(),collec.collectiondate) between 1 and 3 then '1-3'
 when datediff(now(),collec.collectiondate) between 4 and 6 then '4-6'
 when datediff(now(),collec.collectiondate) between 7 and 10 then '7-10'
 when datediff(now(),collec.collectiondate) > 10 then '>10' end 

ELSE 
case when datediff(paynSlip.dateofdeposit,collec.collectiondate) <=0 then '0' 
when datediff(paynSlip.dateofdeposit,collec.collectiondate) between 1 and 3 then '1-3'
when datediff(paynSlip.dateofdeposit,collec.collectiondate) between 4 and 6 then '4-6'
when datediff(paynSlip.dateofdeposit,collec.collectiondate) between 7 and 10 then '7-10'
when datediff(paynSlip.dateofdeposit,collec.collectiondate) > 10 then '>10'
end  
end
) as hold_days
		
FROM     collections collec		  
LEFT JOIN collectionbatches collecBatch   			ON      collec.collectionbatchid = collecBatch.id
LEFT JOIN payinslips paynSlip          				ON      collecBatch.payinslipid = paynSlip.id

ORDER BY  collec.lastmodifieddate ASC



