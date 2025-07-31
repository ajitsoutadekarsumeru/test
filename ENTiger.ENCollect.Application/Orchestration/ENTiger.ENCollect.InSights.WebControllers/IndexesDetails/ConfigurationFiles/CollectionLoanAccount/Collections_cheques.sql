SELECT   
collec.id AS collections_id_paymentid,
chq.lastmodifieddate AS cheques_lastmodifieddate,
chq.instrumentdate AS cheques_instrumentdate_instrumentdate,
chq.instrumentno AS cheques_instrumentno_instrumentno,
chq.micrcode AS cheques_micrcode_micrcode		
FROM      collections collec
LEFT JOIN cheques chq     ON  collec.chequeid = chq.id
WHERE     chq.lastmodifieddate > :sql_last_value 
ORDER BY  collec.lastmodifieddate ASC



