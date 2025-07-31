-- only 26 fields are required for communication template database field mapping
DELETE FROM categoryitem WHERE categorymasterId = 'CommunicationValue'
	AND Name NOT IN
	('AGREEMENTID','BOM_POS','CURRENT_MINIMUM_AMOUNT_DUE','CURRENT_POS','CURRENT_TOTAL_AMOUNT_DUE','CUSTOMERID'
		,'CUSTOMERNAME','DueDate','EMAIL_ID','EMI_OD_AMT','EMIAMT','INTEREST_OD','LOAN_STATUS','MASKEDAGREEMENTID'
		,'NEXT_DUE_DATE','NO_OF_EMI_OD','NPA_STAGEID','PaymentStaticLink','PRIMARYCARDNUMBER','PRINCIPAL_OD'
	,'PRODUCT','ProductGroup','PTPDate','TOS','TOTAL_ARREARS','TOTAL_OUTSTANDING');

-- same value for database field
update categoryitem set Code = 'AGR123456789',Name = 'Account Number',Description = 'AGREEMENTID' where categorymasterId = 'CommunicationValue' and Name = 'AGREEMENTID';
update categoryitem set Code = 'Rs 2,000',Name = 'BOM Principal Outstanding',Description = 'BOM_POS' where categorymasterId = 'CommunicationValue' and Name = 'BOM_POS';
update categoryitem set Code = 'Rs 1,500',Name = 'Current Minimum Due',Description = 'CURRENT_MINIMUM_AMOUNT_DUE' where categorymasterId = 'CommunicationValue' and Name = 'CURRENT_MINIMUM_AMOUNT_DUE';
update categoryitem set Code = 'Rs 45,000',Name = 'Current Principal Outstanding',Description = 'CURRENT_POS' where categorymasterId = 'CommunicationValue' and Name = 'CURRENT_POS';
update categoryitem set Code = 'Rs 5,000',Name = 'Current Total Overdue',Description = 'CURRENT_TOTAL_AMOUNT_DUE' where categorymasterId = 'CommunicationValue' and Name = 'CURRENT_TOTAL_AMOUNT_DUE';
update categoryitem set Code = 'CUST987654',Name = 'Customer ID',Description = 'CUSTOMERID' where categorymasterId = 'CommunicationValue' and Name = 'CUSTOMERID';
update categoryitem set Code = 'John Doe',Name = 'Customer Name',Description = 'CUSTOMERNAME' where categorymasterId = 'CommunicationValue' and Name = 'CUSTOMERNAME';
update categoryitem set Code = '7',Name = 'Due Date',Description = 'DueDate' where categorymasterId = 'CommunicationValue' and Name = 'DueDate';
update categoryitem set Code = 'johndoe@example.com',Name = 'Email ID',Description = 'EMAIL_ID' where categorymasterId = 'CommunicationValue' and Name = 'EMAIL_ID';
update categoryitem set Code = 'Rs 30,000',Name = 'EMI Overdue',Description = 'EMI_OD_AMT' where categorymasterId = 'CommunicationValue' and Name = 'EMI_OD_AMT';
update categoryitem set Code = 'Rs 15,000',Name = 'EMI Amount',Description = 'EMIAMT' where categorymasterId = 'CommunicationValue' and Name = 'EMIAMT';
update categoryitem set Code = 'Rs 1,000',Name = 'Interest',Description = 'INTEREST_OD' where categorymasterId = 'CommunicationValue' and Name = 'INTEREST_OD';
update categoryitem set Code = 'Active',Name = 'Loan Status',Description = 'LOAN_STATUS' where categorymasterId = 'CommunicationValue' and Name = 'LOAN_STATUS';
update categoryitem set Code = 'AGR******6789',Name = 'Masked Account Number',Description = 'MASKEDAGREEMENTID' where categorymasterId = 'CommunicationValue' and Name = 'MASKEDAGREEMENTID';
update categoryitem set Code = '7/10/2025',Name = 'Next Due Date',Description = 'NEXT_DUE_DATE' where categorymasterId = 'CommunicationValue' and Name = 'NEXT_DUE_DATE';
update categoryitem set Code = '2',Name = 'Number Of EMI',Description = 'NO_OF_EMI_OD' where categorymasterId = 'CommunicationValue' and Name = 'NO_OF_EMI_OD';
update categoryitem set Code = 'Stage 2',Name = 'NPA Stage',Description = 'NPA_STAGEID' where categorymasterId = 'CommunicationValue' and Name = 'NPA_STAGEID';
update categoryitem set Code = 'https://pay.example.com/AGR123456789',Name = 'Payment Static Link',Description = 'PaymentStaticLink' where categorymasterId = 'CommunicationValue' and Name = 'PaymentStaticLink';
update categoryitem set Code = 'XXXX-XXXX-XXXX-1234',Name = 'Credit Card Number',Description = 'PRIMARYCARDNUMBER' where categorymasterId = 'CommunicationValue' and Name = 'PRIMARYCARDNUMBER';
update categoryitem set Code = 'Rs 42,000',Name = 'Principal Outstanding',Description = 'PRINCIPAL_OD' where categorymasterId = 'CommunicationValue' and Name = 'PRINCIPAL_OD';
update categoryitem set Code = 'Home Loan',Name = 'Product',Description = 'PRODUCT' where categorymasterId = 'CommunicationValue' and Name = 'PRODUCT';
update categoryitem set Code = 'Mortgage',Name = 'Product Group',Description = 'ProductGroup' where categorymasterId = 'CommunicationValue' and Name = 'ProductGroup';
update categoryitem set Code = '8/10/2025',Name = 'PTP Date',Description = 'PTPDate' where categorymasterId = 'CommunicationValue' and Name = 'PTPDate';
update categoryitem set Code = 'Rs 4,70,000',Name = 'TOS',Description = 'TOS' where categorymasterId = 'CommunicationValue' and Name = 'TOS';
update categoryitem set Code = 'Rs 8,000',Name = 'Total Arrears',Description = 'TOTAL_ARREARS' where categorymasterId = 'CommunicationValue' and Name = 'TOTAL_ARREARS';
update categoryitem set Code = 'Rs 5,00,000',Name = 'Total Outstanding',Description = 'TOTAL_OUTSTANDING' where categorymasterId = 'CommunicationValue' and Name = 'TOTAL_OUTSTANDING';

