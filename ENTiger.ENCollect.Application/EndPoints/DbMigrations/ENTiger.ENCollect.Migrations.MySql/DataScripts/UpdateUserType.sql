update applicationuser au
inner join accountabilities a on a.responsibleid = au.Id
SET au.usertype ='FOS'
where a.AccountabilityTypeId like '%FOS%';
 
update applicationuser au
inner join accountabilities a on a.responsibleid = au.Id
SET au.usertype ='Telecaller'
where a.AccountabilityTypeId like '%TC%';
 
update applicationuser 
SET usertype ='Others'
where usertype is null or usertype = '';