--language category master
INSERT INTO categorymaster(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name)
VALUES('Languages',0,NULL,NOW(),NULL,NOW(),'Languages');
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'English','Languages',NULL,'English','English',0);
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'Tamil','Languages',NULL,'Tamil','Tamil',0);
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'Telugu','Languages',NULL,'Marathi','Marathi',0);
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'Kannada','Languages',NULL,'Kannada','Kannada',0);
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'Hindi','Languages',NULL,'Hindi','Hindi',0);
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'Malayalam','Languages',NULL,'Malayalam','Malayalam',0);
 
INSERT INTO categoryitem(Id,IsDeleted,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Name,categorymasterId,ParentId,Code,Description,IsDisabled)
VALUES(REPLACE(NEWID(), '-', ''),0,NULL,NOW(),NULL,NOW(),'Marathi','Languages',NULL,'Marathi','Marathi',0);


--trigger type
INSERT INTO TriggerType (Id, Name, IsActive,Description,CreatedDate,LastModifiedDate,IsDeleted) 
VALUES 
(REPLACE(NEWID(), '-', ''), 'On Xth day before due date', '1', 'Send communication X days before payment is due', NOW(), NOW(), '0'),
(REPLACE(NEWID(), '-', ''), 'On Xth day after statement date', '1', 'Send communication X days after statement is generated', NOW(), NOW(), '0'),
(REPLACE(NEWID(), '-', ''), 'On X DPD', '1', 'Send communication when account is X days past due',NOW(), NOW(), '0'),
(REPLACE(NEWID(), '-', ''), 'On PTP Date', '1', 'Send communication on promise to pay date', NOW(), NOW(), '0'),
(REPLACE(NEWID(), '-', ''), 'On Broken PTP', '1', 'Send communication when promise to pay is broken', NOW(), NOW(), '0'),
(REPLACE(NEWID(), '-', ''), 'On Agency allocation change', '1', 'Send communication when account is transferred to a new agency',NOW(), NOW(), '0');
 